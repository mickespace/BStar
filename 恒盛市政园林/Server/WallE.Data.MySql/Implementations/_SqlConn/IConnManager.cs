using System.Collections.Generic;

namespace WallE.Data.MySql
{
    public interface IConnManager
    {
        /// <summary>
        /// 根据Key获取连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetConnString(string key);

        /// <summary>
        /// 添加连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connectionString"></param>
        void AddConnString(string key, string connectionString);

        /// <summary>
        /// 移除连接字符串
        /// </summary>
        /// <param name="key"></param>
        void RemoveConnString(string key);

        /// <summary>
        /// 获取Master连接字符串
        /// </summary>
        /// <returns></returns>
        string GetMasterConnString();

        /// <summary>
        /// 获取管理数据库名称
        /// </summary>
        /// <returns></returns>
        string GetManagementDbName();

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetDbName(string key);

        /// <summary>
        /// 获取所有连接字符串
        /// </summary>
        /// <returns></returns>
        Dictionary<string, string> GetConnectionInfo();
    }
}
