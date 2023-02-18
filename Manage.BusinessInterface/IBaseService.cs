using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Manage.BusinessInterface
{
    /// <summary>
    /// 提供的就是业务逻辑的基础实现
    /// 提供的公共逻辑--通用的增删改查
    /// </summary>
    public interface IBaseService
    {
        #region Query
        /// <summary>
        /// 主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Find<T>(int id) where T : class;

        /// <summary>
        /// 主键查询-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync<T>(int id) where T : class;

        /// <summary>
        /// 提供对单表的查询
        /// </summary>
        /// <returns>ISugarQueryable类型集合</returns>
        [Obsolete("尽量避免使用，using 带表达式目录树的 代替")]
        ISugarQueryable<T> Set<T>() where T : class;

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        ISugarQueryable<T> Query<T>(Expression<Func<T, bool>> funcWhere) where T : class;

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
        PagingData<T> QueryPage<T>(Expression<Func<T, bool>> funcWhere, int pageSize, int pageIndex, Expression<Func<T, object>> funcOrderby, bool isAsc = true) where T : class;
        #endregion

        #region Add

        /// <summary>
        /// 新增数据-同步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        T Insert<T>(T t) where T : class, new();

        /// <summary>
        /// 新增数据-异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<T> InsertAsync<T>(T t) where T : class, new();

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tList"></param>
        /// <returns></returns>
        Task<bool> InsertList<T>(List<T> tList) where T : class, new();
        #endregion


        #region Update
        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        void Update<T>(T t) where T : class, new();


        /// <summary>
        /// 更新数据，异步版本
        /// </summary>
        /// <param name="t"></param>
        Task UpdateAsync<T>(T t) where T : class, new();

        /// <summary>
        /// 更新数据，即时Commit
        /// </summary>
        /// <param name="tList"></param>
        void Update<T>(List<T> tList) where T : class, new();
        #endregion


        #region Delete
        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pId"></param>
        /// <returns></returns>
        bool Delete<T>(object pId) where T : class, new();

        /// <summary>
        /// 根据主键删除数据--异步版本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pId"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(object pId) where T : class, new();

        /// <su+mary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <param name="t"></param>
        void Delete<T>(T t) where T : class, new();

        /// <summary>
        /// 删除数据，即时Commit
        /// </summary>
        /// <param name="tList"></param>
        void Delete<T>(List<T> tList) where T : class;
        #endregion


        #region Other

        /// <summary>
        /// 执行sql 返回集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        ISugarQueryable<T> ExcuteQuery<T>(string sql) where T : class, new();

        #endregion

    }
}
