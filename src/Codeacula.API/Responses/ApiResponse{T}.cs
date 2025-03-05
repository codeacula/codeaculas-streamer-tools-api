namespace Codeacula.API.Responses;

public sealed class ApiResponse<T>
{
  public ApiResponse()
  {
    Success = false;
  }

  public ApiResponse(T data)
  {
    Success = true;
    Data = data;
  }

  public ApiResponse(string errorCode, string errorMessage)
  {
    Success = false;
    ErrorCode = errorCode;
    ErrorMessage = errorMessage;
  }

  public T? Data { get; set; }

  public string? ErrorCode { get; set; }

  public string? ErrorMessage { get; set; }

  public bool Success { get; set; }

  public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
