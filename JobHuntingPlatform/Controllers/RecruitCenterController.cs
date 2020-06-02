using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 招聘中心界面的控制器.
    /// </summary>
    public class RecruitCenterController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入招聘中心界面.
        /// </summary>
        /// <returns>招聘中心界面.</returns>
        public ActionResult RecruitCenter()
        {
            return View();
        }

        /// <summary>
        /// 获取招聘信息.
        /// </summary>
        /// <param name="page">总页数.</param>
        /// <param name="limit">一页多少行数据.</param>
        /// <param name="companyId">企业编号.</param>
        /// <param name="search">查询字段.</param>
        /// <returns>企业列表.</returns>
        public ActionResult GetRecruit(int page, int limit,int companyId ,string search = null)
        {
            List<RecruitmentDTO> list = null;
            int count;
            // 分页操作，Skip()跳过前面数据项
            if (string.IsNullOrEmpty(search))
            {
                // 分页操作，Skip()跳过前面数据项
                var temp = Db.Queryable<Recruitment, Company>((r, c) => new object[] 
                {
                    JoinType.Inner, r.CompanyId == c.Id && r.CompanyId == companyId,
                }).Select((r, c) => new RecruitmentDTO
                {
                    Id = r.Id,
                    CompanyId = r.CompanyId,
                    Offer = r.Offer,
                    Require = r.Require,
                    Number = r.Number,
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
                    JoinType.Inner, r.CompanyId == c.Id && r.CompanyId == companyId && r.Offer.Contains(search),
                }).Select((r, c) => new RecruitmentDTO
                {
                    Id = r.Id,
                    CompanyId = r.CompanyId,
                    Offer = r.Offer,
                    Require = r.Require,
                    Number = r.Number,
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

        /// <summary>
        /// 新建招聘.
        /// </summary>
        /// /// <param name="recruit">传入数据.</param>
        /// <returns>Json.</returns>
        public ActionResult AddRecruit(Recruitment recruit)
        {
            // 自增列用法
            recruit.Time = DateTime.Now.ToString();
            Db.Insertable(recruit).ExecuteReturnIdentity();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改招聘信息.
        /// </summary>
        /// <param name="recruit">招聘信息.</param>
        /// <returns>Json.</returns>
        public ActionResult EditRecruit(Recruitment recruit)
        {
            Db.Updateable(recruit).ExecuteCommand();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除招聘信息.
        /// </summary>
        /// <param name="recruitId">招聘编号.</param>
        /// <returns>Json.</returns>
        public ActionResult DelRecruit(int recruitId)
        {
            Db.Deleteable<Recruitment>().Where(it => it.Id == recruitId).ExecuteCommand();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }
    }
}