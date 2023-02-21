using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel.File;

public class FileEntity : ModelId
{
    [Description("创建时间")]
    public DateTime CreateTime { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string SavaPath { get; set; }
    [Description("文件大小，单位为字节")]
    public long FileSize { get; set; }
    [Description("oss存储中的文件名")]
    public string OssFileName { get; set; }
    [Description("文件名")]
    public string FileName { get; set; }

    public FileEntity()
    {

    }
    public FileEntity(string fileName, string ossFileName, DateTime createTime, string savaPath, long fileSize)
    {
        this.CreateTime = createTime;
        this.SavaPath = savaPath;
        this.FileSize = fileSize;
        this.FileName = fileName;
        this.OssFileName = ossFileName;
    }
}
