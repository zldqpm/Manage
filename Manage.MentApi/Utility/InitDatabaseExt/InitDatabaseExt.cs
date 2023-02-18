using Manage.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System.Reflection;

namespace Manage.MentApi.Utility.InitDatabaseExt
{
    /// <summary>
    /// 是否初始化数据库
    /// </summary>
    public static class InitDatabaseExt
    {
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="builder"></param>
        public static void InitDatabase(this WebApplicationBuilder builder)
        {
            //保存在系统中获取的功能数据---有页面功能的，--按钮功能
            List<Sys_Menu> MenuList = new List<Sys_Menu>();
            Assembly asm = Assembly.GetExecutingAssembly();
            IEnumerable<Type> controlleractionlist = asm.GetTypes()
                    .Where(type => typeof(ControllerBase)
                    .IsAssignableFrom(type));

            foreach (var controller in controlleractionlist)
            {
                if (controller.IsDefined(typeof(FunctionAttribute), true))
                {
                    FunctionAttribute? attribute = controller.GetCustomAttribute<FunctionAttribute>();
                    Guid guid = Guid.NewGuid();
                    Sys_Menu sysMenu = new Sys_Menu()
                    {
                        Id = guid,
                        ParentId = default,
                        MenuText = attribute?.GetDescription(),
                        MenuType = attribute is null ? (int)MuType.Page : (int)attribute.GetMuType()
                    };
                    MenuList.Add(sysMenu);
                    var mehtodlist = controller.GetMethods()
                       .Where(m => m.IsDefined(typeof(FunctionAttribute), true));
                    foreach (var method in mehtodlist)
                    {
                        FunctionAttribute? childAttribute = method.GetCustomAttribute<FunctionAttribute>();
                        Sys_Menu childMenu = new Sys_Menu()
                        {
                            Id = Guid.NewGuid(),
                            ParentId = guid,
                            FullName = $"{controller.FullName}.{method.Name}",
                            MenuText = childAttribute?.GetDescription(),
                            MenuType = childAttribute is null ? (int)MuType.Btn : (int)childAttribute.GetMuType(),
                            ControllerName = controller.Name.ToLower().Replace("controller", ""),
                            ActionName = method.Name.ToLower()
                        };
                        MenuList.Add(childMenu);
                    }
                }
            }

            //读取配置文件中的数据库链接字符串
            string? connectionString = builder.Configuration.GetConnectionString("ConnectionString");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("请配置数据库链接字符串~");
            }
            ConnectionConfig connection = new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            };
            using (SqlSugarClient client = new SqlSugarClient(connection))
            {
                client.DbMaintenance.CreateDatabase();
                Assembly assembly = Assembly.LoadFile(Path.Combine(AppContext.BaseDirectory, "Manage.Models.dll"));

                Type[] typeArray = assembly.GetTypes().Where(t => !t.Name.Equals("Sys_BaseModel") && t.Namespace.Equals("Manage.Models.Entity"))
                    .ToArray();

                client.CodeFirst.InitTables(typeArray);
                client.Insertable(MenuList).ExecuteCommand();
            }
        }
    }
}
