using DataModel;
using DataModel.User;
using FileService.Service.IService;
using FileService.Service.Service;
using FileServiceRepsitory.IRepository;

namespace FileServiceApi.Service.Service
{
    public class UserService : BaseService<UserDto>, IUserService
    {
        public UserService(IUserRepository userRepository) : base(userRepository)
        {

        }
        public override async Task<UserDto> Create(UserDto userDto)
        {
            return await BaseRepository.Create(userDto);
        }
        public override async Task<List<UserDto>> FindAllAsync()
        {
            return await BaseRepository.FindAllAsync();
        }
    }
}