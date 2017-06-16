namespace WallE.Data.MySql
{
    /// <summary>
    /// Class ColumnInfo.
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 列的名称
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        /// <value>The type of the database.</value>
        public int DbType { get; set; }

        ///// <summary>
        ///// 数据类型的名称
        ///// </summary>
        ///// <value>The name of the database type.</value>
        //public string DbTypeName { get; set; }

        /// <summary>
        /// 是否是主键
        /// </summary>
        /// <value><c>true</c> if this instance is key; otherwise, <c>false</c>.</value>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 列的长度
        /// </summary>
        /// <value>The size of the column.</value>
        public int ColumnSize { get; set; }

        /// <summary>
        /// 是否自动递增
        /// </summary>
        /// <value><c>true</c> if [automatic increment]; otherwise, <c>false</c>.</value>
        public bool IsAutoIncrement { get; set; }
    }
}
