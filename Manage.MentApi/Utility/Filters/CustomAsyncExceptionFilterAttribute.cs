using Manage.Common.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Manage.MentApi.Utility.Filters
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class CustomAsyncExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                context.Result = new JsonResult(new ApiDataResult<string> { Success = false, Message = context.Exception.Message });
            }
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
