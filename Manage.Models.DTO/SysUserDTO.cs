using Manage.Common.ValidateRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Models.DTO
{
    /// <summary>
    ///  用户信息
    /// </summary> 
    public class SysUserDTO : BaseDTO
    {
        public Guid Id { get; set; }


        [CustomRequiredAttribute("用户名称不能为空")]
        public string? Name { set; get; }

        public string? Password { set; get; }

        /// <summary>
        /// 用户状态  0正常 1冻结 2删除
        /// </summary>
        public int Status { set; get; }

        public string? Phone { set; get; }

        public string? Mobile { set; get; }

        [CustomRequiredAttribute("用户地址不为空")]
        public string? Address { set; get; }

        public string? Email { set; get; }

        [CustomValueIsNumAttribute("QQ号必须微数字")]
        public long QQ { set; get; }

        public string? WeChat { set; get; }

        public int Sex { set; get; }

        public DateTime LastLoginTime { set; get; }
    }
}
