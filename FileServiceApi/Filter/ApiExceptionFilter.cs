using System.Diagnostics;
using FileServiceApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileService.Filter;

public class BusinessExceptionFilter : IExceptionFilter
{
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
                System.Console.WriteLine(context.Exception.StackTrace);
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
