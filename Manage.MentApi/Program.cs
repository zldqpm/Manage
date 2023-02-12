
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