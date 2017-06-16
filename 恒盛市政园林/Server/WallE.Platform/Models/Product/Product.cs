using System;
using WallE.Data.MySql;

namespace WallE.Platform.Models
{
    /// <summary>
    /// ������Ϣ
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
        /// ��ʶ
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// ״̬��-1ɾ��1����0���ã�
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// ��ʾλ�ã�1�б�ҳ2��ҳ4����ҳ�棩
        /// </summary>
        public int DisplayType { get; set; }

        /// <summary>
        /// ��ʾ˳��
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// ����ͼ
        /// </summary>
        public string ImageSmall { get; set; }

        /// <summary>
        /// ԭʼͼƬ
        /// </summary>
        public string ImageNormal { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// �û����
        /// </summary>
        public int UserId { get; set; }
        #endregion

        public override string MapPropertyName(string propertyName)
        {
            return propertyName;
        }
    }
}
