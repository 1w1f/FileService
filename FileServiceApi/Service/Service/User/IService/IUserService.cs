using DataModel;
using DataModel.User;
using FileService.Service.Service;
using FileServiceRepsitory.IRepository;

namespace FileService.Service.IService;

public interface IUserService : IBaseService<UserDto, IUserRepository>
{
    Task<UserDto> FindByUserNameAndPassWord(UserDto userDto);
}
