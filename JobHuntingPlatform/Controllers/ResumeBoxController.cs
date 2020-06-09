using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 简历邮箱界面的控制器.
    /// </summary>
    public class ResumeBoxController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入简历邮箱界面.
        /// </summary>
        /// <returns>简历邮箱界面.</returns>
        public ActionResult ResumeBox()
        {
            return View();
        }

        /// <summary>
        /// 进入简历界面.
        /// </summary>
        /// <returns>简历界面.</returns>
        public ActionResult ResumeView(int userId)
        {
            ViewBag.ResumePath = Db.Queryable<Seeker>().Where(it => it.Id == userId).Single().ResumePath;
            return View();
        }

        /// <summary>
        /// 获取简历信息.
        /// </summary>
        /// <param name="page">总页数.</param>
        /// <param name="limit">一页多少行数据.</param>
        /// <param name="search">查询字段.</param>
        /// <param name="userId">查询者.</param>
        /// <returns>企业列表.</returns>
        public ActionResult GetResume(int page, int limit, int userId, string search = null)
        {
            List<ResumeDTO> list = null;
            int count;
            if (string.IsNullOrEmpty(search))
            {
                // 分页操作，Skip()跳过前面数据项
                var result = Db.Queryable<Company, Recruitment, Record, Seeker>((c, r1, r2, s) => new object[]
                {
                    JoinType.Inner, c.Id == r1.CompanyId && c.Id == userId,
                    JoinType.Inner, r1.Id == r2.RecruitmentId,
                    JoinType.Inner, r2.SeekerId == s.Id,
                }).Select((c, r1, r2, s) => new ResumeDTO
                {
                    SeekerId = s.Id,
                    Id = r2.Id,
                    Name = s.Name,
                    Time = r2.Time,
                    ResumePath = s.ResumePath,
                });
                count = result.Count();
                list = result.Skip((page - 1) * limit).Take(limit).ToList();
            }
            else
            {
                // 分页操作，Skip()跳过前面数据项
                var result = Db.Queryable<Company, Recruitment, Record, Seeker>((c, r1, r2, s) => new object[]
                {
                    JoinType.Inner, r1.CompanyId == c.Id && c.Id == userId,
                    JoinType.Inner, r1.Id == r2.RecruitmentId,
                    JoinType.Inner, r2.SeekerId == s.Id && s.Name.Contains(search),
                }).Select((c, r1, r2, s) => new ResumeDTO
                {
                    Id = r2.Id,
                    Name = s.Name,
                    Time = r2.Time,
                    ResumePath = s.ResumePath,
                });
                count = result.Count();
                list = result.Skip((page - 1) * limit).Take(limit).ToList();
            }

            // 参数必须一一对应，JsonRequestBehavior.AllowGet一定要加，表单要求code返回0
            return Json(new { code = 0, msg = string.Empty, count, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}