using FileService.Application;
using FileService.Filter;
using FileService.Middleware;
using FileService.ServerExceptionHandler;
using FileServiceApi.Filter;
using FileServiceApi.ServiceExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt => opt.Filters.Add<ResultFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCustomService();

builder.Services.AddJwtAuth(builder.Configuration["AuthKey"]);

builder.Services.AddDbContext(builder.Configuration["sqlCon"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionMiddleware>();

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// 自动迁移表
app.AutoMigateDatebase();

app.Run();
