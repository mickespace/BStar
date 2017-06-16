using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace WallE.Data.MySql
{
    public interface IDataAccessor
    {
        #region 查询数据
        /// <summary>
        /// 获取表的所有数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="orderFilter"></param>
        /// <returns></returns>
        Task<List<DataRecord>> AllAsync(string tableName, IDictionary<string, bool> orderFilter = null);

        /// <summary>
        /// 根据字典中的条件查询数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnFilter"></param>
        /// <param name="orderFilter"></param>
        /// <returns></returns>
        Task<List<DataRecord>> QueryAsync(string tableName, IDictionary<string, object> columnFilter, IDictionary<string, bool> orderFilter = null);

        /// <summary>
        /// 根据某个属性值查询数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <param name="orderFilter"></param>
        /// <returns></returns>
        Task<List<DataRecord>> QueryAsync(string tableName, string columnName, object value, IDictionary<string, bool> orderFilter = null);

        /// <summary>
        /// 查询某列与集合中的值相等的记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="contains"></param>
        /// <param name="orderFilter"></param>
        /// <returns></returns>
        Task<List<DataRecord>> QueryAsync(string tableName, string columnName, IEnumerable<object> contains, IDictionary<string, bool> orderFilter = null);

        /// <summary>
        /// 根据sql语句查询数据
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        Task<List<DataRecord>> QueryAsync(string sqlStr);

        /// <summary>
        /// 执行查询单个值的sql语句
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        Task<Result> QueryOneValueAsync(string sqlStr);

        /// <summary>
        /// 根据主键查找
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="keyValue">主键值</param>
        /// <returns>Task&lt;DataRecord&gt;.</returns>
        Task<DataRecord> FindByKeyAsync(string tableName, object keyValue);
        #endregion

        #region 保存数据

        /// <summary>
        /// 保存多张表的数据
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        Task<Result> SaveAsync(IEnumerable<TableStatus> tables);

        /// <summary>
        /// 保存单个表的数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<Result> SaveAsync(TableStatus table);

        /// <summary>
        /// 往表中插入单条数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        Task<Result> InsertAsync(string tableName, DataRecord dataRecord);

        /// <summary>
        /// 更新某条记录数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        Task<Result> UpdatetAsync(string tableName, DataRecord dataRecord);

        /// <summary>
        /// 删除某条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataRecord"></param>
        /// <returns></returns>
        Task<Result> DeleteAsync(string tableName, DataRecord dataRecord);

        /// <summary>
        /// 删除对应值的所有记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string tableName, string columnName, object value);

        /// <summary>
        /// 使用事务保存sql语句
        /// </summary>
        /// <param name="sqlStrList"></param>
        /// <returns></returns>
        Task<Result> SaveDataWithTransactionAsync(List<string> sqlStrList);

        #endregion

        #region 扩展方法

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        string GetConnStr();

        /// <summary>
        /// 获取表的主键信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<List<PrimaryKey>> GetPrimaryKeysAsync(string tableName);

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sqlStr">The SQL string.</param>
        /// <param name="converterFunc">The converter function.</param>
        /// <returns>List&lt;T&gt;.</returns>
        List<T> ExcuteSqlReader<T>(string sqlStr, Func<IDataRecord, T> converterFunc);

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sqlStr">The SQL string.</param>
        /// <returns>List&lt;T&gt;.</returns>
        bool ExecuteSqlNonQuery(string sqlStr);

        /// <summary>
        /// 异步执行sql语句
        /// </summary>
        /// <param name="cmdList"></param>
        /// <returns></returns>
        Task<Result> ExcuteSqlWithTransactionAsync(List<string> cmdList);

        /// <summary>
        /// 读取单个数据
        /// </summary>
        /// <param name="sqlStr">The SQL string.</param>
        /// <returns>List&lt;T&gt;.</returns>
        Result ExecuteScalar(string sqlStr);

        #endregion

        #region 创建、删除数据库及执行sql文件

        /// <summary>
        /// 执行sql文件
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<Result> ExecuteSqlFileAsync(Stream stream);

        /// <summary>
        /// 异步创建数据库
        /// </summary>
        /// <param name="dbName">数据库名</param>
        /// <returns></returns>
        Task<Result> CreateDatabaseAsync(string dbName);

        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        void DeleteDatabase(string dbName);

        #endregion
    }
}
