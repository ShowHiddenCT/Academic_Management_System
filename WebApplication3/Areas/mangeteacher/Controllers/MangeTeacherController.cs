using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Areas.mangeteacher.Controllers
{
    public class MangeTeacherController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: mangeteacher/tb_teacher
        public ActionResult Index()
        {
            //var tb_teacher = db.tb_teacher.Include(t => t.tb_dept);
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }


        public ActionResult SelectTeachers()
        {
                List<SelectTeacher> ListTeacher = new List<SelectTeacher>();
                string TeacherNo = null;
                string DeptName = null;
                string TeacherTitle = null;
                TeacherNo = Request.Form["No"];
                TeacherTitle = Request.Form["Major"];
                DeptName = Request.Form["Dept"];
                var temp = from tbTeacher in db.tb_teacher
                           join tbdept in db.tb_dept on tbTeacher.DeptNo equals tbdept.DeptNo
                           select new SelectTeacher()
                           {
                               TeacherNo = tbTeacher.TeacherNo,
                               TeacherName = tbTeacher.TeacherName,
                               TeacherSex = tbTeacher.TeacherSex,
                               TeacherBirthday = tbTeacher.TeacherBirthday.ToString(),
                               TeacherId = tbTeacher.TeacherId,
                               TeacherTel = tbTeacher.TeacherTel,
                               TeacherTitle = tbTeacher.TeacherTitle,
                               DeptName = tbdept.DeptName,
                           };
                if (TeacherNo != "")
                {
                    temp = temp.Where(m => m.TeacherNo == TeacherNo);
                }
                else
                {
                    if (TeacherTitle != "")
                        temp = temp.Where(m => m.TeacherTitle == TeacherTitle);
                    if (DeptName != "")
                        temp = temp.Where(m => m.DeptName == DeptName);
                }

                // var inTotalRow = temp.Count();
                ListTeacher = temp.ToList();
                //ListStudent.Clear();
                return PartialView("Index",ListTeacher);

        }

        // GET: mangeteacher/tb_teacher/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_teacher tb_teacher = db.tb_teacher.Find(id);
            if (tb_teacher == null)
            {
                return HttpNotFound();
            }
            return View(tb_teacher);
        }

        // GET: mangeteacher/tb_teacher/Create
        public ActionResult Create()
        {
            tb_teacher teacher = new tb_teacher();
            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName");
            return View(teacher);
        }

        // POST: mangeteacher/tb_teacher/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherNo,TeacherPasswd,DeptNo,TeacherName,TeacherSex,TeacherBirthday,TeacherTitle,Teacherphoto,TeacherId,TeacherTel")] tb_teacher tb_teacher)
        {
            if (ModelState.IsValid)
            {
                tb_teacher.TeacherSex = Request.Form["sex"];
                tb_teacher.TeacherBirthday = DateTime.Parse(Request.Form["BirthDate"]);
                db.tb_teacher.Add(tb_teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName", tb_teacher.DeptNo);
            return View(tb_teacher);
        }

        // GET: mangeteacher/tb_teacher/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_teacher tb_teacher = db.tb_teacher.Find(id);
            if (tb_teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName", tb_teacher.DeptNo);
            return View(tb_teacher);
        }

        // POST: mangeteacher/tb_teacher/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherNo,TeacherPasswd,DeptNo,TeacherName,TeacherSex,TeacherBirthday,TeacherTitle,Teacherphoto,TeacherId,TeacherTel")] tb_teacher tb_teacher)
        {
            if (ModelState.IsValid)
            {
                tb_teacher.TeacherSex = Request.Form["sex"];
                tb_teacher.TeacherBirthday = DateTime.Parse(Request.Form["BirthDate"]);
                db.Entry(tb_teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName", tb_teacher.DeptNo);
            return View(tb_teacher);
        }

        // GET: mangeteacher/tb_teacher/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_teacher tb_teacher = db.tb_teacher.Find(id);
            if (tb_teacher == null)
            {
                return HttpNotFound();
            }
            return View(tb_teacher);
        }

        // POST: mangeteacher/tb_teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_teacher tb_teacher = db.tb_teacher.Find(id);
            db.tb_teacher.Remove(tb_teacher);
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
