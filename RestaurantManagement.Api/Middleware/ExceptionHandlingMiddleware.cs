using System.Net;
using System.Text.Json;
using RestaurantManagement.Application.Common.Exceptions;

namespace RestaurantManagement.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var status = ex switch
                {
                    NotFoundException => HttpStatusCode.NotFound,
                    ConflictException => HttpStatusCode.Conflict,
                    BusinessException => HttpStatusCode.BadRequest,
                    _ => HttpStatusCode.InternalServerError
                };

                if (status == HttpStatusCode.InternalServerError) _logger.LogError(ex, "Unhandled exception");
                context.Response.StatusCode = (int)status;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    message = status == HttpStatusCode.InternalServerError ? "Lỗi hệ thống, vui lòng thử lại sau" : ex.Message
                }));
            }
        }
    }
}
