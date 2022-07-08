using FileServiceApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileServiceApi.Filter
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {

                context.Result = new ObjectResult(new CustomApiResult
                {
                    Code = (int)objectResult.StatusCode,
                    Message = "请求成功",
                    Data = objectResult.Value,
                    TraceId = context.HttpContext.TraceIdentifier
                });

            }
        }
    }
}