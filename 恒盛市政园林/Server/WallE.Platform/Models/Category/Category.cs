using WallE.Data.MySql;

namespace WallE.Platform.Models
{
    /// <summary>
    /// 分类信息
    /// </summary>
    public class Category : EntityBase
    {
        public const string TableName = "category";
        public const string ColId = "Id";
        public const string ColName = "Name";
        public const string ColType = "Type";

        #region Properties

        /// <summary>
        /// 分类编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 分类类型（1Product2其它）
        /// </summary>
        public int Type { get; set; }

        #endregion

        public override string MapPropertyName(string propertyName)
        {
            return propertyName;
        }
    }
}