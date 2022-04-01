using FileServiceApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileServiceApi.Filter
{
    public class ResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var objectResult = context.Result as ObjectResult;
            if (objectResult != null)
            {

                context.Result = new ObjectResult(new CustomApiResult
                {
                    Code = 200,
                    Message = "请求成功",
                    Data = objectResult.Value
                });

            }
        }
    }
}