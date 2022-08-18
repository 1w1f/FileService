using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DataModel.User;
using DataModel.User.Vo;
using FileService.Service.IService;
using FileServiceApi.Common;
using FileServiceApi.Service.Service.LoginRecord.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace FileServiceApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    [Produces("application/json")]
    [Authorize]
    public class UserController : ControllerBase
    {
        protected IUserService UserService { get; set; }
        protected IMapper Mapper { get; set; }

        protected ILoginRecordService LoginRecordService { get; set; }
        private ILogger<UserController> _logger
        { get; set; }


        private IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper, ILoginRecordService loginRecordService, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
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
            if (string.IsNullOrEmpty(vo.Name) || string.IsNullOrEmpty(vo.PassWord))
            {
                throw new BusinessException(4001, "用户名或密码不能为空");
            }
            var user = Mapper.Map<UserVo, UserDto>(vo);
            user.CreateTime = new DateTime();
            var result = await UserService.CreateAsync(user);
            if (result != null)
            {
                return Mapper.Map<UserDto, UserVo>(result); ;
            }
            else
            {
                throw new BusinessException(4002, "创建失败");
            }
        }


        /// <summary>
        /// 获取所有User
        /// </summary>
        /// <returns></returns>
        [HttpGet("FindAllUsers")]
        public async Task<ActionResult<List<UserVo>>> FindUserIncludeLoginRecords()
        {
            var userDtoList = await UserService.FindAllAsync();
            return userDtoList.ConvertAll(new Converter<UserDto, UserVo>(userDto => Mapper.Map<UserDto, UserVo>(userDto)));
        }

        [AllowAnonymous]
        [HttpGet("Login/{Name}/{passWord}")]
        public async Task<ActionResult<UserVoWithToken>> Login(string Name, string passWord)
        {
            // throw new BusinessException(4401, "ceshi");
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


                var claims = new Claim[]
               {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim("Id",user.Id.ToString()),
                    new Claim("name",user.Name),
               };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthKey"]));
                var token = new JwtSecurityToken(
                    issuer: "http://192.168.1.102:5129",
                    audience: "http://192.168.1.102:5129",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                user.Token = jwtToken;
                user.ExpirationTime = DateTime.Now.AddHours(1).ToString("yyyy-mm-dd HH-mm-ss");


                var userVoWithToken = Mapper.Map<UserVoWithToken>(user);
                return userVoWithToken;
            }
            else
            {
                throw new BusinessException(4011, "用户名密码登录失败");
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