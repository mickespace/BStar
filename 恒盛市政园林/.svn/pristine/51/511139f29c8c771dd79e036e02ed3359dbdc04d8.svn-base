using System;
using WallE.Data.MySql;

namespace WallE.Platform.Models
{
    /// <summary>
    /// 案例信息
    /// </summary>
    public class Product : EntityBase
    {
        public const string ColId = "Id";
        public const string ColCreateTime = "CreateTime";
        public const string ColStatus = "Status";
        public const string ColDisplayType = "DisplayType";
        public const string ColDisplayOrder = "DisplayOrder";
        public const string TableName = "product";
        public const string ColTitle = "Title";
        public const string ColDescription = "Description";
        public const string ColContent = "Content";
        public const string ColImageSmall = "ImageSmall";
        public const string ColImageNormal = "ImageNormal";
        public const string ColType = "Type";
        public const string ColAuthor = "Author";
        public const string ColUserId = "UserId";

        #region Properties
        /// <summary>
        /// 标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态（-1删除1启用0禁用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 显示位置（1列表页2首页4其它页面）
        /// </summary>
        public int DisplayType { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 案例标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 案例描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 案例内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ImageSmall { get; set; }

        /// <summary>
        /// 原始图片
        /// </summary>
        public string ImageNormal { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { get; set; }
        #endregion

        public override string MapPropertyName(string propertyName)
        {
            return propertyName;
        }
    }
}
