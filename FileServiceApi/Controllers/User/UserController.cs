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

        public async Task<ActionResult<UserVo>> CreateUser([FromBody] UserWithPassWordVo vo)
        {
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
        public async Task<ActionResult<List<UserVo>>> FindUserIncludeLoginRecords()
        {
            var userDtoList= await UserService.FindAllAsync();
            return userDtoList.ConvertAll(new Converter<UserDto, UserVo>(userDto=>Mapper.Map<UserDto,UserVo>(userDto)));
        }


        [HttpGet("Login/{Name}/{passWord}")]
        public async Task<ActionResult<string>> Login(string Name, string passWord)
        {

            var clientIp = HttpContext.Connection.RemoteIpAddress;
            _logger.LogInformation($"clientIP:{clientIp}");
            var userDto = new UserDto
            {
                Name = Name,
                PassWord = passWord
            };
            var user = await UserService.FindByUserNameAndPassWord(userDto);
            if (user != null)
            {
                await LoginRecordService.CreateAsync(new LoginRecordDto(clientIp, DateTime.Now, user.Id));
                return "登录成功";
            }
            else
            {
                return "用户名密码登录失败";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateNameAndPassWord")]
        public async Task<ActionResult> UpdateUserName(UserWithPassWordVo userWithPassWordVo)
        {
            if (string.IsNullOrEmpty(userWithPassWordVo.Id))
            {
                throw new ArgumentException("修改的用户id不能为空");
            }
            bool updateName = false, updatePassWord = false;
            if (!string.IsNullOrEmpty(userWithPassWordVo.Name))
            {
                updateName = true;
            }
            if (!string.IsNullOrEmpty(userWithPassWordVo.PassWord))
            {
                updatePassWord = true;
            }
            if (!updateName && !updatePassWord)
            {
                throw new ArgumentException("无效操作，请检查传参");
            }
            var userDto = Mapper.Map<UserWithPassWordVo, UserDto>(userWithPassWordVo);
            userDto.UpdateTime = DateTime.Now;
            var result = await UserService.UpdateUserNameAndPassWord(Mapper.Map<UserWithPassWordVo, UserDto>(userWithPassWordVo), updateName, updatePassWord);
            if (!result)
            {
                throw new ArgumentException("请求失败");
            }
            else
            {
                return Ok();
            }
        }
    }
}