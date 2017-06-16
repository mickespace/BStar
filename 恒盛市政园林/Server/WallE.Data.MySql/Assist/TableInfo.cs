using System.Collections.Generic;
using System.Linq;

namespace WallE.Data.MySql
{
    /// <summary>
    /// Class TableInfo.
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableInfo"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        public TableInfo(string tableName)
        {
            TableName = tableName;
            Columns = new List<ColumnInfo>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableInfo"/> class.
        /// </summary>
        public TableInfo()
            : this(null)
        {

        }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get; set; }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public List<ColumnInfo> Columns { get; private set; }

        /// <summary>
        /// Gets the <see cref="ColumnInfo"/> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>ColumnInfo.</returns>
        public ColumnInfo this[string name]
        {
            get { return Columns.FirstOrDefault(t => t.Name == name); }
        }
    }
}
