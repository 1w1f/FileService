using System.Configuration;
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
 
        }
    }
}