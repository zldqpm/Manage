
using Manage.BusinessInterface;
using Manage.BusinessInterface.MapConfig;
using Manage.BusinessService;
using Manage.MentApi.Utility.InitDatabaseExt;
using Manage.MentApi.Utility.RegisterExt;
using Manage.MentApi.Utility.SwaggerExt;

namespace Manage.MentApi
{
    /// <summary>
    /// ��Ŀ����
    /// </summary>
    public class Program
    {
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            if (builder.Configuration["IsInitDatabase"] == "1")
            {
                //����SqlSugar--��ʼ�����ݿ�
                //��Ŀ�״�����--Ҫ��ʼ��
                builder.InitDatabase();
            }
            builder.InitSqlSugar(); //��ʼ��SqlSugar-ע�ᵽIOC����
            // Add services to the container.
            builder.CrosDomainsPolicy(); //���ÿ���
            builder.Services.AddControllers();
            //swgger����
            builder.AddSwaggerExt();
            builder.Services.AddTransient<IUserService, UserService>();
            //Automapperӳ��
            builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));
            var app = builder.Build();
            // �Ƿ�ֻ�ڿ�����������swggerUI ���������л�����������ʾ
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwaggerExt();
            //}
            app.UseCrosDomainsPolicy();//ʹ�ÿ������
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}