using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Manage.BusinessInterface;

namespace Manage.BusinessService
{


    /// <summary>
    /// 提供的就是业务逻辑的基础实现
    /// 提供的公共逻辑--最好不要直接使用，通过继承来调用
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        /// <summary>
        /// 链接数据库操作专用的SqlSugar
        /// </summary>
        protected ISqlSugarClient _Client { get; set; }

        public BaseService(ISqlSugarClient client)
        {
            _Client = client;
        }

        #region Query

        /// <summary>
        /// 主键查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find<T>(int id) where T : class
        {
            return _Client.Queryable<T>().InSingle(id);
        }

        /// <summary>
        /// 主键查询-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> FindAsync<T>(int id) where T : class
        {
            return await _Client.Queryable<T>().InSingleAsync(id);
        }

        /// <summary>
        /// 不应该暴露给上端使用者，尽量少用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete("尽量避免使用，using 带表达式目录树的代替")]
        public ISugarQueryable<T> Set<T>() where T : class
        {
            return _Client.Queryable<T>();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        public ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class
        {
            return _Client.Queryable<T>().Where(funcWhere);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="funcOrderby"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public PagingData<T> QueryPage<T>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, object>> funcOrderby, bool isAsc = true) where T : class
        {
            var list = _Client.Queryable<T>();
            if (funcWhere != null)
            {
                list = list.Where(funcWhere);
            }
            list = list.OrderByIF(true, funcOrderby, isAsc ? OrderByType.Asc : OrderByType.Desc);
            PagingData<T> result = new PagingData<T>()
            {
                DataList = list.ToPageList(pageIndex, pageSize),
                PageIndex = pageIndex,
                PageSize = pageSize,
                RecordCount = list.Count(),
            };
            return result;
        }
        #endregion


        #region Insert

        /// <summary>
        /// 新增数据-同步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Insert<T>(T t) where T : class, new()
        {
            return _Client.Insertable(t).ExecuteReturnEntity();
        }

        /// <summary>
        /// 新增数据-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<T> InsertAsync<T>(T t) where T : class, new()
        {
            return await _Client.Insertable(t).ExecuteReturnEntityAsync();
        }

        /// <summary>
        /// 批量新增-事务执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns></returns>
        public async Task<bool> InsertList<T>(List<T> tList) where T : class, new()
        {
            return await _Client.Insertable(tList.ToList()).ExecuteCommandIdentityIntoEntityAsync();
        }
        #endregion

        #region Update
        /// <summary>
        /// 是没有实现查询，直接更新的,需要Attach和State
        /// 
        /// 如果是已经在context，只能再封装一个(在具体的service)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Update<T>(T t) where T : class, new()
        {
            if (t == null) throw new Exception("t is null");

            _Client.Updateable(t).ExecuteCommand();
        }

        /// <summary>
        /// 更新数据，异步版本
        /// </summary>
        /// <param name="t"></param>
        public async Task UpdateAsync<T>(T t) where T : class, new()
        {
            if (t == null) throw new Exception("t is null");
            await _Client.Updateable(t).ExecuteCommandAsync();
        }

        public void Update<T>(List<T> tList) where T : class, new()
        {
            _Client.Updateable(tList).ExecuteCommand();
        }

        #endregion

        #region Delete
        /// <summary>
        /// 先附加 再删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Delete<T>(T t) where T : class, new()
        {
            _Client.Deleteable(t).ExecuteCommand();
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pId"></param>
        /// <returns></returns>
        public bool Delete<T>(object pId) where T : class, new()
        {
            T t = _Client.Queryable<T>().InSingle(pId);
            return _Client.Deleteable(t).ExecuteCommandHasChange();
        }


        /// <summary>
        /// 根据主键删除数据--异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<T>(object pId) where T : class, new()
        {
            T t = _Client.Queryable<T>().InSingle(pId);
            return await _Client.Deleteable(t).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        public void Delete<T>(List<T> tList) where T : class
        {
            _Client.Deleteable(tList).ExecuteCommand();
        }
        #endregion

        #region Other 
        ISugarQueryable<T> IBaseService.ExcuteQuery<T>(string sql) where T : class
        {
            return _Client.SqlQueryable<T>(sql);
        }
        public void Dispose()
        {
            if (_Client != null)
            {
                _Client.Dispose();
            }
        }
        #endregion



    }
}
