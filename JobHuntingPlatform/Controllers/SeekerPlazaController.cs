using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 人才广场界面的控制器.
    /// </summary>
    public class SeekerPlazaController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入人才广场界面.
        /// </summary>
        /// <returns>人才广场界面.</returns>
        public ActionResult SeekerPlaza()
        {
            return View();
        }

        /// <summary>
        /// 发出邀请.
        /// </summary>
        /// <param name="userId">用户.</param>
        /// <param name="companyId">企业.</param>
        /// <returns>Json.</returns>
        public ActionResult Invite(int userId, int companyId)
        {
            if (Db.Queryable<Notice>().Where(it => it.SourceId == companyId && it.TargetId == userId && it.Type == "面试邀请").Single() != null)
            {
                return Json(new { code = 400 }, JsonRequestBehavior.AllowGet);
            }

            Notice old = Db.Queryable<Notice>().Where(it => it.SourceId == userId && it.TargetId == companyId && it.IsReply == 0).Single();
            if (old != null)
            {
                old.IsReply = 1;
                Db.Updateable(old).ExecuteCommand();
            }

            Company company = Db.Queryable<Company>().Where(it => it.Id == companyId).Single();
            Notice notice = new Notice
            {
                SourceId = companyId,
                TargetId = userId,
                Time = DateTime.Now.ToString(),
                Content = company.Name + "向您发出面试邀请，请及时回复",
                Type = "面试邀请",
                IsReply = 0,
            };
            Db.Insertable(notice).ExecuteReturnIdentity();

            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取求职者列表.
        /// </summary>
        /// <param name="page">总页数.</param>
        /// <param name="limit">一页多少行数据.</param>
        /// <param name="search">查询字段.</param>
        /// <returns>求职者列表.</returns>
        public ActionResult GetSeeker(int page, int limit, string search = null)
        {
            List<Seeker> list = null;
            int count;
            // 分页操作，Skip()跳过前面数据项
            if (string.IsNullOrEmpty(search))
            {
                // 分页操作，Skip()跳过前面数据项
                var temp = Db.Queryable<Seeker>().Where(it => it.IsRelease == 1);
                count = temp.Count();
                list = temp.Skip((page - 1) * limit).Take(limit).ToList();
            }
            else
            {
                var temp = Db.Queryable<Seeker>().Where(it => it.Name.Contains(search) && it.IsRelease == 1);
                count = temp.Count();
                list = temp.Skip((page - 1) * limit).Take(limit).ToList();
            }

            // 参数必须一一对应，JsonRequestBehavior.AllowGet一定要加，表单要求code返回0
            return Json(new { code = 0, msg = string.Empty, count, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}