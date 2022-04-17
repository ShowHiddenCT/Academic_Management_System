using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Areas.Login.Controllers;
using System.Drawing;
using System.Drawing.Imaging;

namespace WebApplication3.Areas.LoginWindow.Controllers
{
    public class LoginController : Controller
    {
        private JiaowuContext db = new JiaowuContext();


        public ActionResult Logindex()
        {
            //Userlogin user = new Userlogin();
            return View();
        }

        [HttpPost,ActionName("Logindex")]
        [ValidateAntiForgeryToken]
        public ActionResult Logindex(string id, string passwd)
{
            string cnt = Request.Form["identy"];
            if(cnt=="管理员")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tb_Admin userlogin = db.tb_Admin.Find(id);
                if (userlogin == null)
                {
                    ModelState.AddModelError("failed", "用户不存在，请联系管理员");
                    return View();
                }
                string verify = Request.Form["verifycode"];
                if (!string.Equals(verify, HttpContext.Session["checkCode"].ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    ModelState.AddModelError("failed", "验证码错误");
                    return View();

                }
                if (passwd.Equals(userlogin.AdminPasswd))
                {
                    HttpContext.Session["currentuser"] = userlogin;
                    return Redirect(Url.Action("Index", "tb_Admin", new { area = "Admin"}));
                }
                else
                {
                    ModelState.AddModelError("failed", "用户名或密码错误");
                    return View();
                }
            }
            else if(cnt=="学生")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tb_student userlogin = db.tb_student.Find(id);
                if (userlogin == null)
                {
                    ModelState.AddModelError("failed", "用户不存在，请联系管理员");
                    return View();
                }
                string verify = Request.Form["verifycode"];
                if (!string.Equals(verify, HttpContext.Session["checkCode"].ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    ModelState.AddModelError("failed", "验证码错误");
                    return View();

                }
                if (passwd.Equals(userlogin.StudentPasswd))
                {
                    HttpContext.Session["currentuser"] = userlogin;
                    return Redirect(Url.Action("Index", "tb_student", new { area = "student", id = userlogin.StudentNo }));
                }
                else
                {
                    ModelState.AddModelError("failed", "用户名或密码错误");
                    return View();
                }
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tb_teacher userlogin = db.tb_teacher.Find(id);
                if (userlogin == null)
                {
                    ModelState.AddModelError("failed", "用户不存在，请联系管理员");
                    return View();
                }
                string verify = Request.Form["verifycode"];
                if (!string.Equals(verify, HttpContext.Session["checkCode"].ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    ModelState.AddModelError("failed", "验证码错误");
                    return View();

                }
                if (passwd.Equals(userlogin.TeacherPasswd))
                {
                    HttpContext.Session["currentuser"] = userlogin;
                    ViewBag.teacherNo = userlogin.TeacherNo;
                    return Redirect(Url.Action("Index", "tb_teacher", new { area = "teacher" , id = userlogin.TeacherNo }));
                }
                else
                {
                    ModelState.AddModelError("failed", "用户名或密码错误");
                    return View();
                }
            }
            
        }
        public void VerifyCode()
        {
            VerifyCodeHelper v = new VerifyCodeHelper();
            string code = v.CreateVerifyCode();
            //TODO 存入本地session或者缓存中 方便验证使用
            //1、获取到图片对象，怎么输出到前台就自定义了
            Bitmap bitmap = v.CreateImageCode(code);
            base.HttpContext.Session["CheckCode"] = code;
            bitmap.Save(base.Response.OutputStream, ImageFormat.Gif);
            base.Response.ContentType = "image/gif";
        }
    }
}
