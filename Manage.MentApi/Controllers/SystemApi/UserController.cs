using AutoMapper;
using Manage.BusinessInterface;
using Manage.Common.Result;
using Manage.MentApi.Utility.Filters;
using Manage.MentApi.Utility.InitDatabaseExt;
using Manage.MentApi.Utility.SwaggerExt;
using Manage.Models.DTO;
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
    [CustomAsyncExceptionFilter]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V1))] //指定版本
    [FunctionAttribute(MuType.Page, "用户管理")]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///  用户信息的分页查询
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="mapper"></param>
        /// <param name="pageindex"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchaString"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{pageindex:int}/{pageSize:int}/{searchaString}")]
        [Route("{pageindex:int}/{pageSize:int}")]
        [FunctionAttribute(MuType.Btn, "查询用户")]
        public async Task<JsonResult> GetUserPage([FromServices] IUserService userService, [FromServices] IMapper mapper, int pageindex, int pageSize, string? searchaString = null)
        {
            Expressionable<Sys_User> expressionable = new Expressionable<Sys_User>();
            expressionable.AndIF(!string.IsNullOrWhiteSpace(searchaString), u => u.Name.Contains(searchaString));
            PagingData<Sys_User> paging = userService.QueryPage<Sys_User>(expressionable.ToExpression(), pageSize, pageindex, c => c.CreateTime, false);

            ApiDataResult<PagingData<SysUserDTO>> result = new ApiDataResult<PagingData<SysUserDTO>>()
            {
                Data = mapper.Map<PagingData<Sys_User>, PagingData<SysUserDTO>>(paging),
                Success = true,
                Message = "用户分页查询"
            };
            return await Task.FromResult(new JsonResult(result));
        }
        //还有可能只需要返回一个状态即可


        /// <summary>
        /// 【添加用户】
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="mapper"></param>
        /// <param name="userDTO">用户信息</param> 
        [HttpPost]
        [CustomAsyncValidateParaActionFilterAttribute]
        public async Task<JsonResult> AddUser([FromServices] IUserService userService, [FromServices] IMapper mapper, SysUserDTO userDTO)
        {
            //对于数据在添加之前
            //参数的校验
            //if (true)
            //{ 
            //}

            //可以通过actionFilter扩展

            Sys_User adduser = mapper.Map<SysUserDTO, Sys_User>(userDTO);
            //adduser.Password = MD5Encrypt.Encrypt(adduser.Password);
            userService.Insert(adduser);
            var result = new JsonResult(new ApiDataResult<Sys_User>() { Data = adduser, Success = true, Message = "添加用户" });
            return await Task.FromResult(result);
        }

    }
}
