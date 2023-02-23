using System.ComponentModel;

namespace DataModel.User
{
    public enum UserTypeEnum
    {
        [Description("未知身份")]
        None,
        [Description("管理员")]
        MANAGER,
        [Description("用户")]
        USER
    }
}