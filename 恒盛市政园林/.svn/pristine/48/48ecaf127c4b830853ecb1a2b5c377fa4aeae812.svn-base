using System.Collections.Generic;

namespace WallE.Data.MySql
{
    /// <summary>
    /// 表示一张表的状态
    /// </summary>
    public class TableStatus
    {
        public TableStatus(string tableName)
        {
            TableName = tableName;
        }

        /// <summary>
        /// 表的名称
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get; set; }

        /// <summary>
        /// 表中新增的记录
        /// </summary>
        /// <value>The appended records.</value>
        public List<DataRecord> AppendedRecords { get; } = new List<DataRecord>();

        /// <summary>
        /// 表中修改的记录
        /// </summary>
        /// <value>The modified records.</value>
        public List<DataRecord> ModifiedRecords { get; } = new List<DataRecord>();

        /// <summary>
        /// 表中被删除的记录
        /// </summary>
        /// <value>The deleted records.</value>
        public List<DataRecord> DeletedRecords { get; } = new List<DataRecord>();

        /// <summary>
        /// 表的状态是否有改变
        /// </summary>
        /// <value><c>true</c> if this instance is changed; otherwise, <c>false</c>.</value>
        public bool IsChanged => (AppendedRecords.Count > 0)|| (ModifiedRecords.Count > 0)|| (DeletedRecords.Count > 0);

        /// <summary>
        /// 获取所有更改的记录
        /// </summary>
        /// <returns>IEnumerable&lt;DataRecord&gt;.</returns>
        public IEnumerable<DataRecord> GetAllRecords()
        {
            if (AppendedRecords != null)
            {
                foreach (var record in AppendedRecords)
                    yield return record;
            }
            if (ModifiedRecords != null)
            {
                foreach (var record in ModifiedRecords)
                    yield return record;
            }
            if (DeletedRecords != null)
            {
                foreach (var record in DeletedRecords)
                    yield return record;
            }
        }
    }
}