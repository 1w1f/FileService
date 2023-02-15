using System.Diagnostics;
using FileService.Common;
using FileServiceApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileService.Filter;

public class BusinessExceptionFilter : IExceptionFilter
{
    protected ILogger<BusinessExceptionFilter> _logger;
    public BusinessExceptionFilter(ILogger<BusinessExceptionFilter> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        if (context.Exception != null)
        {
            if (context.Exception is BusinessException businessException)
            {
                context.Result = new ObjectResult(new CustomApiResult
                {
                    Code = businessException.Code,
                    Message = businessException.Message
                });
            }
            else
            {
                _logger.LogError($"异常:{context.Exception.GetType()}");
                context.Result = new ObjectResult(new CustomApiResult
                {
                    Code = 500,
                    Message = $"内部错误",
                    Data = new
                    {
                        context.Exception.Message,
                        context.Exception.InnerException,
                        context.Exception.StackTrace
                    }
                }
                    );
                context.ExceptionHandled = true;
            }
        }
    }
}
