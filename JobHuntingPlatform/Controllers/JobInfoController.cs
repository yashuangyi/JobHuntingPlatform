using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 求职资讯界面的控制器.
    /// </summary>
    public class JobInfoController : Controller
    {
        /// <summary>
        /// 进入求职资讯界面.
        /// </summary>
        /// <returns>求职资讯界面.</returns>
        public ActionResult JobInfo()
        {
            return View();
        }
    }
}