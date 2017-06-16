using System.Collections.Generic;
using System.Linq;

namespace WallE.Data.MySql
{
    public class SqlUtil
    {
        public static string GetDeleteSql(string tableName, string columnName, object value)
        {
            var sqlStr = string.Format("Delete FROM {0} where [{1}] = '{2}'", tableName, columnName, value);
            return sqlStr;
        }

        public static string GetDeleteSql(string tableName, IDictionary<string, object> columnFilter)
        {
            var str1 = "Delete FROM " + tableName + " where ";
            var str2 = columnFilter.Aggregate("", (t1, t2) => t1 == ""
                ? string.Format("[{0}]='{1}'", t2.Key, t2.Value)
                : string.Format("{0} and [{1}]='{2}'", t1, t2.Key, t2.Value));
            return str1 + str2;
        }
    }
}
