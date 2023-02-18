using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.ValidateRules
{
    public abstract class BaseAbstractAttribute : Attribute
    {
        public BaseAbstractAttribute(string? messge)
        {
            this.Message = messge;
        }

        protected string? Message { get; set; }

        public abstract (bool, string?) DoValidate(object oValue);
    }
}
