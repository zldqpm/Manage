using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BusinessInterface
{
    /// <summary>
    /// 每个Service 各自的接口  都是这个Service 内部的个性化实现
    /// </summary>
    public interface IUserService : IBaseService
    {
        public void DeleteComapnyUser();
    }
}
