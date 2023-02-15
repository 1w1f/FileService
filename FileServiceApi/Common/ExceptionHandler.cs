using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;

namespace FileServiceApi.Common;

public static class ExceptionHandler
{
    public static RequestDelegate HandlerHttpFeatureException = async (ctx) =>
    {
        var exceptionDetails = ctx.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionDetails.Error as BusinessException;
        var result = new CustomApiResult()
        {
            Code = exception != null ? exception.Code : StatusCodes.Status500InternalServerError,
            Message = exception != null ? exception.Message : exceptionDetails.Error.Message,
            Data = exceptionDetails.Error.StackTrace,
            TraceId = ctx.TraceIdentifier
        };
        await ctx.Response.WriteAsJsonAsync(result);
    };
}