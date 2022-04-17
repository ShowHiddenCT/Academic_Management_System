using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Areas.Admin.Controllers
{
    public class tb_AdminController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: Admin/tb_Admin
        public ActionResult Index()
        {
            var student = db.tb_student.ToArray();
            var teacher = db.tb_teacher.ToArray();
            var dept = db.tb_dept.ToArray();
            ViewBag.studentCount = student.Length;
            ViewBag.teacherCount = teacher.Length;
            ViewBag.dept = dept.Length;
            if(Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }

        // GET: Admin/tb_Admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Admin tb_Admin = db.tb_Admin.Find(id);
            if (tb_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tb_Admin);
        }

        // GET: Admin/tb_Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/tb_Admin/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdminNo,AdminPasswd")] tb_Admin tb_Admin)
        {
            if (ModelState.IsValid)
            {
                db.tb_Admin.Add(tb_Admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_Admin);
        }

        // GET: Admin/tb_Admin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Admin tb_Admin = db.tb_Admin.Find(id);
            if (tb_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tb_Admin);
        }

        // POST: Admin/tb_Admin/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdminNo,AdminPasswd")] tb_Admin tb_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_Admin);
        }

        // GET: Admin/tb_Admin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_Admin tb_Admin = db.tb_Admin.Find(id);
            if (tb_Admin == null)
            {
                return HttpNotFound();
            }
            return View(tb_Admin);
        }

        // POST: Admin/tb_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_Admin tb_Admin = db.tb_Admin.Find(id);
            db.tb_Admin.Remove(tb_Admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
  
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
