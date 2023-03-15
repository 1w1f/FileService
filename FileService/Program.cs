using FileService.Common;
using FileService.Common.ServiceExtension;
using FileService.Filter;
using FileService.Option;
using Minio.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(opt => { opt.Filters.Add<ResultFilter>(); });
builder.Services.AddEndpointsApiExplorer();
builder.AddConfigurationType();
builder.Services.AddCustomService();
builder.Services.AddJwtAuth(builder.Configuration["AuthKey"]);
builder.Services.AddDbContext(builder.Configuration["sqlCon"]);
builder.Services.AddMinio(option =>
{
    var minioSetUp = new MinioSetUp();
    builder.Configuration.Bind(nameof(MinioSetUp), minioSetUp);
    option.AccessKey = minioSetUp.AccessKey;
    option.SecretKey = minioSetUp.SecretKey;
    option.Endpoint = minioSetUp.EndPoint;
});
var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("home"))
{
    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        option.RoutePrefix = string.Empty;
    });
}
// 使用框架内部的异常中间件处理异常 代替自定义中间件
app.UseExceptionHandler(builder => builder.Run(ExceptionHandler.HandlerHttpFeatureException));

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();