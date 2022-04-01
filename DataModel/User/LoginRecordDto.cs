using System.Net;
namespace DataModel.User;
public class LoginRecordDto
{
    public int LoginRecordId { get; set; }
    public int UserId { get; set; }
    public DateTime LoginTime { get; set; }
    public IPAddress LoginIp { get; set; }
}