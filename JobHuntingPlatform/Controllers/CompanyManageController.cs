using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 企业管理界面的控制器.
    /// </summary>
    public class CompanyManageController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入企业管理界面.
        /// </summary>
        /// <returns>企业管理界面.</returns>
        public ActionResult CompanyManage()
        {
            return View();
        }

        /// <summary>
        /// 获取企业列表.
        /// </summary>
        /// <param name="page">总页数.</param>
        /// <param name="limit">一页多少行数据.</param>
        /// <param name="search">查询字段.</param>
        /// <returns>企业列表.</returns>
        public ActionResult GetCompany(int page, int limit, string search = null)
        {
            List<Company> list = null;
            int count;
            // 分页操作，Skip()跳过前面数据项
            if (string.IsNullOrEmpty(search))
            {
                // 分页操作，Skip()跳过前面数据项
                var temp = Db.Queryable<Company>();
                count = temp.Count();
                list = temp.Skip((page - 1) * limit).Take(limit).ToList();
            }
            else
            {
                var temp = Db.Queryable<Company>().Where(it => it.Name.Contains(search));
                count = temp.Count();
                list = temp.Skip((page - 1) * limit).Take(limit).ToList();
            }

            // 参数必须一一对应，JsonRequestBehavior.AllowGet一定要加，表单要求code返回0
            return Json(new { code = 0, msg = string.Empty, count, data = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改用户.
        /// </summary>
        /// <param name="user">用户.</param>
        /// <returns>Json.</returns>
        public ActionResult EditCompany(Company user)
        {
            Db.Updateable(user).ExecuteCommand();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除账户.
        /// </summary>
        /// <param name="userId">账户编号.</param>
        /// <returns>Json.</returns>
        public ActionResult DelCompany(int userId)
        {
            Db.Deleteable<Company>().Where(it => it.Id == userId).ExecuteCommand();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 审核账户.
        /// </summary>
        /// <param name="userId">账户编号.</param>
        /// <returns>Json.</returns>
        public ActionResult CheckCompany(int userId)
        {
            var company = Db.Queryable<Company>().Where(it => it.Id == userId).Single();
            company.IsPass = 1;
            Db.Updateable(company).ExecuteCommand();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }
    }
}