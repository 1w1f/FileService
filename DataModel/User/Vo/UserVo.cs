using System.ComponentModel.DataAnnotations;
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

}
