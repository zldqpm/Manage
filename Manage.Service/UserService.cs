using Manage.BusinessInterface;
using SqlSugar;

namespace Manage.BusinessService
{

    /// <summary>
    /// 这个方法中，可以有增伤改查
    /// 不仅限于这个方法中需要 其他的类 也需要--提出公共的代码--把大家都需要的来一个父类，子类直接继承即可
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="client"></param>
        public UserService(ISqlSugarClient client) : base(client)
        {

        }

        public void DeleteComapnyUser()
        {
            throw new NotImplementedException();
        }
    }
}