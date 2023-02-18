using AutoMapper;
using Manage.Models.DTO;
using Manage.Models.Entity;

namespace Manage.BusinessInterface.MapConfig
{
    /// <summary>
    /// AutoMapper映射类
    /// </summary>
    public class AutoMapperConfigs : Profile
    {
        /// <summary>
        /// 配置映射关系，在实例化当前这个类的时候，就要处理好
        /// </summary>
        public AutoMapperConfigs()
        {
            CreateMap<Sys_User, SysUserDTO>().ReverseMap();

            CreateMap<PagingData<Sys_User>, PagingData<SysUserDTO>>().ReverseMap();

        }
    }
}
