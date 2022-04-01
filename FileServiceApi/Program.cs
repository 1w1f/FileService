using FileService.Filter;
using FileServiceApi.ServiceExtension;
using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// builder.WebHost.UseKestrel(option => { option.Limits.MaxRequestBodySize = int.MaxValue; });
// Add services to the container.
builder.Services.AddControllers(opt=>opt.Filters.Add<ApiExceptionFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustom();


// var sqlConection=new ConfigurationManager()["sqlCon"];
var sqlConection = builder.Configuration["sqlCon"];
builder.Services.AddDbContext<FileServiceDbContext>(option => option.UseMySql(sqlConection, new MySqlServerVersion(new Version(8,0,27)), x => x.MigrationsAssembly("FileServiceRepsitory")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



