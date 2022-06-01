using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Areas.stucourse.Controllers
{
    public class tb_stucourseController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: stucourse/tb_stucourse
        public ActionResult Index()
        {
            var tb_stucourse = db.tb_stucourse.Include(t => t.tb_course).Include(t => t.tb_student).Include(t => t.tb_teacher);
            return View(tb_stucourse.ToList());
        }

        // GET: stucourse/tb_stucourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_stucourse tb_stucourse = db.tb_stucourse.Find(id);
            if (tb_stucourse == null)
            {
                return HttpNotFound();
            }
            return View(tb_stucourse);
        }

        // GET: stucourse/tb_stucourse/Create
        public ActionResult Create()
        {
            ViewBag.CourseNo = new SelectList(db.tb_course, "CourseNo", "CourseName");
            ViewBag.StudentNo = new SelectList(db.tb_student, "StudentNo", "StudentPasswd");
            ViewBag.TeacherNo = new SelectList(db.tb_teacher, "TeacherNo", "TeacherPasswd");
            return View();
        }

        // POST: stucourse/tb_stucourse/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentNo,CourseNo,TeacherNo,Grade")] tb_stucourse tb_stucourse)
        {
            if (ModelState.IsValid)
            {
                db.tb_stucourse.Add(tb_stucourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseNo = new SelectList(db.tb_course, "CourseNo", "CourseName", tb_stucourse.CourseNo);
            ViewBag.StudentNo = new SelectList(db.tb_student, "StudentNo", "StudentPasswd", tb_stucourse.StudentNo);
            ViewBag.TeacherNo = new SelectList(db.tb_teacher, "TeacherNo", "TeacherPasswd", tb_stucourse.TeacherNo);
            return View(tb_stucourse);
        }

        // GET: stucourse/tb_stucourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_stucourse tb_stucourse = db.tb_stucourse.Find(id);
            if (tb_stucourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseNo = new SelectList(db.tb_course, "CourseNo", "CourseName", tb_stucourse.CourseNo);
            ViewBag.StudentNo = new SelectList(db.tb_student, "StudentNo", "StudentPasswd", tb_stucourse.StudentNo);
            ViewBag.TeacherNo = new SelectList(db.tb_teacher, "TeacherNo", "TeacherPasswd", tb_stucourse.TeacherNo);
            return View(tb_stucourse);
        }

        // POST: stucourse/tb_stucourse/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentNo,CourseNo,TeacherNo,Grade")] tb_stucourse tb_stucourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_stucourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseNo = new SelectList(db.tb_course, "CourseNo", "CourseName", tb_stucourse.CourseNo);
            ViewBag.StudentNo = new SelectList(db.tb_student, "StudentNo", "StudentPasswd", tb_stucourse.StudentNo);
            ViewBag.TeacherNo = new SelectList(db.tb_teacher, "TeacherNo", "TeacherPasswd", tb_stucourse.TeacherNo);
            return View(tb_stucourse);
        }

        // GET: stucourse/tb_stucourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_stucourse tb_stucourse = db.tb_stucourse.Find(id);
            if (tb_stucourse == null)
            {
                return HttpNotFound();
            }
            return View(tb_stucourse);
        }

        // POST: stucourse/tb_stucourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_stucourse tb_stucourse = db.tb_stucourse.Find(id);
            db.tb_stucourse.Remove(tb_stucourse);
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
