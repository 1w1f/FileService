using System.Configuration;
using FileService.AutoMapper.Profiles;
using FileService.Service.IService;
using FileServiceApi.Service.Service;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository;
using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;

namespace FileServiceApi.ServiceExtension
{
    public static class ServiceExtension
    {
        public static void AddCustom(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IUserRepository, UserModelRepository>();
 


            #region AutoMapperDI
            serviceCollection.AddAutoMapper(typeof(UserProfile));
            #endregion
            #region IPMiddleware
                
            #endregion
        }
    }
}