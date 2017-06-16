namespace WallE.Data.MySql
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserBriefInfo
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(RealName) ? Name : RealName;
        }
    }
}
