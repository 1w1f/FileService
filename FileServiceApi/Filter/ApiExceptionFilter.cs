using System.Diagnostics;
using FileServiceApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileService.Filter;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception != null)
        {
            if (context.Exception is ApiException apiException)
            {
                context.Result = new ObjectResult(new CustomApiResult
                {
                    Code = apiException.Code,
                    Message = apiException.Message
                });
            }
            else
            {
                System.Console.WriteLine(context.Exception.StackTrace);
                context.Result=new ObjectResult(new CustomApiResult{
                        Code=500,
                        Message=$"内部错误",
                        Data=new { 
                        Message=context.Exception.Message,
                        InnerException=context.Exception.InnerException,
                        StackTrace=context.Exception.StackTrace
                        }}
                    );
                context.ExceptionHandled=true;
            }
        }
    }
}
