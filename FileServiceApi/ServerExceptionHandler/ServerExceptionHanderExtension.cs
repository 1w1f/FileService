using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace FileService.ServerExceptionHandler;

public static class ServerExceptionHanderExtension
{


    public static IApplicationBuilder UseCustomExceptionHandler(this WebApplication app)
    {

        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = Text.Plain;
                await context.Response.WriteAsync("An exception was thrown");
                var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();
                await context.Response.WriteAsync("异常信息是:" + exceptionHandlerPathFeature.Error.Message);
            });
            app.UseHsts();
        });


        return app;
    }
}
