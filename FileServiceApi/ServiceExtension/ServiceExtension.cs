using System.Configuration;
using FileService.AutoMapper.Profiles;
using FileService.Service.IService;
using FileServiceApi.Service.Service;
using FileServiceApi.Service.Service.LoginRecord;
using FileServiceApi.Service.Service.LoginRecord.IService;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository;
using FileServiceRepsitory.Repository.DbContextModel;
using FileServiceRepsitory.Repository.LoginRecord;
using FileServiceRepsitory.Repository.LoginRecord.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FileServiceApi.ServiceExtension
{
    public static class ServiceExtension
    {
        public static void AddCustom(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserRepository, UserModelRepository>();
            serviceCollection.AddScoped<ILoginRecordService,LoginRecordService>();
            serviceCollection.AddScoped<ILoginRecordRepository,LoginRecordRepository>();

            #region AutoMapperDI
            serviceCollection.AddAutoMapper(typeof(UserProfile));
            #endregion
        }
    }
}