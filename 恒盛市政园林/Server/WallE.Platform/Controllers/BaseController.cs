using System;
using System.Web.Mvc;
using WallE.Platform.Support;

namespace WallE.Platform.Controllers
{
    /// <summary>
    /// Class BaseController.
    /// </summary>
    [Authorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// 错误图片
        /// </summary>
        protected static readonly string ErrorPicture = "/Content/resource/error.png";

        /// <summary>
        /// 原图文件路径
        /// </summary>
        public string SrcImagPath { get; set; }

        /// <summary>
        /// 缩略图文件路径
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// 文件统一管理
        /// </summary>
        /// <returns></returns>
        public void SavePicture(bool needThumbnail = false,string name= "Picture")
        {
            SrcImagPath = null;
            ThumbnailPath = null;
            var file = Request.Files[name];//获取上传的文件
            if (file == null) return;
            var fileType = file.FileName.Substring(file.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
            if (fileType == "gif" || fileType == "GIF" || fileType == "jpg" || fileType == "JPG" || fileType == "png" || fileType == "PNG")
            {
                //新的文件名
                var imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fileType;
                var srcImagPath = "/Content/resource/" + imgName;
                var srcPath = Server.MapPath("~" + srcImagPath);
                file.SaveAs(srcPath);
                SrcImagPath = srcImagPath;
                if (needThumbnail)
                {
                    var thumbnailName = "t_" + imgName;
                    var thumbnailPath = "/Content/resource/" + thumbnailName;
                    if (FileManager.GenerateThumbnail(srcPath, Server.MapPath("~" + thumbnailPath)))
                        ThumbnailPath = thumbnailPath;
                }
            }
        }    
    }
}