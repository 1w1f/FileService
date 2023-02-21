using DataModel;
using DataModel.User;
using FileServiceRepsitory.Repository.Base;

namespace FileServiceRepsitory.IRepository;

public interface IUserRepository : IBaseRepository<UserDto>
{
    Task<UserDto> FindUserByUserNameAndPassWord(UserDto userDto);


    Task<bool> UpdateNameAndPassWord(UserDto userDto, bool updateName, bool updatePassWord);
}
