using DataModel.User;
using FileService.Service.BaseService;
using FileServiceRepsitory.IRepository;

namespace FileService.Service.IService;

public interface IUserService : IBaseService<UserDto, IUserRepository>
{

    Task<UserDto> FindByUserNameAndPassWord(UserDto userDto);

    Task<bool> UpdateUserNameAndPassWord(UserDto userDto, bool updateName, bool updatePassWord);

}
