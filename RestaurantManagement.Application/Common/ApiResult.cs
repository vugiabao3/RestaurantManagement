namespace RestaurantManagement.Application.Common;

public class ApiResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResult<T> Ok(T data, string message = "Thành công.")
        => new() { Success = true, Message = message, Data = data };

    public static ApiResult<T> Fail(string message, T? data = default)
        => new() { Success = false, Message = message, Data = data };
}
