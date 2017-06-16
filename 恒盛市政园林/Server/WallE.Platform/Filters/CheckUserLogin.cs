using System.Web;
using System.Web.Mvc;

namespace WallE.Platform.Filters
{
    public class CheckUserLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var cookie = HttpContext.Current.Request.Cookies["ValidateUser"];
            if (cookie == null)
            {
                var result = new RedirectResult("/Account/Login");
                result.ExecuteResult(filterContext);
            }
        }
    }
}