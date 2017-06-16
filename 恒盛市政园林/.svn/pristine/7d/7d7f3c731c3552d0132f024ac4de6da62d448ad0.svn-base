using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WallE.Data.MySql
{
    /// <summary>
    /// 记录Id管理器
    /// </summary>
    [Export(typeof(IRecordIdManager))]
    internal class RecordIdManager:IRecordIdManager
    {
        private readonly List<RecordIdModel> _recordIdList = new List<RecordIdModel>();

        public async Task<Result> GenerateIdAsync(string connStr, string tableName)
        {
            return await GenerateIdAsync(connStr, tableName, 1);
        }

        public async Task<Result> GenerateIdAsync(string connStr, string tableName, int totalCount)
        {
            var curRecordId = 2;
            var connStrBuilder = new SqlConnectionStringBuilder(connStr);
            var dbName = connStrBuilder.InitialCatalog;
            var curRecord = _recordIdList.FirstOrDefault(t => t.DbName == dbName &&
                                                              t.TableName == tableName);
            if (curRecord != null)
            {
                curRecordId = curRecord.CurRecordId;
                curRecord.CurRecordId += totalCount;
            }
            else
            {
                var dbAccesssor = M.DbAccessorMgnt.GetDbAccessor(connStr);
                var keyList = await dbAccesssor.GetPrimaryKeysAsync(tableName);
                if (keyList.Count != 1)
                    return new Result(new Exception("主键不唯一，无法进行此操作！"));
                var keyName = keyList[0].Name;
                var sqlStr = string.Format("SELECT MAX({0}) FROM {1}", keyName, tableName);
                var result = await dbAccesssor.QueryOneValueAsync(sqlStr);
                if (result.ResultType != ResultType.Ok)
                    return result;
                if (result.Data != null && !(result.Data is DBNull))
                    curRecordId = Convert.ToInt32(result.Data) + 1;
                _recordIdList.Add(new RecordIdModel
                {
                    DbName = dbName,
                    TableName = tableName,
                    CurRecordId = curRecordId + totalCount
                });
            }
            return new Result(curRecordId);
        }
    }
}
