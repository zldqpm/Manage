using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.ValidateRules
{
    /// <summary>
    /// 验证是否为数字
    /// </summary>
    public class CustomValueIsNumAttribute : BaseAbstractAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messge"></param>
        public CustomValueIsNumAttribute(string? messge) : base(messge)
        {
        }

        public override (bool, string?) DoValidate(object oValue)
        {
            return (true, "");
        }
    }
}
