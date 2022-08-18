using System.Text;
using FileService.AutoMapper.Profiles;
using FileService.Service.IService;
using FileService.Service.Service.File;
using FileServiceApi.Service.File;
using FileServiceApi.Service.Service;
using FileServiceApi.Service.Service.LoginRecord;
using FileServiceApi.Service.Service.LoginRecord.IService;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository;
using FileServiceRepsitory.Repository.DbContextModel;
using FileServiceRepsitory.Repository.LoginRecord;
using FileServiceRepsitory.Repository.LoginRecord.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FileServiceApi.ServiceExtension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCustomService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserRepository, UserModelRepository>();
            serviceCollection.AddScoped<ILoginRecordService, LoginRecordService>();
            serviceCollection.AddScoped<ILoginRecordRepository, LoginRecordRepository>();

            serviceCollection.AddScoped<IFileStoreService, FileStoreService>();

            #region AutoMapperDI
            serviceCollection.AddAutoMapper(typeof(UserProfile));
            #endregion


            #region configure Swagger

            serviceCollection.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new()
                {
                    Title = "FileServiceApi",
                    Version = "V1"
                });

                #region Swagger 使用鉴权组件

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Bearer 加空格",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference=new OpenApiReference{
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });



                #endregion



                //配置swagger从文件中读取相关注释
                var fileServiceFilePath = Path.Combine(System.AppContext.BaseDirectory, "FileService.xml");
                var DataModelFilePath = Path.Combine(System.AppContext.BaseDirectory, "DataModel.xml");
                option.IncludeXmlComments(fileServiceFilePath);
                option.IncludeXmlComments(DataModelFilePath);
            });
            #endregion

            return serviceCollection;
        }



        public static IServiceCollection AddDbContext(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<FileServiceDbContext>(
                option =>
                {
                    option.UseMySql(dbConnectionString, new MySqlServerVersion(new Version(8, 0, 27)), x => x.MigrationsAssembly("FileServiceRepsitory"));
                    option.ReplaceService<IMigrationsModelDiffer, MigrationWithOutForegnKey>();
                }
            );

            return services;
        }

        /// <summary>
        /// 添加jwt认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="authKey">jwt私匙</param>
        /// <returns></returns>
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, string authKey)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authKey)),
                    ValidateIssuer = true,
                    ValidIssuer = "http://192.168.1.102:5129",
                    ValidateAudience = true,
                    ValidAudience = "http://192.168.1.102:5129",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(60),
                };
            });

            return services;
        }




    }
}