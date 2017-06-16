using System;
using System.Collections.Generic;
using System.Data;

namespace WallE.Data.MySql
{
    /// <summary>
    /// 表示一条记录
    /// </summary>
    public class DataRecord : Dictionary<string, object>
    {
        /// <summary>
        /// 附加数据
        /// </summary>
        /// <value>The tag.</value>
        internal object Tag { get; set; }

        public DataRecord()
        {

        }

        public DataRecord(IEnumerable<KeyValuePair<string, object>> dic)
        {
            foreach (var item in dic)
            {
                this[item.Key] = item.Value;
            }
        }

        public DataRecord(IDataRecord reader)
        {
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                if (name.StartsWith("__sys") || name.StartsWith("sync_"))
                    continue;
                var value = reader[i];
                if (value is DBNull)
                    value = null;
                this[name] = value;
            }
        }

        public void SetValue(IDictionary<string, object> dic)
        {
            foreach (var item in dic)
                this[item.Key] = item.Value;
        }
    }
}
