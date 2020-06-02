using JobHuntingPlatform.DB;
using JobHuntingPlatform.Models;
using SqlSugar;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace JobHuntingPlatform.Controllers
{
    /// <summary>
    /// 登录界面的控制器.
    /// </summary>
    public class LoginController : Controller
    {
        private static readonly SqlSugarClient Db = DataBase.CreateClient();

        /// <summary>
        /// 进入后台登录界面.
        /// </summary>
        /// <returns>后台登录界面.</returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 后台登录校验.
        /// </summary>
        /// <param name="user">后台登录信息.</param>
        /// <returns>状态码.</returns>
        public ActionResult Check(Admin user)
        {
            var account = user.Account;
            var password = user.Password;
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                return Json(new { code = 401 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var login = Db.Queryable<Admin>().Where(it => it.Account == account && it.Password == password).Single();
                if (login != null)
                {
                    Session.Add("userId", login.Id);
                    Session.Add("userPower", "管理员");
                    Session.Add("userName", login.Name);
                    return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var login2 = Db.Queryable<Company>().Where(it => it.Account == account && it.Password == password).Single();
                    if (login2 != null)
                    {
                        if (login2.IsPass == 0)
                        {
                            return Json(new { code = 402 }, JsonRequestBehavior.AllowGet);
                        }
                        Session.Add("userId", login2.Id);
                        Session.Add("userPower", "企业");
                        Session.Add("userName", login2.Name);
                        return Json(new { code = 202 }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var login3 = Db.Queryable<Seeker>().Where(it => it.Account == account && it.Password == password).Single();
                        if (login3 != null)
                        {
                            Session.Add("userId", login3.Id);
                            Session.Add("userPower", "求职者");
                            Session.Add("userName", login3.Name);
                            return Json(new { code = 201 }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { code = 404 }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
        }

        /// <summary>
        /// 上传简历.
        /// </summary>
        /// <returns>Json.</returns>
        public ActionResult UploadResume()
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
        /// 新建求职者账户.
        /// </summary>
        /// /// <param name="user">传入数据.</param>
        /// <returns>Json.</returns>
        public ActionResult AddSeeker(Seeker user)
        {
            var isExist = Db.Queryable<Seeker>().Where(it => it.Account == user.Account).Single();
            if (isExist != null)
            {
                return Json(new { code = 402 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // 自增列用法
                Db.Insertable(user).ExecuteReturnIdentity();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 新建企业账户.
        /// </summary>
        /// /// <param name="user">传入数据.</param>
        /// <returns>Json.</returns>
        public ActionResult AddCompany(Company user)
        {
            var isExist = Db.Queryable<Company>().Where(it => it.Account == user.Account).Single();
            if (isExist != null)
            {
                return Json(new { code = 402 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // 自增列用法
                Db.Insertable(user).ExecuteReturnIdentity();
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}