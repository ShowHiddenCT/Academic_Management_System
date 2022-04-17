using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.filters;
using WebApplication3.Models;

namespace WebApplication3.Areas.student.Controllers
{
    //[CustomAuthorizationFilterAttribute]
    public class tb_studentController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: student/tb_student
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            var student = db.tb_student.ToArray();
            var teacher = db.tb_teacher.ToArray();
            var dept = db.tb_dept.ToArray();
            //ViewBag.student = id;
            ViewBag.studentCount = student.Length;
            ViewBag.teacherCount = teacher.Length;
            ViewBag.dept = dept.Length;
            if (Request.IsAjaxRequest())
            {
                return PartialView(tb_student);
            }
            return View(tb_student);
        }

        // GET: student/tb_student/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            return View(tb_student);
        }

        // GET: student/tb_student/Create
        public ActionResult Create()
        {
            ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "DeptNo");
            return View();
        }

        // POST: student/tb_student/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentNo,StudentPasswd,MajorNo,StudentName,StudentSex,StudentBirthday,Studentphoto,StudentId,StudentTel")] tb_student tb_student)
        {
            if (ModelState.IsValid)
            {
                db.tb_student.Add(tb_student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "DeptNo", tb_student.MajorNo);
            return View(tb_student);
        }

        // GET: student/tb_student/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "DeptNo", tb_student.MajorNo);
            return View(tb_student);
        }

        // POST: student/tb_student/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentNo,StudentPasswd,MajorNo,StudentName,StudentSex,StudentBirthday,Studentphoto,StudentId,StudentTel")] tb_student tb_student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "DeptNo", tb_student.MajorNo);
            return View(tb_student);
        }

        // GET: student/tb_student/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            return View(tb_student);
        }

        // POST: student/tb_student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_student tb_student = db.tb_student.Find(id);
            db.tb_student.Remove(tb_student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //学生选课操作
        

        public ActionResult DeleteCourse(string id)
        {
            var item = id.Split(',');
            tb_stucourse deletcourse = new tb_stucourse();
            var t = db.tb_stucourse.Include(m => m.tb_teacher)
                .Include(m => m.tb_course);
            var cnt = item[0];
            var cnt2 = item[1];
            var selectcourse = db.tb_stucourse.Where(x => x.CourseNo == cnt && x.StudentNo == cnt2).ToList();
            
            if (selectcourse.Count() == 0)
            {
                return this.JavaScript("alert('你还未选过该课程')");
            }
            deletcourse = selectcourse[0];
            db.tb_stucourse.Remove(deletcourse);
            db.SaveChanges();
            return this.JavaScript("alert('成功退选')");
        }

        public ActionResult showCourse(string id)
        {

            var tb_stucourse = db.tb_stucourse.Include(t => t.tb_teacher).Include(t => t.tb_course).Include(t => t.tb_student);
            var show = db.tb_stucourse.Where(x => x.StudentNo == id);
            if (Request.IsAjaxRequest())
            {
                return PartialView(show.ToList());
            }
            return View(show.ToList());
        }
        public ActionResult Course(string id)
        {
            ViewBag.studentNo = id;
            var tb_course = db.tb_course.Include(t => t.tb_teacher);
            if (Request.IsAjaxRequest())
            {
                return PartialView(tb_course.ToList());
            }
            return View(tb_course.ToList());
        }

        public ActionResult selectCourse(string id)
        {
            var item = id.Split(',');
            tb_stucourse stucourse = new tb_stucourse();
            var t = db.tb_stucourse.Include(m => m.tb_teacher)
                .Include(m => m.tb_course);
            var cnt = item[0];
            var cnt2 = item[1];
            var now = db.tb_course.Find(cnt);
            var selectcourse = db.tb_stucourse.Where(x => x.CourseNo == cnt && x.StudentNo == cnt2);
            var scourse = db.tb_stucourse.Where(x => x.StudentNo == cnt2);
            
            if (selectcourse.Count() != 0)
            {
                return this.JavaScript("alert('你已选过该课程')");
                //return RedirectToAction("Course", new { id = item[1]});
            }
            foreach (var s in scourse)
            {
                var st = db.tb_course.Find(s.CourseNo);
                if (now.CourseTime == st.CourseTime)
                {
                    return this.JavaScript("alert('时间冲突，无法选中')");
                }

            }
            stucourse.StudentNo = item[1];
            stucourse.CourseNo = item[0];
            stucourse.TeacherNo = item[2];
            db.tb_stucourse.Add(stucourse);
            db.SaveChanges();
            return this.JavaScript("alert('成功选中')");

            //var tb_stucourse = db.tb_stucourse.Include(t => t.tb_teacher).Include(t => t.tb_course).Include(t => t.tb_student);
            //var show = db.tb_stucourse.Where(x => x.StudentNo == stuNo);
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        foreach (var v in show)
            //        {
            //            tb_stucourse stucourse = new tb_stucourse();
            //            string k = "a" + v.Id.ToString();
            //            int flag = int.Parse(Request.Form["Status"]);
            //            if (flag == 1)
            //            {
            //                stucourse.CourseNo = v.CourseNo;
            //                stucourse.StudentNo = v.StudentNo;
            //                db.tb_stucourse.Add(stucourse);
            //                db.SaveChanges();
            //            }


            //        }
            //    }
            //    catch (DataException err)
            //    {
            //        ModelState.AddModelError("StudentID", "出错了。\n" + err.Message);
            //    }
            //    return RedirectToAction("Index", new { id = stuNo });
            //}
            //return View();


        }


        public ActionResult StudentInfo(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_student tb_student = db.tb_student.Find(id);
            if (tb_student == null)
            {
                return HttpNotFound();
            }
            // ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "DeptNo", tb_student.MajorNo);
            if (Request.IsAjaxRequest())
            {
                return PartialView(tb_student);
            }
            return View(tb_student);
        }

        [HttpPost, ActionName("StudentInfo")]
        public ActionResult StudentInfo(string id, HttpPostedFileBase images)
        {
            if (images != null)//成功
            {
                //找到对象
                tb_student tb_student = db.tb_student.Find(id);
                if (tb_student != null)
                {
                    tb_student.Studentphoto = images.FileName;
                    db.SaveChanges();
                    images.SaveAs(Server.MapPath("~/Pictures/" + images.FileName));
                    return View(tb_student);
                }
            }
            else//失败
            {

            }
            return View();
        }

        public ActionResult ShowScore(string id)
        {
            tb_student tb_student = db.tb_student.Find(id);
            
            var cnt1 = tb_student.MajorNo;
            tb_major tb_Major = db.tb_major.Find(cnt1);
          
            var cnt2 = tb_Major.DeptNo;
            tb_dept tb_Dept = db.tb_dept.Find(cnt2);
            ViewBag.dept = tb_Dept.DeptName;
            ViewBag.major = tb_Major.MajorName;
            ViewBag.studentNo = tb_student.StudentNo;
            ViewBag.studentName = tb_student.StudentName;
            var tb_stucourse = db.tb_stucourse.Include(t => t.tb_teacher).Include(t => t.tb_course).Include(t => t.tb_student);
            var show = db.tb_stucourse.Where(x => x.StudentNo == id);
            foreach(var item in show)
            {
                if (item.Grade == null)
                {
                    return this.JavaScript("alert('有任课老师还未录入成绩，请等待老师录入后再来查看')");
                }
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(show.ToList());
            }
            return View(show.ToList());
        }

        public ActionResult PayExpense(string id)
        {
            ViewBag.studentNo = id;
            if (Request.IsAjaxRequest())
            {
                return PartialView(db.tb_expense.ToList());
            }
            return View(db.tb_expense.ToList());
        }

        public ActionResult Paying(string id)
        {
            var item = id.Split(',');           
            var cnt = item[0];
            tb_student tb_student = db.tb_student.Find(cnt);
            ViewBag.studentName=tb_student.StudentName;
            ViewBag.sumExpense = item[1];
            return View(tb_student);
        }



        //[HttpPost, ActionName("Paying")]
        //[ValidateAntiForgeryToken]
        public ActionResult Payof(string id)
        {
            tb_payment payment = new tb_payment();
            var tb_payment = db.tb_payment.Where(x => x.StudentNo == id);
            if(tb_payment.Count()!=0)
            {
                return this.JavaScript("alert('你已交过费，请勿重复交费')");
               
            }
            if (ModelState.IsValid)
            {
                payment.StudentNo = id;
                db.tb_payment.Add(payment);
                db.SaveChanges();
                return this.JavaScript("alert('缴费成功')");
              
            }
            return this.JavaScript("alert('缴费失败')");
           

        }
        //  专业信息  学院信息
        public ActionResult majorinfo(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_major tb_major = db.tb_major.Find(id);
            if (tb_major == null)
            {
                return HttpNotFound();
            }
            return View(tb_major);
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
