using DataModel.User;
using FileServiceRepsitory.IRepository;
using FileServiceRepsitory.Repository.Base;
using FileServiceRepsitory.Repository.DbContextModel;
using Microsoft.EntityFrameworkCore;

namespace FileServiceRepsitory.Repository.User
{
    public class UserRepository : BaseRepository<UserDto>, IUserRepository
    {
        public UserRepository(FileServiceDbContext dbContext) : base(dbContext)
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


        /// <summary>
        /// 根据用户名密码
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<UserDto> FindUserByUserNameAndPassWord(UserDto
        userDto)
        {
            var dtos = await DbContext.Users.Where(user => user.Name == userDto.Name && user.PassWord == userDto.PassWord).ToListAsync();
            return dtos.FirstOrDefault();
        }

        /// <summary>
        /// 更新Name,PassWord,UpdateTime等字段,传入的UserDto必须包括主键
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateNameAndPassWord(UserDto userDto, bool updateName, bool updatePassWord)
        {
            if (!updateName && !updatePassWord) return false;
            var user = DbContext.Users.Attach(userDto);
            if (updateName)
            {
                user.Property(u => u.Name).IsModified = true;
            }
            if (updatePassWord)
            {
                user.Property(u => u.PassWord).IsModified = true;
            }
            user.Property(u => u.UpdateTime).IsModified = true;
            return await DbContext.SaveChangesAsync() > 0;
        }
    }
}