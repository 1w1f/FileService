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
using FileServiceApi.Service.Service.LoginRecord.IService;
using FileServiceRepsitory.Repository.LoginRecord.IRepository;
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

        protected ILoginRecordService LoginRecordService { get; set; }
        private ILogger<UserController> _logger
        { get; set; }

        public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper, ILoginRecordService loginRecordService)
        {
            _logger = logger;
            UserService = userService;
            LoginRecordService = loginRecordService;
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
            var userDto = Mapper.Map<UserVo, UserDto>(userVo);
            var user = await UserService.FindByUserNameAndPassWord(userDto);
            if (user != null)
            {

                return "登录成功";
            }
            else
            {
                return "用户名密码登录失败";
            }
        }

        // public async Task<Action<UserD>>
    }
}