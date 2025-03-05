namespace Codeacula.Infrastructure.Http;

using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Codeacula.Core.Common.Services;

[ExcludeFromCodeCoverage]
public class HttpClientWrapper(HttpClient httpClient) : IHttpClientWrapper
{
  private readonly HttpClient httpClient = httpClient;

  public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content) => httpClient.PostAsync(requestUri, content);

  public Task<HttpResponseMessage> GetAsync(Uri requestUri) => httpClient.GetAsync(requestUri);

  public void SetAuthorizationHeader(string scheme, string parameter) => httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);

  public void AddDefaultRequestHeader(string name, string value) => httpClient.DefaultRequestHeaders.Add(name, value);
}
