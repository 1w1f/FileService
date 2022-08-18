using DataModel;
using DataModel.User;
using FileService.Service.BaseService;
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

        public Task<bool> UpdateUserNameAndPassWord(UserDto userDto, bool updateName, bool updatePassWord)
        {
            return Repository.UpdateNameAndPassWord(userDto, updateName, updatePassWord);
        }
    }
}