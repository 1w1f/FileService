using System.Text.Json;
using FileService.Application;
using FileService.Middleware;
using FileServiceApi.Common;
using FileServiceApi.Filter;
using FileServiceApi.ServiceExtension;
using Microsoft.AspNetCore.Diagnostics;
using Minio.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt => opt.Filters.Add<ResultFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomService();

builder.Services.AddJwtAuth(builder.Configuration["AuthKey"]);

builder.Services.AddDbContext(builder.Configuration["sqlCon"]);
builder.Services.AddMinio(option =>
{
    option.AccessKey = builder.Configuration["AccessKey"];
    option.SecretKey = builder.Configuration["SecretKey"];
    option.Endpoint = builder.Configuration["192.168.50.16:9000"];
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// 使用框架内部的异常中间件处理异常 代替自定义中间件
// app.UseMiddleware<CustomExceptionMiddleware>();
app.UseExceptionHandler(builder => builder.Run(ExceptionHandler.HandlerHttpFeatureException));



// app.Run(ctx =>
// { throw new Exception("9999"); });

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
app.MapControllers();

// 自动迁移表
app.AutoMigateDatebase();

app.Run();