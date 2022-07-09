using DataModel;
using DataModel.User;

namespace FileServiceRepsitory.IRepository;

public interface IUserRepository : IBaseRepository<UserDto>
{
    Task<UserDto> FindUserByUserNameAndPassWord(UserDto userDto);
}
