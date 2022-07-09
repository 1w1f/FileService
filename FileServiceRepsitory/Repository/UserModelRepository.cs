using DataModel;
using DataModel.User;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;

namespace FileServiceRepsitory.Repository
{
    public class UserModelRepository : BaseRepository<UserDto, FileServiceDbContext>, IUserRepository
    {
        public UserModelRepository(FileServiceDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 无实体跟踪，优化查询
        /// </summary>
        /// <returns></returns>
        public override async Task<List<UserDto>> FindAllAsync()
        {
            return await DbContext.Users.Include(u => u.LoginRecords).AsNoTracking().ToListAsync();
        }





        public async Task<UserDto> FindUserByUserNameAndPassWord(UserDto userDto)
        {
            var dtos= await DbContext.Users.Where(user => user.Name == userDto.Name && user.PassWord == userDto.PassWord).AsNoTracking().ToListAsync();
            return dtos.FirstOrDefault();
        }
    }
}