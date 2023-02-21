using System.Net;
namespace DataModel.User;
public class LoginRecordDto : ModelId
{
    public UserDto User { get; set; }
    public DateTime LoginTime { get; set; }
    public IPAddress LoginIp { get; set; }

    public LoginRecordDto()
    {

    }
    public LoginRecordDto(IPAddress loginIp, DateTime loginTime, UserDto user)
    {
        LoginIp = loginIp;
        LoginTime = loginTime;
        User = user;
    }
}