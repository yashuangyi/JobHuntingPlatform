using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 招聘中心管理界面的控制器.
    /// </summary>
    public class RecruitManageController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入招聘中心管理界面.
        /// </summary>
        /// <returns>招聘中心管理界面.</returns>
        public ActionResult RecruitManage()
        {
            return View();
        }

        /// <summary>
        /// 获取招聘信息.
        /// </summary>
        /// <param name="page">总页数.</param>
        /// <param name="limit">一页多少行数据.</param>
        /// <param name="search">查询字段.</param>
        /// <returns>企业列表.</returns>
        public ActionResult GetRecruit(int page, int limit, string search = null)
        {
            List<RecruitmentDTO> list = null;
            int count;
            // 分页操作，Skip()跳过前面数据项
            if (string.IsNullOrEmpty(search))
            {
                // 分页操作，Skip()跳过前面数据项
                var temp = Db.Queryable<Recruitment, Company>((r, c) => new object[]
                {
                    JoinType.Inner, r.CompanyId == c.Id,
                }).Select((r, c) => new RecruitmentDTO
                {
                    Id = r.Id,
                    CompanyId = r.CompanyId,
                    Offer = r.Offer,
                    Number = r.Number,
                    Require = r.Require,
                    Time = r.Time,
                    Name = c.Name,
                    Type = c.Type,
                    Address = c.Address,
                    Phone = c.Phone,
                });
                count = temp.Count();
                list = temp.Skip((page - 1) * limit).Take(limit).ToList();
            }
            else
            {
                var temp = Db.Queryable<Recruitment, Company>((r, c) => new object[]
                {
                    JoinType.Inner, r.CompanyId == c.Id && (r.Offer.Contains(search)||c.Name.Contains(search)),
                }).Select((r, c) => new RecruitmentDTO
                {
                    Id = r.Id,
                    CompanyId = r.CompanyId,
                    Offer = r.Offer,
                    Number = r.Number,
                    Require = r.Require,
                    Time = r.Time,
                    Name = c.Name,
                    Type = c.Type,
                    Address = c.Address,
                    Phone = c.Phone,
                });
                count = temp.Count();
                list = temp.Skip((page - 1) * limit).Take(limit).ToList();
            }

            // 参数必须一一对应，JsonRequestBehavior.AllowGet一定要加，表单要求code返回0
            return Json(new { code = 0, msg = string.Empty, count, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}