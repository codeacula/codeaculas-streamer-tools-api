# ============================
# 🔹 STAGE 1: Production Build (Unchanged)
# ============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY ../ ./
RUN dotnet restore codeaculas-streamer-tools-api.sln
RUN dotnet build --configuration Release --output /app/build

# ============================
# 🔹 STAGE 2: Production
# ============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS production
WORKDIR /app
COPY --from=build /app/build /app/build
CMD ["dotnet", "/app/build/codeaculas-streamer-tools-api.dll"]

# ============================
# 🔹 STAGE 3: Development
# ============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dev
WORKDIR /app

COPY ./config/entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh

CMD ["/entrypoint.sh"]

# ============================
# 🔹 STAGE 4: Integration Tests
# ============================
FROM build AS test
RUN dotnet test --logger trx --results-directory /app/test-results
CMD ["tail", "-f", "/dev/null"]
