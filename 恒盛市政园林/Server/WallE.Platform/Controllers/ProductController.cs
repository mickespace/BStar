using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WallE.Data.MySql;
using WallE.Platform.DalService;
using WallE.Platform.Models;

namespace WallE.Platform.Controllers
{
    /// <summary>
    /// Class ProductController.
    /// </summary>
    public class ProductController : BaseController
    {
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public async Task<ActionResult> Add(Product model)
        {
            if (string.IsNullOrEmpty(model.Title))
                model.Title = "未命名的工程案例";
            SavePicture(true);
            model.Content = HttpUtility.UrlDecode(model.Content);
            model.ImageNormal = SrcImagPath ?? ErrorPicture;
            model.ImageSmall = ThumbnailPath ?? ErrorPicture;
            model.CreateTime = DateTime.Now.Date;
            model.Status = 1;
            model.DisplayType = 1;
            //model.Id = tempId;
            model.Id = Guid.NewGuid().ToString().ToLower();
            var result = await ProductDal.Ins.CreateProductAsync(model);
            if (result.ResultType == ResultType.Ok)
                return RedirectToAction("Query");
            model.Id = null;
            model.ImageNormal = "";
            model.ImageSmall = "";
            ViewData["Categorys"] = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.Product);
            //if (!string.IsNullOrEmpty(tempId))
            //{
            //    ViewData["TempId"] = tempId;
            //    ViewData["Pictures"] = await PictureDal.Ins.GetPicturesAsync(tempId);
            //}
            return View("ProductDetail", model);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public async Task<ActionResult> Update(Product model)
        {
            SavePicture(true);
            model.Content = HttpUtility.UrlDecode(model.Content);
            var updateDic = new Dictionary<string, object>();
            if (SrcImagPath != null)  //图片更改
            {
                updateDic[Product.ColImageNormal] = SrcImagPath ?? ErrorPicture;
                updateDic[Product.ColImageSmall] = ThumbnailPath ?? ErrorPicture;
            }
            updateDic[Product.ColType] = model.Type;
            updateDic[Product.ColContent] = model.Content;
            updateDic[Product.ColDisplayOrder] = model.DisplayOrder;
            updateDic[Product.ColTitle] = model.Title;
            updateDic[Product.ColDescription] = model.Description;
            var r = await ProductDal.Ins.ModifyProductAsync(model.Id, updateDic);
            if (r)
                return RedirectToAction("Query");
            ViewData["Categorys"] = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.Product);
            return View("ProductDetail", model);
        }

        /// <summary>
        /// Queries the specified base parameters.
        /// </summary>
        /// <param name="searchParams">The search parameters.</param>
        [HttpGet]
        public async Task<ActionResult> Query(SearchParams searchParams)
        {
            var products = await ProductDal.Ins.GetProductListAsync(searchParams);
            ViewData["SearchParams"] = searchParams;
            return View("Product", products);
        }

        /// <summary>
        /// Products the detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet]
        public async Task<ActionResult> ProductDetail(string id)
        {
            ViewData["Categorys"] = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.Product);
            if (string.IsNullOrEmpty(id))
            {
                //var tempId = Guid.NewGuid().ToString().ToLower();
                //ViewData["TempId"] = tempId;
                return View(new Product());
            }
            var product = await ProductDal.Ins.GetProductInfoAsync(id);
            //ViewData["TempId"] = id;
            //ViewData["Pictures"] = await PictureDal.Ins.GetPicturesAsync(id);
            return View(product);
        }

        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Category()
        {
            var categorys = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.Product);
            return View(categorys);
        }

        /// <summary>
        ///导航菜单视图
        /// </summary>
        public async Task<PartialViewResult> NavMenu()
        {
            var categorys = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.Product);
            return PartialView(categorys);
        }
    }
}