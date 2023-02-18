using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Common.Result
{
    public class ApiResult
    {
        /// <summary>
        /// 是否正常返回
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 处理消息
        /// </summary>
        public string? Message { get; set; }
    }


    public class ApiDataResult<T> : ApiResult where T : class
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// 冗余结果
        /// </summary>
        public object? OValue { get; set; }
    }
}
