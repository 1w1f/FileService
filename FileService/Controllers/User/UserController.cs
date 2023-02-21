using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DataModel.User;
using DataModel.User.Vo;
using FileService.Common;
using FileService.Service.IService;
using FileServiceApi.Service.Service.LoginRecord.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FileService.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    [Authorize]
    [Produces("application/json")]
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
        [AllowAnonymous]
        public async Task<ActionResult<UserVo>> CreateUser([FromBody] UserAccount vo)
        {
            if (string.IsNullOrEmpty(vo.Name) || string.IsNullOrEmpty(vo.PassWord))
            {
                throw new BusinessException(4001, "用户名或密码不能为空");
            }
            var user = Mapper.Map<UserAccount, UserDto>(vo);
            user.CreateTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
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
                await LoginRecordService.CreateAsync(new LoginRecordDto(clientIp, DateTime.Now, user));


                var claims = new Claim[]
               {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim("UserId",user.Id.ToString()),
                    new Claim("test",user.Name),
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
                user.ExpirationTime = DateTime.Now.AddHours(1).ToString("yyyy-MM-dd HH-mm-ss");


                UserVoWithToken userVoWithToken = Mapper.Map<UserVoWithToken>(user);
                return userVoWithToken;
            }
            else
            {
                return Ok(new { code = 100, mesage = "222" });
                throw new BusinessException(4011, "用户名密码登录失败");
            }
        }






        /// <summary>
        /// 更新用户的
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