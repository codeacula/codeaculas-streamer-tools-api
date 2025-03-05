namespace Codeacula.Core.Common.Services;

public interface IHttpClientWrapper
{
  Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content);

  Task<HttpResponseMessage> GetAsync(Uri requestUri);

  void SetAuthorizationHeader(string scheme, string parameter);

  void AddDefaultRequestHeader(string name, string value);
}
