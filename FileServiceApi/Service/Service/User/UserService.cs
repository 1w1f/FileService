using DataModel;
using DataModel.User;
using FileService.Service.IService;
using FileService.Service.Service;
using FileServiceRepsitory.IRepository;

namespace FileServiceApi.Service.Service
{
    public class UserService : BaseService<UserDto, IUserRepository>, IUserService
    {
        public UserService(IUserRepository userRepository) : base(userRepository)
        {

        }

        public async Task<UserDto> FindByUserNameAndPassWord(UserDto userDto)
        {
            return await Repository.FindUserByUserNameAndPassWord(userDto);
        }

    }
}