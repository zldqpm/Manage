using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Models.Entity
{
    /// <summary>
    ///  用户信息
    /// </summary>
    [SugarTable("Sys_User")]
    public class Sys_User
    {
        [SugarColumn(ColumnName = "UserId", IsIdentity = true, IsPrimaryKey = true)]
        public int UserId { get; set; }

        public string? Name { set; get; }

        public string? Password { set; get; }

        /// <summary>
        /// 用户状态  0正常 1冻结 2删除
        /// </summary>
        public int Status { set; get; }

        public string? Phone { set; get; }

        public string? Mobile { set; get; }

        public string? Address { set; get; }

        public string? Email { set; get; }

        public long QQ { set; get; }

        public string? WeChat { set; get; }

        public int Sex { set; get; }

        public DateTime LastLoginTime { set; get; }
    }
}
