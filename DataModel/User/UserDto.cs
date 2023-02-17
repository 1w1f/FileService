using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DataModel.User;
public class UserDto : ModelId
{
    [Column(name: "UserName", TypeName = "nvarchar(50)")]
    [StringLength(20)]
    public string Name { get; set; }
    [Required]
    [StringLength(100)]
    public string PassWord { get; set; }

    [Column(TypeName = "varchar(50)")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 更新时间
    /// </summary>
    /// <value></value>
    [Column(TypeName = "nvarchar(50)")]
    public DateTime UpdateTime { get; set; }

    public virtual List<LoginRecordDto> LoginRecords { get; set; } = new List<LoginRecordDto>();

    /// <summary>
    /// 用户凭证
    /// </summary>
    /// <value></value>
    [NotMapped]
    public string Token { get; set; }
    /// <summary>
    /// 有效期
    /// </summary>
    /// <value>string</value>
    [NotMapped]
    public string ExpirationTime { get; set; }
}
