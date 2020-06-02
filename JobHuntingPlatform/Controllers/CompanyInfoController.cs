using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 企业基本资料界面的控制器.
    /// </summary>
    public class CompanyInfoController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入企业基本资料界面.
        /// </summary>
        /// <returns>企业基本资料界面.</returns>
        public ActionResult CompanyInfo()
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
            var user = Db.Queryable<Company>().Where(it => it.Id == userId).Single();
            Object data = new
            {
                id = user.Id,
                name = user.Name,
                password = user.Password,
                account = user.Account,
                isRelease = user.IsPass,
                address = user.Address,
                phone = user.Phone,
                type = user.Type,
                isPass = user.IsPass,
            };
            return Json(new { code = 200, data }, JsonRequestBehavior.AllowGet);
        }
    }
}