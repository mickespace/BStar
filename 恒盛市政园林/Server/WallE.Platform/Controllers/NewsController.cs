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
    /// Class NewsController.
    /// </summary>
    public class NewsController : BaseController
    {
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public async Task<ActionResult> Add(News model)
        {
            if (string.IsNullOrEmpty(model.Title))
                model.Title = "未命名的新闻";
            SavePicture(true);
            model.Content = HttpUtility.UrlDecode(model.Content);
            model.ImageNormal = SrcImagPath ?? ErrorPicture;
            model.ImageSmall = ThumbnailPath ?? ErrorPicture;
            model.CreateTime = DateTime.Now.Date;
            model.Status = 1;
            model.DisplayType = 1;
            model.Id = Guid.NewGuid().ToString().ToLower();
            var result = await NewsDal.Ins.CreateNewsAsync(model);
            if (result.ResultType == ResultType.Ok)
                return RedirectToAction("Query");
            model.Id = null;
            model.ImageNormal = "";
            model.ImageSmall = "";
            ViewData["Categorys"] = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.News);
            return View("NewsDetail", model);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public async Task<ActionResult> Update(News model)
        {
            SavePicture(true);
            model.Content = HttpUtility.UrlDecode(model.Content);
            var updateDic = new Dictionary<string, object>();
            if (SrcImagPath != null)  //图片更改
            {
                updateDic[News.ColImageNormal] = SrcImagPath ?? ErrorPicture;
                updateDic[News.ColImageSmall] = ThumbnailPath ?? ErrorPicture;
            }
            updateDic[News.ColType] = model.Type;
            updateDic[News.ColContent] = model.Content;
            updateDic[News.ColDisplayOrder] = model.DisplayOrder;
            updateDic[News.ColTitle] = model.Title;
            updateDic[News.ColDescription] = model.Description;
            var r = await NewsDal.Ins.ModifyNewsAsync(model.Id, updateDic);
            if (r)
                return RedirectToAction("Query");
            ViewData["Categorys"] = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.News);
            return View("NewsDetail", model);
        }

        /// <summary>
        /// Queries the specified base parameters.
        /// </summary>
        /// <param name="searchParams">The search parameters.</param>
        /// <returns>ActionResult.</returns>
        public async Task<ActionResult> Query(SearchParams searchParams)
        {
            var newss = await NewsDal.Ins.GetNewsListAsync(searchParams);
            ViewData["SearchParams"] = searchParams;
            return View("News", newss);
        }

        /// <summary>
        /// Newss the detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public async Task<ActionResult> NewsDetail(string id)
        {
            ViewData["Categorys"] = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.News);
            if (string.IsNullOrEmpty(id))
                return View(new News());
            var news = await NewsDal.Ins.GetNewsInfoAsync(id);
            return View(news);
        }

        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Category()
        {
            var categorys = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.News);
            return View(categorys);
        }

        /// <summary>
        ///导航菜单视图
        /// </summary>
        public async Task<PartialViewResult> NavMenu()
        {
            var categorys = await CategoryDal.Ins.GetCategorysAsync((int)CategoryType.News);
            return PartialView(categorys);
        }
    }
}