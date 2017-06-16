using System.Collections.Generic;

namespace WallE.Platform.Models
{
    /// <summary>
    /// 修改信息
    /// </summary>
    public class ModifyModel
    {
        public ModifyModel()
        {
            PropertyDic=new Dictionary<string, object>();
        }

        /// <summary>
        /// 属性修改字典
        /// </summary>
        public Dictionary<string, object> PropertyDic { get; set; }
    }
}