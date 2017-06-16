using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WallE.Data.MySql;
using WallE.Platform.DalService;
using WallE.Platform.Models;

namespace WallE.Platform.Controllers
{
    /// <summary>
    /// 工程案例管理
    /// </summary>
    [RoutePrefix("api/product")]
    // ReSharper disable once InconsistentNaming
    public class ProductAPIController : ApiController
    {
        /// <summary>
        /// 获取案例列表(pageIndex为1,pageSize为-1时则获取所有)
        /// </summary>
        /// <param name="pageIndex">页索引（从1开始）</param>
        /// <param name="pageSize">页记录数量</param>
        /// <param name="category">案例分类，默认不填（获取所有）</param>
        [HttpGet]
        [Route("list")]
        public async Task<ApiResult<List<ProductModel>>> ProductList(int pageIndex, int pageSize, string category = null)
        {
            var search = new SearchParams { PageIndex = pageIndex, PageSize = pageSize, Category = category };
            var productList = await ProductDal.Ins.GetProductListAsync(search);
            return new ApiResult<List<ProductModel>>(productList) { Count = search.TotalCount };
        }

        /// <summary>
        /// 获取案例分类
        /// </summary>
        [HttpGet]
        [Route("categorys")]
        public async Task<ApiResult<List<Category>>> CategoryList()
        {
            var categorys = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.Product);
            return new ApiResult<List<Category>>(categorys);
        }

        /// <summary>
        /// 获取案例图集
        /// </summary>
        /// <param name="id">案例编号</param>
        [HttpGet]
        [Route("pictures")]
        public async Task<ApiResult<List<Picture>>> PictureList(string id)
        {
            var pictures = await PictureDal.Ins.GetPicturesAsync(id);
            return new ApiResult<List<Picture>>(pictures);
        }

        /// <summary>
        /// 获取案例详细信息
        /// </summary>
        /// <param name="id">案例编号</param>
        [HttpGet]
        [Route("info")]
        public async Task<ApiResult<Product>> ProductDetial(string id)
        {
            var model = await ProductDal.Ins.GetProductInfoAsync(id);
            return new ApiResult<Product>(model);
        }

        /// <summary>
        /// 上线与下线
        /// </summary>
        /// <param name="accessToken">令牌</param>
        /// <param name="id">编号</param>
        /// <param name="status">状态(true下线false上线)</param>
        [HttpPost]
        [Route("update")]
        public async Task<ApiResult> ModifyStatus(string accessToken, string id, bool status)
        {
            return await ProductDal.Ins.ModifyStatusAsync(id, status);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="accessToken">令牌</param>
        /// <param name="id">编号</param>
        [HttpPost]
        [Route("delete")]
        public async Task<ApiResult> Delete(string accessToken, string id)
        {
            return await ProductDal.Ins.DeleteProductAsync(id);
        }

        /// <summary>
        /// 删除新分类
        /// </summary>
        /// <param name="accessToken">令牌</param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete_category")]
        public async Task<ApiResult> DeleteCategory(string accessToken, string categoryId)
        {
            var r = await CategoryDal.Ins.DeleteCategoryAsync(categoryId);
            return r ? new ApiResult() : new ApiResult(new Exception("删除分类失败！"));
        }

        /// <summary>
        /// 添加新分类
        /// </summary>
        /// <param name="accessToken">令牌</param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add_category")]
        public async Task<ApiResult> AddCategory(string accessToken, string category)
        {
            var record=await CategoryDal.Ins.AddCategoryAsync((int)CategoryType.Product, category);
            return record == null ? new ApiResult(new Exception("添加新分类失败！")) : new ApiResult<Category>(record);
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="accessToken">令牌</param>
        /// <param name="pictureId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete_category")]
        public async Task<ApiResult> DeletePicture(string accessToken, string pictureId)
        {
            var r = await PictureDal.Ins.DeletePictureAsync(pictureId);
            return r ? new ApiResult() : new ApiResult(new Exception("删除图片失败！"));
        }
    }
}