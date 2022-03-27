using DataModel;
using DataModel.User;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;

namespace FileServiceRepsitory.Repository
{
    public class UserModelRepository:BaseRepository<UserModel,FileServiceDbContext>,IUserRepository
    {
        public UserModelRepository(FileServiceDbContext dbContext) : base(dbContext)
        {
        }


        public override async Task<List<UserModel>> FindAllAsync(UserModel t)
        {
            return await DbContext.Users.Include(u=>u.LoginRecords).ToListAsync();
        }
    }
}