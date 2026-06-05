namespace RestaurantManagement.Application.Common;

public class NghiepVuException : Exception
{
    public int StatusCode { get; }

    public NghiepVuException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
