using System.Net;
namespace DataModel.User;
public class LoginRecordDto : ModelId
{
    public UserDto User { get; set; }
    public DateTime _loginTime;
    public string LoginTime => _loginTime.ToString("yyyy/MM/dd HH:mm:ss");
    private IPAddress _loginIp;
    public string LoginIp => _loginIp.ToString();

    public LoginRecordDto()
    {

    }
}