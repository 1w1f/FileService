using DataModel;
using DataModel.User;
using FileService.Service.IService;
using FileService.Service.Service;
using FileServiceRepsitory.IRepository;

namespace FileServiceApi.Service.Service
{
    public class UserService : BaseService<UserModel>, IUserService
    {
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
        }
        public override async Task<List<UserModel>> FindAllAsync()
        {
            return await BaseRepository.FindAllAsync(new UserModel());
        }
    }
}