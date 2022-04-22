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
    public class UserController : ControllerBase
    {
        protected IUserService UserService { get; set; }
        protected IMapper Mapper { get; set; }

        public UserController(IUserService userService, IMapper mapper)
        {
            UserService = userService;
            Mapper = mapper;
        }
        /// <summary>
        /// 创建用户 
        /// </summary>
        /// <param name="vo">uservo</param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ResultFilter]
        // [ApiResultFilterAttribute]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserVo vo)
        {
            return NotFound(new UserDto());
            // throw new ArgumentException(@"系统异常测试");
            // throw new ApiException(451,"异常接口返回测试");
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
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> FindUserIncludeLoginRecords()
        {
            return await UserService.FindAllAsync();
        }



        // public async Task<Action<UserD>>
    }
}