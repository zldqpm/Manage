using Manage.BusinessInterface;
using Manage.MentApi.Utility.SwaggerExt;
using Manage.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Manage.MentApi.Controllers.SystemApi
{
    /// <summary>
    ///用户管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V1))] //指定版本
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 用户信息的分页查询
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="pageindex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchaString"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{pageindex:int}/{pageSize:int}")]
        public async Task<JsonResult> GetUserPage([FromServices] IUserService userService, int pageindex, int pageSize, string? searchaString = null)
        {
            Expressionable<Sys_User> expressionable = new Expressionable<Sys_User>();
            expressionable.AndIF(!string.IsNullOrWhiteSpace(searchaString), u => u.Name.Contains(searchaString));
            PagingData<Sys_User> paging = userService.QueryPage<Sys_User>(expressionable.ToExpression(), pageSize, pageindex, c => c.UserId, false);
            return new JsonResult(paging);
        }

    }
}
