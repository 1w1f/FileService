using FileService.Filter;
using FileServiceApi.ServiceExtension;
using FileServiceRepsitory.Repository;
using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt => opt.Filters.Add<BusinessExceptionFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new()
    {
        Title = "FileServiceApi",
        Version = "V1"
    });
    //配置swagger从文件中读取相关注释
    var fileServiceFilePath = Path.Combine(System.AppContext.BaseDirectory, "FileService.xml");
    var DataModelFilePath = Path.Combine(System.AppContext.BaseDirectory, "DataModel.xml");
    option.IncludeXmlComments(fileServiceFilePath);
    option.IncludeXmlComments(DataModelFilePath);

});
builder.Services.AddCustom();

var sqlConection = builder.Configuration["sqlCon"];
builder.Services.AddDbContext<FileServiceDbContext>(
    option =>
    {
        option.UseMySql(sqlConection, new MySqlServerVersion(new Version(8, 0, 27)), x => x.MigrationsAssembly("FileServiceRepsitory"));
        option.ReplaceService<IMigrationsModelDiffer, MigrationWithOutForegnKey>();
    }
);
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



