// ***********************************************************************
// Assembly         : WallE.DAL.dll
// Author           : 
// Created          : 2015-11-30 10:03
// 
// Last Modified By : 郭华华
// Last Modified On : 2015-11-30 10:13
// ***********************************************************************
// <copyright file="DbAccessor.cs" company="深圳筑星科技有限公司">
//      Copyright (c) BStar All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WallE.Data.MySql
{
    internal class DbAccessor : IDataAccessor
    {
        /// <summary>
        /// 存放Table对应的键
        /// </summary>
        private readonly Dictionary<string, List<PrimaryKey>> _tableKeyDic = new Dictionary<string, List<PrimaryKey>>();
        private readonly string _conStr;

        public DbAccessor(string connStr)
        {
            _conStr = connStr;
        }

        public string GetConnStr()
        {
            return _conStr;
        }

        #region 查询数据

        public async Task<List<DataRecord>> AllAsync(string tableName, IDictionary<string, bool> orderFilter = null)
        {
            var sqlStr = string.Format("select * FROM {0}", tableName);
            var orderSql = orderFilter == null ? "" : orderFilter.Aggregate("", (t1, t2) => t1 == ""
                   ? string.Format(" ORDER BY {0} {1}", t2.Key, t2.Value ? "" : "DESC")
                   : string.Format("{0} , {1} {2}", t1, t2.Key, t2.Value ? "" : "DESC"));
            return await QueryAsync(sqlStr+ orderSql);
        }

        public async Task<List<DataRecord>> QueryAsync(string tableName, IDictionary<string, object> columnFilter, IDictionary<string, bool> orderFilter=null)
        {
            if (columnFilter.Count == 0)
                return new List<DataRecord>();
            var str1 = "select * FROM " + tableName + " where ";
            var str2 = columnFilter.Aggregate("", (t1, t2) => t1 == ""
                ? string.Format("{0}='{1}'", t2.Key, t2.Value)
                : string.Format("{0} and {1}='{2}'", t1, t2.Key, t2.Value));
            var orderSql = orderFilter == null ? "" : orderFilter.Aggregate("", (t1, t2) => t1 == ""
                   ? string.Format(" ORDER BY {0} {1}", t2.Key, t2.Value ? "" : "DESC")
                   : string.Format("{0} , {1} {2}", t1, t2.Key, t2.Value ? "" : "DESC"));
            return await QueryAsync(str1 + str2 + orderSql);
        }

        public async Task<List<DataRecord>> QueryAsync(string tableName, string columnName, object value, IDictionary<string, bool> orderFilter = null)
        {
            var sqlStr = string.Format("select * FROM {0} where {1} = '{2}'", tableName, columnName, value);
            var orderSql = orderFilter == null ? "" : orderFilter.Aggregate("", (t1, t2) => t1 == ""
                   ? string.Format(" ORDER BY {0} {1}", t2.Key, t2.Value ? "" : "DESC")
                   : string.Format("{0} , {1} {2}", t1, t2.Key, t2.Value ? "" : "DESC"));
            return await QueryAsync(sqlStr + orderSql);
        }

        public async Task<List<DataRecord>> QueryAsync(string tableName, string columnName, IEnumerable<object> contains, IDictionary<string, bool> orderFilter = null)
        {
            var inStr = contains.Aggregate("", (t1, t2) => t1 == "" ? "'" + t2 + "'" : string.Format("{0},'{1}'", t1, t2));
            if (string.IsNullOrEmpty(inStr))
                return new List<DataRecord>();
            var sqlStr = string.Format("SELECT * FROM {0} WHERE {1} IN ({2})", tableName, columnName, inStr);
            var orderSql = orderFilter == null ? "" : orderFilter.Aggregate("", (t1, t2) => t1 == ""
                   ? string.Format(" ORDER BY {0} {1}", t2.Key, t2.Value ? "" : "DESC")
                   : string.Format("{0} , {1} {2}", t1, t2.Key, t2.Value ? "" : "DESC"));
            return await QueryAsync(sqlStr + orderSql);
        }

        public async Task<List<DataRecord>> QueryAsync(string sqlStr)
        {
            var result = await Task.Run(() => ExcuteSqlReader(sqlStr, t => new DataRecord(t)));
            return result;
        }

        public async Task<Result> QueryOneValueAsync(string sqlStr)
        {
            var result = await Task.Run(() => ExecuteScalar(sqlStr));
            return result;
        }

        public async Task<DataRecord> FindByKeyAsync(string tableName, object keyValue)
        {
            var keys = await GetPrimaryKeysAsync(tableName);
            if (keys.Count != 1)
                throw new InvalidOperationException("主键不唯一，无法调用该方法！");
            var results = await QueryAsync(tableName, keys[0].Name, keyValue);
            return results.FirstOrDefault();
        }
        #endregion

        #region 保存数据

        public async Task<Result> SaveAsync(IEnumerable<TableStatus> tables)
        {
            var result = await Task.Run(() =>
            {
                var sqlConn = new MySqlConnection(_conStr);
                MySqlTransaction trans = null;
                var tsList = tables as IList<TableStatus> ?? tables.ToList();
                try
                {
                    sqlConn.Open();
                    var tuples = new List<Tuple<TableInfo, DataTable, DataTable>>();
                    foreach (var ts in tsList)
                    {
                        DataTable insertTable, otherTable;
                        if (!SqlDbUtil.GetChangedData(ts, sqlConn, out insertTable, out otherTable))
                            continue;
                        var tableInfo = SqlDbUtil.GetTableInfo(ts.TableName, sqlConn);
                        tuples.Add(Tuple.Create(tableInfo, insertTable, otherTable));
                    }

                    trans = sqlConn.BeginTransaction();
                    for (var i = 0; i < tuples.Count; i++)
                    {
                        var ti = tuples[i].Item1;
                        var insertTable = tuples[i].Item2;
                        var otherTable = tuples[i].Item3;
                        if (otherTable.Rows.Count > 0 || insertTable.Rows.Count > 0)
                        {
                            //采用SqlDataAdapter批量更新及删除数据
                            var cmd = sqlConn.CreateCommand();
                            cmd.CommandText = string.Format("select * from {0} where 0=1", ti.TableName);
                            cmd.Transaction = trans;
                            
                            var sda = new MySqlDataAdapter(cmd);
                            if (otherTable.Rows.Count > 0)
                            {
                                //var sb1 = new MySqlCommandBuilder(sda);
                                var updateCmd = SqlDbUtil.GetUpdateCommand(ti, sqlConn);
                                var deleteCmd = SqlDbUtil.GetDeleteCommand(ti, sqlConn);
                                updateCmd.Transaction = trans;
                                deleteCmd.Transaction = trans;
                                sda.UpdateCommand = updateCmd;
                                sda.DeleteCommand = deleteCmd;
                            }
                            if (insertTable.Rows.Count > 0 )
                            {
                                var insertCmd = SqlDbUtil.GetInsertCommand(ti, sqlConn);
                                insertCmd.Transaction = trans;
                                sda.InsertCommand = insertCmd;
                            }
                            try
                            {
                                if(otherTable.Rows.Count > 0)
                                    sda.Update(otherTable);
                                if (insertTable.Rows.Count > 0)
                                    sda.Update(insertTable);
                            }
                            catch (DBConcurrencyException)
                            {

                            }
                        }
                    }
                    trans.Commit();
                    return Result.Ok;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    return new Result(e);
                }
                finally
                {
                    sqlConn.Close();
                    if (trans != null)
                        trans.Dispose();
                }
            });
            return result;
        }

        public Task<Result> SaveAsync(TableStatus table)
        {
            return SaveAsync(new List<TableStatus> { table });
        }

        public async Task<Result> InsertAsync(string tableName, DataRecord dataRecord)
        {
            var tableStatus = new TableStatus(tableName)
            {
                AppendedRecords = { dataRecord }
            };
            return await SaveAsync(tableStatus);
        }

        public async Task<Result> UpdatetAsync(string tableName, DataRecord dataRecord)
        {
            var tableStatus = new TableStatus(tableName)
            {
                ModifiedRecords = { dataRecord }
            };
            return await SaveAsync(tableStatus);
        }

        public async Task<Result> DeleteAsync(string tableName, DataRecord dataRecord)
        {
            var tableStatus = new TableStatus(tableName)
            {
                DeletedRecords = { dataRecord }
            };
            return await SaveAsync(tableStatus);
        }

        public async Task<bool> DeleteAsync(string tableName, string columnName, object value)
        {
            var sqlStr = string.Format("Delete FROM {0} where {1} = '{2}'", tableName, columnName, value);
            return await Task.Run(() => ExecuteSqlNonQuery(sqlStr));
        }

        /// <summary>
        /// 使用事务保存sql语句
        /// </summary>
        /// <param name="sqlStrList"></param>
        /// <returns></returns>
        public async Task<Result> SaveDataWithTransactionAsync(List<string> sqlStrList)
        {
            return await Task.Run(() => ExcuteSqlWithTransactionAsync(sqlStrList));
        }

        #endregion

        #region 扩展方法

        public async Task<List<PrimaryKey>> GetPrimaryKeysAsync(string tableName)
        {
            if (_tableKeyDic.ContainsKey(tableName))
                return _tableKeyDic[tableName].ToList();
            var sqlConn = new MySqlConnection(_conStr);
            var talbeInfo = SqlDbUtil.GetTableInfo(tableName, sqlConn);
            var priKeys = SqlDbUtil.GetPrimaryKeys(talbeInfo);
            _tableKeyDic[tableName] = priKeys;
            return priKeys.ToList();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sqlStr">The SQL string.</param>
        /// <param name="converterFunc">The converter function.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> ExcuteSqlReader<T>(string sqlStr, Func<IDataRecord, T> converterFunc)
        {
            var sqlConnection = new MySqlConnection(_conStr);
            var sqlCommand = new MySqlCommand(sqlStr, sqlConnection);
            try
            {
                sqlConnection.Open();
                var sqlDataReader = sqlCommand.ExecuteReader();
                var list = new List<T>();
                while (sqlDataReader.Read())
                    list.Add(converterFunc(sqlDataReader));
                return list;
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex);
                return null;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sqlStr">The SQL string.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public bool ExecuteSqlNonQuery(string sqlStr)
        {
            var sqlConnection = new MySqlConnection(_conStr);
            var sqlCommand = new MySqlCommand(sqlStr, sqlConnection);
            try
            {
                sqlConnection.Open();
                var rowNum = sqlCommand.ExecuteNonQuery();
                //return rowNum != 0;
                return true;
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex);
                return false;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public async Task<Result> ExcuteSqlWithTransactionAsync(List<string> cmdList)
        {
            var result = await Task.Run(() =>
            {
                var sqlConn = new MySqlConnection(_conStr);
                MySqlTransaction trans = null;
                try
                {
                    sqlConn.Open();
                    trans = sqlConn.BeginTransaction();
                    var cmd = sqlConn.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.Transaction = trans;
                    foreach (var cmdText in cmdList)
                    {
                        cmd.CommandText = cmdText;
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return Result.Ok;
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    return new Result(e);
                }
                finally
                {
                    sqlConn.Close();
                    if (trans != null)
                        trans.Dispose();
                }
            });
            return result;
        }

        /// <summary>
        /// 读取单个数据
        /// </summary>
        /// <param name="sqlStr">The SQL string.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public Result ExecuteScalar(string sqlStr)
        {
            var sqlConnection = new MySqlConnection(_conStr);
            var sqlCommand = new MySqlCommand(sqlStr, sqlConnection);
            try
            {
                sqlConnection.Open();
                var curObject = sqlCommand.ExecuteScalar();
                return new Result(curObject);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex);
                return new Result(ex);
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        #endregion

        #region 创建、删除数据库及执行sql文件

        public async Task<Result> ExecuteSqlFileAsync(Stream stream)
        {
            if (stream == null)
                return Result.Nochange;
            //处理sql文件
            var sqlList = new List<string>();
            var cmdStr = new StringBuilder();
            var reader = new StreamReader(stream, Encoding.UTF8);
            while (reader.Peek() > -1)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                    continue;
                if (line.Contains("DROP TABLE"))
                {
                    var str = cmdStr.ToString();
                    cmdStr.Clear();
                    if (!string.IsNullOrEmpty(str) && !str.Contains("DROP TABLE"))
                        sqlList.Add(str);
                }
                else if (line.Contains("CREATE TABLE"))
                {
                    var str = cmdStr.ToString();
                    cmdStr.Clear();
                    if (!string.IsNullOrEmpty(str) && !str.Contains("DROP TABLE"))
                        sqlList.Add(str);
                    cmdStr.AppendLine(line);
                }
                else
                    cmdStr.AppendLine(line);
            }
            if (!cmdStr.ToString().Contains("DROP TABLE"))
                sqlList.Add(cmdStr.ToString());
            reader.Close();
            //采用事务执行sql语句
            MySqlConnection conn = null;
            MySqlTransaction trans = null;
            try
            {
                conn = new MySqlConnection(_conStr);
                await conn.OpenAsync();

                var cmd = new MySqlCommand { Connection = conn };

                trans = conn.BeginTransaction();
                cmd.Transaction = trans;

                foreach (var sql in sqlList)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                await Task.Run(() => trans.Commit());
            }
            catch (Exception e)
            {
                trans.Rollback();
                return new Result(e);
            }
            finally
            {
                conn.Close();
            }
            return Result.Ok;
        }

        public async Task<Result> CreateDatabaseAsync(string dbName)
        {
            //创建数据库
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(_conStr);
                var msg = string.Format("正在创建数据库{0}...", dbName);
                LoggerHelper.Info(msg);
                var cmd = new MySqlCommand($"CREATE DATABASE {dbName} CHARACTER SET utf8 COLLATE utf8_general_ci", conn);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                var msg = "创建数据库失败！";
                LoggerHelper.Error(msg, e);
                return new Result(e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            return Result.Ok;
        }

        public async void DeleteDatabase(string dbName)
        {
            //删除数据库
            MySqlConnection conn = null;
            try
            {
                var msg = string.Format("正在删除数据库{0}...", dbName);
                LoggerHelper.Info(msg);
                conn = new MySqlConnection(_conStr);
                var cmd = new MySqlCommand($"select * from SCHEMATA where SCHEMA_NAME = '{dbName}'", conn);
                await conn.OpenAsync();
                var n = await cmd.ExecuteScalarAsync();
                if (n == null)
                {
                    LoggerHelper.Info("服务器中不存在名为" + dbName + "的数据库！");
                    return;
                }
                //首先恢复默认状态（强制删除数据库）
                //cmd = new MySqlCommand(string.Format("alter database {0} set single_user with rollback immediate", dbName), conn);
                //await cmd.ExecuteNonQueryAsync();
                cmd = new MySqlCommand(@"Drop DATABASE " + dbName, conn);
                await cmd.ExecuteNonQueryAsync();
                msg = string.Format("{0}数据库删除成功！", dbName);
                LoggerHelper.Info(msg);
            }
            catch (Exception e)
            {
                var msg = string.Format("删除数据({0})库失败,请手动删除！", dbName);
                LoggerHelper.Error(msg, e);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        #endregion
    }
}