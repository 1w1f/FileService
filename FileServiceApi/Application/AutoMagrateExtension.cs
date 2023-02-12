using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;

namespace FileService.Application;

public static class AutoMagrateExtension
{

    public static void AutoMigateDatebase(this WebApplication app)
    {

        try
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<FileServiceDbContext>();
            context.Database.Migrate();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
