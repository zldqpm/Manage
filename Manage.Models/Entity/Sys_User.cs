﻿using SqlSugar;

namespace Manage.Models.Entity
{
    /// <summary>
    ///  用户信息
    /// </summary>
    [SugarTable("Sys_User")]
    public class Sys_User : Sys_BaseModel
    {
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
