using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModel.User.Vo
{
    public class UserAccount
    {
        /// <summary>
        /// 用户名
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        /// <value></value>
        public string PassWord { get; set; }

        /// <summary>
        /// 用户类型 0未知 1管理员 2普通用户
        /// </summary>
        /// <example>2</example>
        public UserTypeEnum UserType { get; set; }
    }
}