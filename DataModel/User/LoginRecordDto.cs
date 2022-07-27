using System.Net;
namespace DataModel.User;
public class LoginRecordDto : ModelId
{
    public int UserId { get; set; }

    public string LoginTime { get; set; }
    public string LoginIp { get; set; }

    public LoginRecordDto()
    {

    }
    public LoginRecordDto(IPAddress loginIp, DateTime loginTime, int userId)
    {
        UserId = userId;
        LoginIp = loginIp.ToString();
        LoginTime = loginTime.ToString("yyyy/MM/dd HH:mm:ss");
    }
}