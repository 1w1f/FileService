using DataModel.User;

namespace DataModel.File;

public class FileModel : ModelId
{

    public int UserId { get; set; }
    public DateTime CreateTime { get; set; }
    public bool IsDelete { get; set; }
    public UserDto User { get; set; }
    public string SavaPath { get; set; }

    /// <summary>文件大小单位为字节</summary>
    public long FileSize { get; set; }

}
