using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BusinessInterface
{
    public class PagingData<T> where T : class
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// 当前第几页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页多少条
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页的数据集合
        /// </summary>
        public List<T>? DataList { get; set; }


        public string? SearchString { get; set; }
    }
}
