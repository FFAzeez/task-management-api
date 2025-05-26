using System.Net;
using System.Text.Json;
using TeamTaskManagementAPI.Domain.BindingModels.Common;
using TeamTaskManagementAPI.Domain.BindingModels.Response;

namespace TeamTaskManagementAPI.Middleware;

public class ErrorExceptionMiddleware(RequestDelegate next, ILogger<ErrorExceptionMiddleware> logger)
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        var response = context.Response;
        response.ContentType = "application/json";
//new BaseResponse(){ Success = Constant.FailedBool , Message = exception.Message}
        var (statusCode, message) = exception switch
        {
            KeyNotFoundException => (HttpStatusCode.NotFound, exception.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, exception.Message),
            InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message),
            ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
            _ => (HttpStatusCode.InternalServerError, Constant.Error)
        };

        response.StatusCode = (int)statusCode;
        var result = JsonSerializer.Serialize(new BaseResponse(){ Success = Constant.FailedBool , Message = message });
        await response.WriteAsync(result);
    }
}