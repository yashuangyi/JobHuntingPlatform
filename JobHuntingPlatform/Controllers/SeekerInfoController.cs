using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 求职者基本资料界面的控制器.
    /// </summary>
    public class SeekerInfoController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入求职者基本资料界面.
        /// </summary>
        /// <returns>求职者基本资料界面.</returns>
        public ActionResult SeekerInfo()
        {
            return View();
        }

        /// <summary>
        /// 初始化信息.
        /// </summary>
        /// <param name="userId">用户ID.</param>
        /// <returns>Json.</returns>
        public ActionResult GetMyData(int userId)
        {
            var user = Db.Queryable<Seeker>().Where(it => it.Id == userId).Single();
            Object data = new
            {
                id = user.Id,
                name = user.Name,
                password = user.Password,
                account = user.Account,
                resumePath = user.ResumePath,
                isRelease = user.IsRelease,
                offer = user.Offer,
                address = user.Address,
                phone = user.Phone,
                age = user.Age,
                sex = user.Sex,
            };
            return Json(new { code = 200, data }, JsonRequestBehavior.AllowGet);
        }
    }
}