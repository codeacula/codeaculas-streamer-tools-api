namespace Codeacula.API;

using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Codeacula.API.Infrastructure.JWT;
using Codeacula.Core;
using Codeacula.Core.Domain.Authentication.Configuration;
using Codeacula.Core.Logging;
using Codeacula.Infrastructure;
using Codeacula.MongoDB;
using Codeacula.MSSQL;
using Codeacula.Redis;
using Codeacula.Redis.Connections;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;

[ExcludeFromCodeCoverage]
internal sealed class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    _ = builder.Configuration.AddEnvironmentVariables()
      .AddUserSecrets<Program>();

    _ = builder.Logging.ClearProviders()
      .AddConsole();

    if (builder.Environment.IsProduction())
    {
      _ = builder.Services.AddApplicationInsightsTelemetry();
      _ = builder.Logging.AddApplicationInsights();
    }

    // Add services to the container.
    _ = builder.Services.AddOpenApi();
    _ = builder.Services.AddControllers();

    // Add API versioning
    _ = builder.Services.AddApiVersioning(options =>
    {
      options.DefaultApiVersion = new ApiVersion(1, 0);
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.ReportApiVersions = true;
      options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

    _ = builder.Services.AddVersionedApiExplorer(options =>
    {
      options.GroupNameFormat = "'v'VVV";
      options.SubstituteApiVersionInUrl = true;
    });

    _ = builder.Services.AddHttpClient();
    _ = builder.Services.AddCors(options =>
    {
      if (builder.Environment.IsDevelopment())
      {
        options.AddPolicy(
          "AllowAllOrigins",
          builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
      }
      else
      {
        options.AddPolicy(
          "AllowAllOrigins",
          builder => builder
                .WithOrigins("https://codeacula.com")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
      }
    });
    _ = builder.Services.AddEndpointsApiExplorer()
      .AddSwaggerGen();

    if (builder.Environment.IsDevelopment())
    {
      _ = builder.Services.Configure<CookiePolicyOptions>(options =>
            {
              options.MinimumSameSitePolicy = SameSiteMode.None;
              options.HttpOnly = HttpOnlyPolicy.Always;
              options.Secure = CookieSecurePolicy.SameAsRequest;
            });
    }
    else
    {
      _ = builder.Services.Configure<CookiePolicyOptions>(options =>
          {
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.HttpOnly = HttpOnlyPolicy.Always;
            options.Secure = CookieSecurePolicy.Always;
          });
    }

    // Make a JWT service using the local settings
    var jwtConfigSection = builder.Configuration.GetSection("Jwt");
    var jwtConfigSettings = jwtConfigSection.Get<JwtConfigSettings>() ?? throw new ConfigurationErrorsException("JWT settings not found in configuration");

    _ = builder.Services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = jwtConfigSettings.Issuer,
      ValidAudience = jwtConfigSettings.Audience,
      IssuerSigningKey = jwtConfigSettings.SigningKey,
    });

    _ = builder.Services.AddAuthorization();

    var twitchConfig = builder.Configuration.GetSection("Twitch") ?? throw new ConfigurationErrorsException("Missing Twitch Config");
    var twitchSettings = twitchConfig.Get<OAuthConfigSettings>() ?? throw new ConfigurationErrorsException("Config settings not found in Twitch settings");

    if (string.IsNullOrEmpty(twitchSettings.ClientId) || string.IsNullOrEmpty(twitchSettings.ClientSecret))
    {
      throw new InvalidOperationException("Twitch ClientId or ClientSecret is not set");
    }

    _ = builder.Services.AddScoped<IJwtService, JwtService>();

    // Add the Codeacula.Core context
    _ = builder.Services.AddCoreContext(twitchSettings)
      .AddDomainContext();

    // Add the Codeacula.Infrastructure context
    _ = builder.Services.AddInfrastructureContext(jwtConfigSettings);

    // Configure SQL Server
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string is not set");
    _ = builder.Services.AddMSSQLContext(connectionString);

    // Configure Redis
    var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? throw new InvalidOperationException("Redis connection string is not set");
    _ = builder.Services.AddRedisContext(new RedisCacheConnection(redisConnectionString));

    // Databases
    _ = builder.Services.AddMongoDB(builder.Configuration.GetSection("MongoDb"));

    var app = builder.Build();

    // Add logging middleware
    _ = app.Use(async (context, next) =>
    {
      var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
      LogDefinitions.HandlingRequestMessage(logger, context.Request.Method, context.Request.Path);

      await next.Invoke();
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      _ = app.UseDeveloperExceptionPage();
    }
    else
    {
      _ = app.UseExceptionHandler("/Home/Error");
      _ = app.UseHsts();
    }

    if (app.Environment.IsDevelopment())
    {
      _ = app.UseSwagger();
      _ = app.UseSwaggerUI();
    }

    _ = app.MapSwagger();

    _ = app.UseHttpsRedirection();
    _ = app.UseStaticFiles();

    // Add this line and ensure it's before UseRouting and UseAuthorization
    _ = app.UseCors("AllowAllOrigins");

    _ = app.UseRouting();
    _ = app.UseAuthorization();
    _ = app.MapControllers();
    app.Run();
  }
}
