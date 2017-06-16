using System;

namespace WallE.Platform
{
    [Flags]
    public enum DisplayType
    {
        /// <summary>
        /// 列表页
        /// </summary>
        ListPage=1,

        /// <summary>
        /// 首页
        /// </summary>
        FrontPage=2,

        /// <summary>
        /// 二级页
        /// </summary>
        SecondPage=4
    }
}