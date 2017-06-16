using System;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WallE.Data.MySql;

namespace WallE.Platform.Support
{
    /// <summary>
    /// 管理数据库初始化
    /// </summary>
    public class MgntDbManager
    {
        /// <summary>
        /// 创建管理数据库
        /// </summary>
        public async static void CreateManagementDb()
        {
            var masterConn = M.ConnManager.GetMasterConnString();
            if (string.IsNullOrEmpty(masterConn))
            {
                LoggerHelper.Error(new Exception("无法获取数据库连接字符串，请设置数据库连接信息！"));
                Environment.Exit(0);
            }
            var manageDbName = M.ConnManager.GetManagementDbName();
            LoggerHelper.Info("正在检测管理数据库是否存在...");
            //检测管理数据库是否存在
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(masterConn);
                var cmd = new MySqlCommand($"select * from SCHEMATA where SCHEMA_NAME = '{manageDbName}'", conn);
                await conn.OpenAsync();
                var n = await cmd.ExecuteScalarAsync();
                if (n != null)
                {
                    LoggerHelper.Info("成功获取管理数据库！");
                    return;
                }
            }
            catch (Exception e)
            {
                LoggerHelper.Error("无法连接数到据库服务器,请重新设置连接字符串", e);
                Environment.Exit(0);
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
            LoggerHelper.Info("正在创建管理数据库...");
            //创建数据库
            var result = await CreateMgntDatabaseAsync(manageDbName);
            if (result.ResultType == ResultType.Error)
            {
                LoggerHelper.Error("创建管理数据库失败！", result.Error);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 异步创建管理数据库及表结构
        /// </summary>
        /// <returns></returns>
        private async static Task<Result> CreateMgntDatabaseAsync(string dbName)
        {
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mgnt.sql");
            if (!File.Exists(fileName))
                return new Result(new FileNotFoundException(fileName + "未找到！"));
            //创建数据库
            var masterConnStr = M.ConnManager.GetMasterConnString();
            var masterAccessor = M.DbAccessorMgnt.GetDbAccessor(masterConnStr);
            var r = await masterAccessor.CreateDatabaseAsync(dbName);
            if (r.ResultType != ResultType.Ok)
                return r;
            var mgntConn = M.ConnManager.GetConnString(Guid.Empty + "");
            var dbAccessor = M.DbAccessorMgnt.GetDbAccessor(mgntConn);
            LoggerHelper.Info("正在初始化管理数据库...");
            //初始化数据库
            var stream = File.OpenRead(fileName);
            var result = await dbAccessor.ExecuteSqlFileAsync(stream);
            stream.Dispose();
            if (result.ResultType == ResultType.Error)
                masterAccessor.DeleteDatabase(dbName);
            else
                LoggerHelper.Info("初始化管理数据库成功！");
            return result;
        }
    }
}