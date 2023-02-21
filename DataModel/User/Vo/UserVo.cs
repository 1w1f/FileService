using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataModel.User.Vo;

public class UserVo
{
    /// <summary>
    /// 用户Id
    /// </summary>
    /// <example>08db13bd-e2a7-4074-85d8-43e8eefb0658</example>
    public string Id { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    /// <example>zhangsan</example>
    public string Name { get; set; }

}
