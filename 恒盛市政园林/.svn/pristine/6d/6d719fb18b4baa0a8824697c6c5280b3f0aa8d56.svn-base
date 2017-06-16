using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallE.Data.MySql;
using WallE.Platform.Models;

namespace WallE.Platform.DalService
{
    internal class PictureDal
    {
        private static PictureDal _ins;
        private PictureDal() { }

        /// <summary>
        /// 全局唯一变量
        /// </summary>
        public static PictureDal Ins
        {
            get { return _ins ?? (_ins = new PictureDal()); }
        }

        /// <summary>
        /// 获取图片列表
        /// </summary>
        /// <param name="objectId">对象编号</param>
        public async Task<List<Picture>> GetPicturesAsync(string objectId)
        {
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = await dbAccessor.QueryAsync(Picture.TableName, Picture.ColObjectId, objectId, 
                    new Dictionary<string, bool> { { Picture.ColCreateTime, false } });
                var entities = records.Select(t => t.As<Picture>()).ToList();
                return entities;
            }
            catch (Exception ex)
            {
                var error = "获取图片失败,请重试！";
                LoggerHelper.Error(error, ex);
                return new List<Picture>();
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="fileName"></param>
        /// <param name="imageNormal"></param>
        /// <param name="imageSmall"></param>
        /// <returns></returns>
        public async Task<Picture> AddPicture(string objectId, string fileName, string imageNormal, string imageSmall)
        {
            try
            {
                var picture = new Picture
                {
                    Id = Guid.NewGuid().ToString().ToLower(),
                    Name = fileName,
                    ObjectId = objectId,
                    CreateTime = DateTime.Now,
                    ImageNormal = imageNormal,
                    ImageSmall = imageSmall
                };
                //保存附件信息
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var r = await dbAccessor.InsertAsync(Picture.TableName, new DataRecord(picture.ToDictionary()));
                return r.ResultType != ResultType.Ok ? null : picture;
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("保存图片失败", ex);
                return null;
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="id">删除的Id</param>
        public async Task<bool> DeletePictureAsync(string id)
        {
            try
            {
                var dbAccessor = M.DbAccessorMgnt.GetMgntDbAccessor();
                var records = await dbAccessor.QueryAsync(Picture.TableName, Picture.ColId, id);
                var record = records.FirstOrDefault();
                if (record == null)
                {
                    var errorStr = $"删除图片失败，不存在编号为（{id}）的分类！";
                    LoggerHelper.Info(errorStr);
                    return true;
                }
                return await dbAccessor.DeleteAsync(Picture.TableName, Picture.ColId, id);
            }
            catch (Exception ex)
            {
                const string error = "删除图片失败，请重试！";
                LoggerHelper.Error(error, ex);
                return false;
            }
        }
    }
}