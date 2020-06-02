using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 我的简历界面的控制器.
    /// </summary>
    public class MyResumeController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入我的简历界面.
        /// </summary>
        /// <returns>我的简历界面.</returns>
        public ActionResult MyResume()
        {
            return View();
        }

        /// <summary>
        /// 获取状态.
        /// </summary>
        /// <param name="userId">用户编号.</param>
        /// <returns>Json.</returns>
        public ActionResult GetStatus(int userId)
        {
            var user = Db.Queryable<Seeker>().Where(it => it.Id == userId).Single();
            int isRelease = user.IsRelease;
            string resumePath = user.ResumePath;
            return Json(new { code = 200, isRelease, resumePath }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 更改状态.
        /// </summary>
        /// <param name="userId">用户编号.</param>
        /// <returns>Json.</returns>
        public ActionResult ChangeStatus(int userId)
        {
            var seeker = Db.Queryable<Seeker>().Where(it => it.Id == userId).Single();
            seeker.IsRelease = 1 - seeker.IsRelease;
            Db.Updateable(seeker).ExecuteCommand();

            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }
    }
}