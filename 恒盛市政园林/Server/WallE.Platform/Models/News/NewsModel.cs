using System;

namespace WallE.Platform.Models
{
    /// <summary>
    /// 新闻信息
    /// </summary>
    public class NewsModel
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 新闻摘要
        /// </summary>
        public string Description { get; set; }

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
    }
}