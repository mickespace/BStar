using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace WallE.Data.MySql
{
    public static class SqlDbUtil
    {
        private static readonly Dictionary<string, TableInfo> TableDic = new Dictionary<string, TableInfo>();

        /// <summary>
        /// 获取表的信息
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="sqlConn">The SQL connection.</param>
        /// <returns>TableInfo.</returns>
        public static TableInfo GetTableInfo(string tableName, MySqlConnection sqlConn)
        {
            if (TableDic.ContainsKey(tableName))
                return TableDic[tableName];
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();
            DataTable dt = null;
            MySqlDataReader reader = null;
            try
            {
                var tableInfo = new TableInfo(tableName);
                var cmd = sqlConn.CreateCommand();
                cmd.CommandText = string.Format("select * from {0} where 0=1", tableName);

                reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
                dt = reader.GetSchemaTable();
                if (dt!=null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var column = new ColumnInfo
                        {
                            Name = (string)row["ColumnName"],
                            ColumnSize = (int)row["ColumnSize"],
                            IsPrimaryKey = (bool)row["IsKey"],
                            DbType = (int)row["ProviderType"],
                            //DbTypeName = (String)row["DataType"],
                            IsAutoIncrement = (bool)row["IsAutoIncrement"]
                        };
                        tableInfo.Columns.Add(column);
                    }
                }             
                TableDic[tableName] = tableInfo;
                return tableInfo;
            }
            finally
            {
                if (dt != null)
                    dt.Dispose();
                if (reader != null)
                    reader.Close();
            }
        }

        /// <summary>
        /// 获取表的主键
        /// </summary>
        /// <param name="tableInfo">The table information.</param>
        /// <returns>List&lt;PrimaryKey&gt;.</returns>
        public static List<PrimaryKey> GetPrimaryKeys(TableInfo tableInfo)
        {
            return tableInfo.Columns.Where(t => t.IsPrimaryKey)
                .Select(t => new PrimaryKey(t.Name, (SqlDbType) t.DbType))
                .ToList();
        }

        /// <summary>
        /// 获取更新记录命令
        /// </summary>
        /// <param name="ti">The ti.</param>
        /// <param name="sqlConn">The SQL connection.</param>
        /// <returns>SqlCommand.</returns>
        public static MySqlCommand GetUpdateCommand(TableInfo ti, MySqlConnection sqlConn)
        {
            var cmd = new MySqlCommand();
            var strBuilder = new StringBuilder("update " + ti.TableName + " set ");

            var isFirst = true;
            foreach (var column in ti.Columns.Where(t => !t.IsPrimaryKey && !t.IsAutoIncrement))
            {
                if (!isFirst)
                    strBuilder.Append(", ");
                var pName = "@" + column.Name;
                strBuilder.Append(column.Name + "=" + pName);
                cmd.Parameters.Add(pName, (MySqlDbType)column.DbType, column.ColumnSize, column.Name);
                isFirst = false;
            }
            strBuilder.Append(" where ");
            var priKeyCount = ti.Columns.Count(t => t.IsPrimaryKey);
            if (priKeyCount > 1)
                strBuilder.Append("(");
            isFirst = true;
            foreach (var column in ti.Columns.Where(t => t.IsPrimaryKey))
            {
                if (!isFirst)
                    strBuilder.Append(" and ");
                var pName = "@" + column.Name;
                strBuilder.Append("(" + column.Name + "=" + pName + ")");
                cmd.Parameters.Add(pName, (MySqlDbType)column.DbType, column.ColumnSize, column.Name);
                isFirst = false;
            }
            if (priKeyCount > 1)
                strBuilder.Append(")");
            cmd.CommandText = strBuilder.ToString();
            cmd.Connection = sqlConn;
            cmd.UpdatedRowSource = UpdateRowSource.None;
            return cmd;
        }

        /// <summary>
        /// 获取添加记录的命令
        /// </summary>
        /// <param name="ti">The ti.</param>
        /// <param name="sqlConn">The SQL connection.</param>
        /// <returns>SqlCommand.</returns>
        public static MySqlCommand GetInsertCommand(TableInfo ti, MySqlConnection sqlConn)
        {
            var cmd = new MySqlCommand();
            var strBuilder = new StringBuilder("insert into " + ti.TableName + " (");
            var isFirst = true;
            foreach (var column in ti.Columns.Where(t => !t.IsAutoIncrement))
            {
                if (!isFirst)
                    strBuilder.Append(", ");
                strBuilder.Append(column.Name);
                isFirst = false;
            }
            strBuilder.Append(") values (");
            isFirst = true;
            foreach (var column in ti.Columns.Where(t => !t.IsAutoIncrement))
            {
                if (!isFirst)
                    strBuilder.Append(", ");
                var pName = "@" + column.Name;
                strBuilder.Append(pName);
                cmd.Parameters.Add(pName, (MySqlDbType)column.DbType, column.ColumnSize, column.Name);
                isFirst = false;
            }
            strBuilder.Append(")");
            cmd.CommandText = strBuilder.ToString();
            cmd.Connection = sqlConn;
            cmd.UpdatedRowSource = UpdateRowSource.None;
            return cmd;
        }

        /// <summary>
        /// 获取删除记录的命令
        /// </summary>
        /// <param name="ti">The ti.</param>
        /// <param name="sqlConn">The SQL connection.</param>
        /// <returns>SqlCommand.</returns>
        public static MySqlCommand GetDeleteCommand(TableInfo ti, MySqlConnection sqlConn)
        {
            var cmd = new MySqlCommand();
            var strBuilder = new StringBuilder("delete from " + ti.TableName + " where ");
            var isFirst = true;
            var priKeyCount = ti.Columns.Count(t => t.IsPrimaryKey);
            if (priKeyCount > 1)
                strBuilder.Append("(");
            foreach (var column in ti.Columns.Where(t => t.IsPrimaryKey))
            {
                if (!isFirst)
                    strBuilder.Append(" and ");
                var pName = "@" + column.Name;
                strBuilder.Append("(" + column.Name + "=" + pName + ")");
                cmd.Parameters.Add(pName, (MySqlDbType)column.DbType, column.ColumnSize, column.Name);
                isFirst = false;
            }
            if (priKeyCount > 1)
                strBuilder.Append(")");
            cmd.CommandText = strBuilder.ToString();
            cmd.Connection = sqlConn;
            cmd.UpdatedRowSource = UpdateRowSource.None;
            return cmd;
        }

        /// <summary>
        /// 获取更改的表
        /// </summary>
        /// <param name="ts">The ts.</param>
        /// <param name="sqlConn">The SQL connection.</param>
        /// <param name="insertTable">The insert table.</param>
        /// <param name="otherTable">The other table.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetChangedData(TableStatus ts, MySqlConnection sqlConn, out DataTable insertTable, out DataTable otherTable)
        {
            insertTable = null;
            otherTable = null;
            if (!ts.GetAllRecords().Any())
                return false;
            if (sqlConn.State == ConnectionState.Closed)
                sqlConn.Open();
            using (var cmd = sqlConn.CreateCommand())
            {
                cmd.CommandText = string.Format("select * from {0} where 0=1", ts.TableName);
                insertTable = new DataTable(ts.TableName);
                using (var da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(insertTable);
                }
                otherTable = insertTable.Copy();
            }
            if (ts.AppendedRecords != null)
            {
                foreach (var record in ts.AppendedRecords)
                {
                    var row = insertTable.NewRow();
                    foreach (var item in record)
                        row[item.Key] = item.Value;
                    insertTable.Rows.Add(row);
                }
            }
            if (ts.ModifiedRecords != null)
            {
                foreach (var record in ts.ModifiedRecords)
                {
                    var row = otherTable.NewRow();
                    foreach (var item in record)
                        row[item.Key] = item.Value;
                    otherTable.Rows.Add(row);
                    row.AcceptChanges();
                    row.SetModified();
                }
            }
            if (ts.DeletedRecords != null)
            {
                foreach (var record in ts.DeletedRecords)
                {
                    var row = otherTable.NewRow();
                    foreach (var item in record)
                        row[item.Key] = item.Value;
                    otherTable.Rows.Add(row);
                    row.AcceptChanges();
                    row.Delete();
                }
            }
            return true;
        }
    }
}
