using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WallE.Data.MySql;
using WallE.Platform.DalService;
using WallE.Platform.Models;
using WallE.Platform.Support;

namespace WallE.Platform.Controllers
{
    /// <summary>
    /// Banner管理
    /// </summary>
    [RoutePrefix("api/picture")]
    // ReSharper disable once InconsistentNaming
    public class PictureAPIController : ApiController
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
        public void SavePicture(StreamInfo streamInfo)
        {
            SrcImagPath = null;
            ThumbnailPath = null;
            var fileType = streamInfo.FileName.Substring(streamInfo.FileName.LastIndexOf(".", StringComparison.Ordinal) + 1);
            if (fileType == "gif" || fileType == "GIF" || fileType == "jpg" || fileType == "JPG" || fileType == "png" || fileType == "PNG")
            {
                //新的文件名
                var imgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fileType;
                var srcImagPath = "/Content/resource/" + imgName;
                var srcPath = AppDomain.CurrentDomain.BaseDirectory + srcImagPath;
                FileManager.SaveFile(srcPath,streamInfo.Stream);
                SrcImagPath = srcImagPath;
                var thumbnailName = "t_" + imgName;
                var thumbnailPath = "/Content/resource/" + thumbnailName;
                var thumbnailFile = AppDomain.CurrentDomain.BaseDirectory + thumbnailPath;
                if (FileManager.GenerateThumbnail(srcPath, thumbnailFile))
                    ThumbnailPath = thumbnailPath;
            }
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="accessToken">令牌</param>
        /// <param name="fileName">文件名</param>
        /// <param name="objectId">对象编号</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public async Task<ApiResult> Add(string accessToken, string fileName,string objectId)
        {
            if (!Request.Content.IsMimeMultipartContent())
                return new ApiResult(new Exception("传入接口的文件数据有误!"));
            var fileDataList = await Request.Content.GetFileStreamAsync();
            var data = fileDataList.FirstOrDefault();
            if (data == null)
                return new ApiResult(new Exception("传入接口的文件数据有误!"));
            SavePicture(data);
            if (string.IsNullOrEmpty(SrcImagPath) || string.IsNullOrEmpty(ThumbnailPath))
                return new ApiResult(new Exception("添加图片失败!"));
            var record = await PictureDal.Ins.AddPicture(objectId, fileName, SrcImagPath, ThumbnailPath);
            return record == null ? new ApiResult(new Exception("添加图片失败！")) : new ApiResult<Picture>(record);
        }
    }
}