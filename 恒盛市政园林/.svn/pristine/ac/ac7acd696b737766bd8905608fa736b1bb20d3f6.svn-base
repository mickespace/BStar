using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WallE.Data.MySql
{
    internal class DbAccessorMgnt : IDataAccessorMgnt
    {
        private static readonly Dictionary<string, IDataAccessor> DbAccessorDic = new Dictionary<string, IDataAccessor>();

        /// <summary>
        /// 根据连接字符串获取Accessor
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        IDataAccessor IDataAccessorMgnt.GetDbAccessor(string connStr)
        {
            return GetDbAccessor(connStr);
        }

        public IDataAccessor GetDbAccessorByProjectItemKey(string projectItemKey)
        {
            if (string.IsNullOrEmpty(projectItemKey))
                projectItemKey = Guid.Empty.ToString();
            var connStr = M.ConnManager.GetConnString(projectItemKey);
            if (string.IsNullOrEmpty(connStr))
                throw new ArgumentException($"子项目的Key：{projectItemKey}无效！", nameof(projectItemKey));
            var dbAccessor = GetDbAccessor(connStr);
            return dbAccessor;
        }

        public IDataAccessor GetMgntDbAccessor()
        {
            var connStr = M.ConnManager.GetConnString(Guid.Empty.ToString());
            return GetDbAccessor(connStr);
        }

        private static IDataAccessor GetDbAccessor(string connStr)
        {
            var connStrBuilder = new SqlConnectionStringBuilder(connStr);
            var key = $"{connStrBuilder.DataSource}-{connStrBuilder.InitialCatalog}";
            if (DbAccessorDic.ContainsKey(key))
                return DbAccessorDic[key];
            var dbAccessor = new DbAccessor(connStr);
            DbAccessorDic[key] = dbAccessor;
            return dbAccessor;
        }
    }
}
