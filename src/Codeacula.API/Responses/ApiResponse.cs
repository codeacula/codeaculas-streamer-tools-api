namespace Codeacula.API.Responses;

public static class ApiResponse
{
  public static ApiResponse<T> Ok<T>(T data) => new(data);

  public static ApiResponse<string> Error(string errorMessage) => new("500", errorMessage);
}
