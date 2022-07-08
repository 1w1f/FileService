using System;
using System.Net;
using AutoMapper;
using DataModel;
using DataModel.User;
using DataModel.User.Vo;
using FileService.Common;
using FileService.Common.Utilities;
using FileService.Service.IService;
using FileServiceApi.Common;
using FileServiceApi.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FileServiceApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    [ResultFilter]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        protected IUserService UserService { get; set; }
        protected IMapper Mapper { get; set; }

        private ILogger<UserController> _logger { get; set; }

        public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            UserService = userService;
            Mapper = mapper;
        }
        /// <summary>
        /// 创建用户 
        /// </summary>
        /// <param name="vo">uservo</param>
        /// <returns></returns>
        [HttpPost("Create")]

        public async Task<ActionResult<UserVo>> CreateUser([FromBody] UserVo vo)
        {
            // return NotFound(new UserDto());
            return Mapper.Map<UserDto, UserVo>(new UserDto());
            // throw new ArgumentException(@"系统异常测试");
            // return new UserDto();
            // if (string.IsNullOrEmpty(vo.Name) || string.IsNullOrEmpty(vo.PassWord))
            // {

            // }
            // var user = Mapper.Map<UserVo, UserDto>(vo);
            // user.CreateTime = new DateTime();
            // var result = await UserService.Create(user);
            // if (result != null)
            // {
            //     return result;
            // }
            // else
            // {
            //     return null;
            // }
        }


        /// <summary>
        /// 获取所有User
        /// </summary>
        /// <returns></returns>
        [HttpGet("FindAllUsers")]
        public async Task<ActionResult<List<UserDto>>> FindUserIncludeLoginRecords()
        {
            return await UserService.FindAllAsync();
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] UserVo userVo)
        {
            
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            _logger.LogInformation($"clientIP:{clientIp}");
            return "登录成功";
        }

        // public async Task<Action<UserD>>
    }
}