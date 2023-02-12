
using Manage.MentApi.Utility.InitDatabaseExt;
using Manage.MentApi.Utility.SwaggerExt;

namespace Manage.MentApi
{
    /// <summary>
    /// 项目启动
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            if (builder.Configuration["IsInitDatabase"] == "1")
            {
                //配置SqlSugar--初始化数据库
                //项目首次启动--要初始化
                builder.InitDatabase();
            }
            builder.InitSqlSugar(); //初始化SqlSugar-注册到IOC容器
            // Add services to the container.
            builder.Services.AddControllers();
            //swgger配置
            builder.AddSwaggerExt();

            var app = builder.Build();
            // 是否只在开发环境启动swggerUI 这里是所有环境都可以显示
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwaggerExt();
            //}

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}