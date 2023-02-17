using FileService.Common;

namespace FileService.Middleware;

public class CustomExceptionMiddleware
{
    private RequestDelegate _next;
    private ILogger<CustomExceptionMiddleware> _logger;
    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContent)
    {
        try
        {
            await _next(httpContent);
        }
        catch (Exception e)
        {
            httpContent.Response.StatusCode = e is BusinessException exception ? exception.Code : StatusCodes.Status500InternalServerError;
            httpContent.Response.ContentType = "application/json";
            var apiResult = new CustomApiResult
            {
                Data = new
                {
                    code = httpContent.Response.StatusCode,
                    e.Message,
                    e.StackTrace,
                },
                TraceId = httpContent.TraceIdentifier
            };
            await httpContent.Response.WriteAsJsonAsync(apiResult);
        }
    }
}
