using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 资讯管理界面的控制器.
    /// </summary>
    public class ArticleManageController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入资讯管理界面.
        /// </summary>
        /// <returns>资讯管理界面.</returns>
        public ActionResult ArticleManage()
        {
            return View();
        }

        /// <summary>
        /// 进入资讯界面.
        /// </summary>
        /// <returns>资讯界面.</returns>
        public ActionResult ArticleView(int articleId)
        {
            ViewBag.ArticleId = articleId;
            return View();
        }

        /// <summary>
        /// 获取文章信息.
        /// </summary>
        /// <param name="page">总页数.</param>
        /// <param name="limit">一页多少行数据.</param>
        /// <param name="search">查询字段.</param>
        /// <returns>文章列表.</returns>
        public ActionResult GetArticle(int page, int limit, string search = null)
        {
            List<ArticleDTO> list = null;
            int count;
            if (string.IsNullOrEmpty(search))
            {
                // 分页操作，Skip()跳过前面数据项
                var result = Db.Queryable<Article, Admin>((article, admin) => new object[]
                {
                    JoinType.Inner, article.AdminId == admin.Id,
                }).Select((article, admin) => new ArticleDTO
                {
                    Id = article.Id,
                    Name = admin.Name,
                    Time = article.Time,
                    Title = article.Title,
                    ArticlePath = article.ArticlePath,
                    IsTop = article.IsTop
                });
                count = result.Count();
                list = result.OrderBy(article => article.IsTop, OrderByType.Desc).Skip((page - 1) * limit).Take(limit).ToList();
            }
            else
            {
                // 分页操作，Skip()跳过前面数据项
                var result = Db.Queryable<Admin, Article>((admin, article) => new object[]
                {
                    JoinType.Inner, article.AdminId == admin.Id && article.Title.Contains(search),
                }).Select((admin, article) => new ArticleDTO
                {
                    Id = article.Id,
                    Name = admin.Name,
                    Time = article.Time,
                    Title = article.Title,
                    ArticlePath = article.ArticlePath,
                    IsTop = article.IsTop
                });
                count = result.Count();
                list = result.OrderBy(article => article.IsTop, OrderByType.Desc).Skip((page - 1) * limit).Take(limit).ToList();
            }

            // 参数必须一一对应，JsonRequestBehavior.AllowGet一定要加，表单要求code返回0
            return Json(new { code = 0, msg = string.Empty, count, data = list }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上传文章.
        /// </summary>
        /// <returns>Json.</returns>
        public ActionResult UploadArticle()
        {
            string resumePath = string.Empty;
            string resumeName = string.Empty;
            string msg = string.Empty;
            HttpPostedFileWrapper file = (HttpPostedFileWrapper)Request.Files[0];
            resumeName = file.FileName;
            if (string.IsNullOrEmpty(resumeName))
            {
                msg = "无效文件，请重新上传！";
                return Json(new { resumePath, msg, code = 400, photoName = string.Empty }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // 获得当前时间的string类型
                string name = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                string path = "/Source/resumes/";
                string uploadPath = Server.MapPath("~/" + path);
                string ext = Path.GetExtension(resumeName);
                string savePath = uploadPath + name + ext;
                file.SaveAs(savePath);
                resumePath = path + name + ext;
                msg = "上传成功！";
                return Json(new { resumePath, msg, code = 200, resumeName }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 新建文章.
        /// </summary>
        /// <returns>Json.</returns>
        public ActionResult AddArticle(int adminId, string title, string articlePath)
        {
            Article article = new Article
            {
                Id = 0,
                AdminId = adminId,
                Time = DateTime.Now.ToString(),
                ArticlePath = articlePath,
                Title = title,
                IsTop = 0,
            };
            // 自增列用法
            Db.Insertable(article).ExecuteReturnIdentity();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除文章.
        /// </summary>
        /// <param name="articleId">文章编号.</param>
        /// <returns>Json.</returns>
        public ActionResult DelArticle(int articleId)
        {
            Db.Deleteable<Article>().Where(it => it.Id == articleId).ExecuteCommand();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 置顶文章.
        /// </summary>
        /// <param name="articleId">文章编号.</param>
        /// <returns>Json.</returns>
        public ActionResult TopArticle(int articleId)
        {
            var article = Db.Queryable<Article>().Where(it => it.Id == articleId).Single();
            article.IsTop = 1;
            Db.Updateable(article).ExecuteCommand();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 取消置顶文章.
        /// </summary>
        /// <param name="articleId">文章编号.</param>
        /// <returns>Json.</returns>
        public ActionResult CancelTopArticle(int articleId)
        {
            var article = Db.Queryable<Article>().Where(it => it.Id == articleId).Single();
            article.IsTop = 0;
            Db.Updateable(article).ExecuteCommand();
            return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取状态.
        /// </summary>
        /// <param name="articleId">文章编号.</param>
        /// <returns>Json.</returns>
        public ActionResult GetStatus(int articleId)
        {
            var article = Db.Queryable<Article>().Where(it => it.Id == articleId).Single();
            string articlePath = article.ArticlePath;
            return Json(new { code = 200, articlePath }, JsonRequestBehavior.AllowGet);
        }
    }
}