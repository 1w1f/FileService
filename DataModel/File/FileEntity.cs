using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.File;

public class FileEntity : ModelId
{
    [Description("创建时间")]
    public DateTime CreateTime { get; set; }
    [Description("是否删除")]
    public bool IsDelete { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string SavaPath { get; set; }
    [Description("文件大小，单位为字节")]
    public long FileSize { get; set; }
}
