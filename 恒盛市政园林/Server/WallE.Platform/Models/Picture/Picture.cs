using System;
using WallE.Data.MySql;

namespace WallE.Platform.Models
{
    /// <summary>
    /// 图片数据
    /// </summary>
    public class Picture:EntityBase
    {
        public const string TableName = "picture_info";
        public const string ColId = "Id";
        public const string ColName = "Name";
        public const string ColObjectId = "ObjectId";
        public const string ColImageSmall = "ImageSmall";
        public const string ColImageNormal = "ImageNormal";
        public const string ColCreateTime = "CreateTime";

        #region Properties

        /// <summary>
        /// 图片编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 图片名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 原对象编号
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ImageSmall { get; set; }

        /// <summary>
        /// 原始图片
        /// </summary>
        public string ImageNormal { get; set; }

        #endregion

        public override string MapPropertyName(string propertyName)
        {
            return propertyName;
        }
    }
}