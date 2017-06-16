using System.Web.Mvc;
using WallE.Platform.Models;

namespace WallE.Platform.Controllers
{
    /// <summary>
    /// 主界面控制器
    /// </summary>
    public class HomeController : BaseController
    {
        public ActionResult Index(SearchParams searchParams)
        {
            return View();
        }
    }
}