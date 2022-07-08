using System.Text.Json.Serialization;

namespace DataModel.User.Vo;

public class UserVo
{
    /// <summary>
    /// 用户Id
    /// </summary>
    /// <example>001</example>
    public string Id { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    /// <example>zhangsan</example>
    public string Name { get; set; }
    /// <summary>
    /// 用户密码
    /// </summary>
    /// <example>123456</example>
    [JsonIgnore]
    public string PassWord { get; set; }
}
