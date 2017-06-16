using System.Web.Mvc;
using System.Web.Security;
using WallE.Platform.Models;

namespace WallE.Platform.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View(new LogOnModel());
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                //var r = UserService.Ins.SignIn(model.UserName, model.Password, model.RememberMe);
                //if (r == SignInResult.Ok)
                //{
                //    model.Tip = "登录成功！";
                //    return RedirectToAction("Index", "HomeBstar");
                //}
                //if (r == SignInResult.UserNameError)
                //    model.Tip = "用户名不存在，请联系管理员！";
                //else if (r == SignInResult.PasswordError)
                //    model.Tip = "密码错误，请重新输入！";
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                return RedirectToAction("Index", "Home");
            }
            else
                model.Tip = "用户名或密码不正确！";
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            //UserService.Ins.SignOut();
            return RedirectToAction("LogOn");
        }


        public ActionResult Add(BStarUser model)
        {
            //var result = UserService.Ins.Add(model);
            return RedirectToAction("Query");
        }


        public ActionResult Update(BStarUser model)
        {
            //var result = UserService.Ins.Update(model);
            return RedirectToAction("Query");
        }


        public ActionResult Delete(int id)
        {
            //var result = UserService.Ins.Delete(id);
            return RedirectToAction("Query");
        }

        public ActionResult Query(SearchParams searchParams)
        {
            var accountModel = new AccountModel();
            //accountModel.Accounts = UserService.Ins.Query(ref baseParams);
            //accountModel.BaseParams = baseParams;
            return View("Account", accountModel);
        }

        public ActionResult AccountDetail(int id)
        {
            //if (id == 0)
            //{
            //    return System.Web.UI.WebControls.View(new BStarUser());
            //}
            //var result = UserService.Ins.Query(id);
            return View();
        }
    }
}