﻿using SqlSugar;

namespace Manage.Models.Entity
{
    /// <summary>
    ///和数据库对应实体---在数据库中保存功能菜单和按钮
    ///
    /// 功能菜单：也需要查询API
    ///     按钮：也需要查询API
    ///     
    /// 保存到数据库中去的所有的数据--都必须在我们的项目中有一个API
    /// 项目中有多少个功能---在数据库表Sys_Menu 就应该保存对应的功能数据
    /// 
    /// 可以在项目启动的时候，把项目中已经开发好的功能直接读取出来保存到数据库中去；
    /// 
    /// </summary>
    [SugarTable("Sys_Menu")]
    public class Sys_Menu : Sys_BaseModel
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        [SugarColumn(ColumnName = "ParentId")]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string? MenuText { get; set; }

        /// <summary>
        /// 全名称--多级菜单--  一级==二级菜单==三次
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string? FullName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string? ControllerName { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string? ActionName { get; set; }

        /// <summary>
        /// 菜单类型
        /// 0：菜单功能
        /// 1：按钮功能
        /// </summary>
        public int MenuType { get; set; }

        /// <summary>
        /// 递归类型
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Sys_Menu>? MenuChildList { get; set; }
    }
}
