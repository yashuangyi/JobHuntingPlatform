using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 主页界面的控制器.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 进入主页界面.
        /// </summary>
        /// <returns>主页界面.</returns>
        public ActionResult Home()
        {
            ViewBag.UserId = Session["userId"];
            ViewBag.UserPower = Session["userPower"];
            ViewBag.UserName = Session["userName"];
            return View();
        }

        /// <summary>
        /// 进入首页界面.
        /// </summary>
        /// <returns>首页界面.</returns>
        public ActionResult HomePage()
        {
            return View();
        }
    }
}