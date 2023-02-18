using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.ValidateRules
{
    /// <summary>
    /// 验证是否必填
    /// </summary>
    public class CustomRequiredAttribute : BaseAbstractAttribute
    {

        public CustomRequiredAttribute(string? messge) : base(messge) { }

        public override (bool, string?) DoValidate(object oValue)
        {
            return oValue == null ? (false, Message) : (true, string.Empty);
        }

    }
}
