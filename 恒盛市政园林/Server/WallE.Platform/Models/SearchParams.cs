namespace WallE.Platform.Models
{
    public class SearchParams
    {
        public SearchParams()
        {
            Category = "";
            SearchStr = "";
            PageIndex = 1;
            PageSize = 10;
        }
        /// <summary>
        /// 搜索字符串
        /// </summary>
        public string SearchStr { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 分页页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 单页记录条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        public int EndPage {
            get
            {
                return TotalCount % PageSize == 0 ? TotalCount / PageSize : TotalCount / PageSize+1;
            }
        }
    }
}
