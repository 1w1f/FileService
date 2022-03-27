using DataModel;
using DataModel.User;
using FileService.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace FileServiceApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class UserController : ControllerBase
    {
        protected IUserService UserService { get; set; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<bool>> CreateUser([FromBody] UserModel userModel)
        {
            var result = await UserService.Create(userModel);
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> FindUserIncludeLoginRecords()
        {
            return await UserService.FindAllAsync();
        }





    }
}