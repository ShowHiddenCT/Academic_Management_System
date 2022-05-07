using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Areas.course.Controllers
{
    public class tb_courseController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: course/tb_course
        public ActionResult Index()
        {
            
            var tb_course = db.tb_course.Include(t => t.tb_teacher);
            if(Request.IsAjaxRequest())
            {
                return PartialView(tb_course.ToList());
            }
            return View(tb_course.ToList());
        }

        // GET: course/tb_course/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_course tb_course = db.tb_course.Find(id);
            if (tb_course == null)
            {
                return HttpNotFound();
            }
            return View(tb_course);
        }

        // GET: course/tb_course/Create
        public ActionResult Create()
        {
            ViewBag.CourseTeacher = new SelectList(db.tb_teacher, "TeacherNo", "TeacherName");
            return View();
        }

        // POST: course/tb_course/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseNo,CourseName,CourseTeacher,CourseCredit,CourseTime,CoursePlace,CourseType,CourseHour")] tb_course tb_course)
        {
            if (ModelState.IsValid)
            {
                db.tb_course.Add(tb_course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseTeacher = new SelectList(db.tb_teacher, "TeacherNo", "TeacherName", tb_course.CourseTeacher);
            return View(tb_course);
        }

        // GET: course/tb_course/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_course tb_course = db.tb_course.Find(id);
            if (tb_course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseTeacher = new SelectList(db.tb_teacher, "TeacherNo", "TeacherName", tb_course.CourseTeacher);
            return View(tb_course);
        }

        // POST: course/tb_course/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseNo,CourseName,CourseTeacher,CourseCredit,CourseTime,CoursePlace,CourseType,CourseHour")] tb_course tb_course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseTeacher = new SelectList(db.tb_teacher, "TeacherNo", "TeacherName", tb_course.CourseTeacher);
            return View(tb_course);
        }

        // GET: course/tb_course/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_course tb_course = db.tb_course.Find(id);
            if (tb_course == null)
            {
                return HttpNotFound();
            }
            return View(tb_course);
        }

        // POST: course/tb_course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_course tb_course = db.tb_course.Find(id);
            db.tb_course.Remove(tb_course);
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
