using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 求职广场界面的控制器.
    /// </summary>
    public class JobPlazaController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入求职广场界面.
        /// </summary>
        /// <returns>求职广场界面.</returns>
        public ActionResult JobPlaza()
        {
            return View();
        }

        /// <summary>
        /// 投递简历.
        /// </summary>
        /// <param name="userId">用户.</param>
        /// <param name="recruitmentId">招聘信息.</param>
        /// <returns>Json.</returns>
        public ActionResult Deliver(int userId, int recruitmentId, int companyId)
        {
            if(Db.Queryable<Notice>().Where(it => it.SourceId == userId && it.TargetId == companyId && it.Type == "简历投递").Single() != null)
            {
                return Json(new { code = 400 }, JsonRequestBehavior.AllowGet);
            }

            Record record = new Record
            {
                SeekerId = userId,
                RecruitmentId = recruitmentId,
                Time = DateTime.Now.ToString(),
            };
            Db.Insertable(record).ExecuteReturnIdentity();

            Seeker seeker = Db.Queryable<Seeker>().Where(it => it.Id == userId).Single();
            Recruitment recruitment = Db.Queryable<Recruitment>().Where(it => it.Id == recruitmentId).Single();
            Notice notice = new Notice
            {
                SourceId = userId,
                TargetId = companyId,
                Time = DateTime.Now.ToString(),
                Content = seeker.Name + (seeker.Sex=="男"?"先生":"女士") + "-投递贵司-" + recruitment.Offer + "岗位，详情内容请查看简历邮箱",
                Type = "简历投递",
                IsReply = 0,
            };
            Db.Insertable(notice).ExecuteReturnIdentity();

            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

    }
}