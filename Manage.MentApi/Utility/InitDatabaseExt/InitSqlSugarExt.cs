using SqlSugar;
using System.Reflection;

namespace Manage.MentApi.Utility.InitDatabaseExt
{
    /// <summary>
    /// 初始化SqlSugar
    /// </summary>
    public static class InitSqlSugarExt
    {
        /// <summary>
        /// 初始化SqlSugar
        /// </summary>
        /// <param name="builder"></param>
        public static void InitSqlSugar(this WebApplicationBuilder builder)
        {
            string? connectionString = builder.Configuration.GetConnectionString("ConnectionString");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("请配置数据库链接字符串~");
            }

            ConnectionConfig connection = new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute
            };


            builder.Services.AddScoped<ISqlSugarClient>(s =>
            {
                SqlSugarClient client = new SqlSugarClient(connection);
                client.Aop.OnLogExecuting = (s, p) =>
                {
                    Console.WriteLine($"OnLogExecuting:输出Sql语句:{s} || 参数为：{string.Join(",", p.Select(p => p.Value))}");
                };
                client.Aop.OnExecutingChangeSql = (s, p) =>
                {
                    Console.WriteLine($"OnLogExecuting:输出Sql语句:{s} || 参数为：{string.Join(",", p.Select(p => p.Value))}");
                    return new KeyValuePair<string, SugarParameter[]>(s, p);
                };
                client.Aop.OnLogExecuted = (s, p) =>
                {
                    Console.WriteLine($"OnLogExecuted:输出Sql语句:{s} || 参数为：{string.Join(",", p.Select(p => p.Value))}");
                };
                client.Aop.OnError = e =>
                {
                    Console.WriteLine($"OnError:Sql语句执行异常:{e.Message}");
                };

                {
                    //可以配置对于数据库操作的过滤器--SqlSugar特有的
                }
                return client;
            });



        }
    }
}
