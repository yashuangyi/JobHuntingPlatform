using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 通知中心界面的控制器.
    /// </summary>
    public class NoticeBoxController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入通知中心界面.
        /// </summary>
        /// <returns>通知中心界面.</returns>
        public ActionResult NoticeBox()
        {
            return View();
        }

        /// <summary>
        /// 获取通知信息.
        /// </summary>
        /// <param name="page">总页数.</param>
        /// <param name="limit">一页多少行数据.</param>
        /// <param name="search">查询字段.</param>
        /// <param name="userId">查询者.</param>
        /// <param name="userPower">查询者权限.</param>
        /// <returns>企业列表.</returns>
        public ActionResult GetNotice(int page, int limit, int userId, string userPower, string search = null)
        {
            List<Notice> list = null;
            int count;
            
            if (string.IsNullOrEmpty(search))
            {
                // 分页操作，Skip()跳过前面数据项
                if(userPower == "求职者")
                {
                    var temp = Db.Queryable<Notice>().Where(it => it.TargetId == userId && (it.Type == "面试邀请" || it.Type == "企业回复"));
                    count = temp.Count();
                    list = temp.Skip((page - 1) * limit).Take(limit).ToList();
                }
                else
                {
                    var temp = Db.Queryable<Notice>().Where(it => it.TargetId == userId && (it.Type == "简历投递" || it.Type == "求职者回复"));
                    count = temp.Count();
                    list = temp.Skip((page - 1) * limit).Take(limit).ToList();
                }
            }
            else
            {
                // 分页操作，Skip()跳过前面数据项
                if (userPower == "求职者")
                {
                    var temp = Db.Queryable<Notice>().Where(it => it.TargetId == userId && (it.Type == "面试邀请" || it.Type == "企业回复") && it.Content.Contains(search));
                    count = temp.Count();
                    list = temp.Skip((page - 1) * limit).Take(limit).ToList();
                }
                else
                {
                    var temp = Db.Queryable<Notice>().Where(it => it.TargetId == userId && (it.Type == "简历投递" || it.Type == "求职者回复") && it.Content.Contains(search));
                    count = temp.Count();
                    list = temp.Skip((page - 1) * limit).Take(limit).ToList();
                }
            }

            // 参数必须一一对应，JsonRequestBehavior.AllowGet一定要加，表单要求code返回0
            return Json(new { code = 0, msg = string.Empty, count, data = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 发出拒绝消息.
        /// </summary>
        /// <param name="userId">用户.</param>
        /// <param name="companyId">企业.</param>
        /// <returns>Json.</returns>
        public ActionResult RefuseSeeker(int userId, int companyId)
        {
            Notice old = Db.Queryable<Notice>().Where(it => it.SourceId == userId && it.TargetId == companyId && it.IsReply == 0).Single();
            old.IsReply = 1;
            Db.Updateable(old).ExecuteCommand();

            Company company = Db.Queryable<Company>().Where(it => it.Id == companyId).Single();
            Notice notice = new Notice
            {
                SourceId = companyId,
                TargetId = userId,
                Time = DateTime.Now.ToString(),
                Content = company.Name + "婉拒了您的投递，请另寻他家，加油",
                Type = "求职者回复",
                IsReply = 1,
            };
            Db.Insertable(notice).ExecuteReturnIdentity();

            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 发出拒绝消息.
        /// </summary>
        /// <param name="userId">用户.</param>
        /// <param name="seekerId">求职者.</param>
        /// <returns>Json.</returns>
        public ActionResult RefuseCompany(int userId, int seekerId)
        {
            Notice old = Db.Queryable<Notice>().Where(it => it.SourceId == userId && it.TargetId == seekerId && it.IsReply == 0).Single();
            old.IsReply = 1;
            Db.Updateable(old).ExecuteCommand();

            Seeker seeker = Db.Queryable<Seeker>().Where(it => it.Id == seekerId).Single();
            Notice notice = new Notice
            {
                SourceId = seekerId,
                TargetId = userId,
                Time = DateTime.Now.ToString(),
                Content = seeker.Name + "婉拒了您的面试邀请，请另寻千里马",
                Type = "企业回复",
                IsReply = 1,
            };
            Db.Insertable(notice).ExecuteReturnIdentity();

            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 答应面试消息.
        /// </summary>
        /// <param name="userId">用户.</param>
        /// <param name="seekerId">求职者.</param>
        /// <returns>Json.</returns>
        public ActionResult Agree(int userId, int seekerId)
        {
            Notice old = Db.Queryable<Notice>().Where(it => it.SourceId == userId && it.TargetId == seekerId && it.IsReply == 0).Single();
            old.IsReply = 1;
            Db.Updateable(old).ExecuteCommand();

            Seeker seeker = Db.Queryable<Seeker>().Where(it => it.Id == seekerId).Single();
            Notice notice = new Notice
            {
                SourceId = seekerId,
                TargetId = userId,
                Time = DateTime.Now.ToString(),
                Content = seeker.Name + "答应了您的面试邀请，请及时安排好面试时间和邮件通知",
                Type = "求职者回复",
                IsReply = 1,
            };
            Db.Insertable(notice).ExecuteReturnIdentity();

            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

    }
}