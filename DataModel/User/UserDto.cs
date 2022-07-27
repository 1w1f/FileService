using System.ComponentModel.DataAnnotations;
namespace DataModel.User;
public class UserDto : ModelId
{
    public string Name { get; set; }
    public string PassWord { get; set; }
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    /// <value></value>
    public DateTime UpdateTime { get; set; }

    public virtual List<LoginRecordDto> LoginRecords { get; set; } = new List<LoginRecordDto>();

}
