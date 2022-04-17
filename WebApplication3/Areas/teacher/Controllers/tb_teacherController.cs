using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Areas.teacher.Model;
using WebApplication3.filters;
using WebApplication3.logic;
using WebApplication3.Models;

namespace WebApplication3.Areas.teacher.Controllers
{
    [CustomAuthorizationFilterAttribute]
    public class tb_teacherController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: teacher/tb_teacher
        public ActionResult Index(string id)
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
            var student = db.tb_student.ToArray();
            var teacher = db.tb_teacher.ToArray();
            var dept = db.tb_dept.ToArray();
            ViewBag.studentCount = student.Length;
            ViewBag.teacherCount = teacher.Length;
            ViewBag.dept = dept.Length;
            if (Request.IsAjaxRequest())
            {
                return PartialView(tb_teacher);
            }
            return View(tb_teacher);
        }
        // GET: teacher/tb_teacher/Details/5
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

        [HttpPost]
        public ActionResult AddScore(HttpPostedFileBase ScoreFile)
        {
            DataTable excelTable = new DataTable();
            var fileName = ScoreFile.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "excel"));
            string path = Path.Combine(filePath, fileName);
            ScoreFile.SaveAs(path);

            excelTable = ImportExcel.GetExcelDataTable(path);
            var RowLength = excelTable.Rows.Count;
            tb_student stu = new tb_student();
            string TeacheNo=null;
            for (int i = 0; i < RowLength; i++)
            {
                var studentNo = excelTable.Rows[i][0].ToString();
                var CourseNo = excelTable.Rows[i][1].ToString();
                TeacheNo = excelTable.Rows[i][2].ToString();
                tb_stucourse tb_Stucourse = db.tb_stucourse.Where(x => x.StudentNo == studentNo && x.CourseNo == CourseNo && x.TeacherNo == TeacheNo).First();
                tb_Stucourse.Grade = Convert.ToInt32(excelTable.Rows[i][3].ToString());
                db.SaveChanges();
            }
            string url = Url.Action("Index", "tb_teacher", new { id = TeacheNo });
            return Redirect(url);

        }

        // GET: teacher/tb_teacher/Create
        public ActionResult Create()
        {
            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName");
            return View();
        }

        // POST: teacher/tb_teacher/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherNo,TeacherPasswd,DeptNo,TeacherName,TeacherSex,TeacherBirthday,TeacherTitle,Teacherphoto,TeacherId,TeacherTel")] tb_teacher tb_teacher)
        {
            if (ModelState.IsValid)
            {
                db.tb_teacher.Add(tb_teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName", tb_teacher.DeptNo);
            return View(tb_teacher);
        }

        // GET: teacher/tb_teacher/Edit/5
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

        // POST: teacher/tb_teacher/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherNo,TeacherPasswd,DeptNo,TeacherName,TeacherSex,TeacherBirthday,TeacherTitle,Teacherphoto,TeacherId,TeacherTel")] tb_teacher tb_teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName", tb_teacher.DeptNo);
            return View(tb_teacher);
        }

        // GET: teacher/tb_teacher/Delete/5
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

        // POST: teacher/tb_teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_teacher tb_teacher = db.tb_teacher.Find(id);
            db.tb_teacher.Remove(tb_teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult teacherInfo(string id)
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
            // ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "DeptNo", tb_student.MajorNo);
            if (Request.IsAjaxRequest())
            {
                return PartialView(tb_teacher);
            }
            return View(tb_teacher);
        }

        [HttpPost, ActionName("teacherInfo")]
        public ActionResult TeacherInfo(string id, HttpPostedFileBase images)
        {
            if (images != null)//成功
            {
                //找到对象
                tb_teacher tb_teacher = db.tb_teacher.Find(id);
                if (tb_teacher != null)
                {
                    tb_teacher.Teacherphoto = images.FileName;
                    db.SaveChanges();
                    images.SaveAs(Server.MapPath("~/Pictures/" + images.FileName));
                    return View(tb_teacher);
                }
            }
            else//失败
            {

            }
            return View();
        }

        public ActionResult Score(string id)
        {
            ViewBag.teacherNo = id;
            var tb_stucourse = db.tb_stucourse
                .Include(t => t.tb_teacher)
                .Include(t => t.tb_course)
                .Include(t => t.tb_student);
            var tea_score = tb_stucourse.Where(x => x.TeacherNo == id);
            if (Request.IsAjaxRequest())
            {
                return PartialView(tea_score.ToList());
            }
            return View(tea_score.ToList());
        }

        [HttpPost, ActionName("Score")]
        [ValidateAntiForgeryToken]
        public ActionResult ScorePost(string Id)
        {
            var tb_stucourse = db.tb_stucourse
                .Include(t => t.tb_teacher)
                .Include(t => t.tb_course)
                .Include(t => t.tb_student);
            var tea_score = tb_stucourse.Where(x => x.TeacherNo == Id).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var v in tea_score)
                    {
                        string k = "a" + v.Id.ToString();
                        v.Grade = int.Parse(Request[k]);
                        db.Entry(v).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                catch (DataException err)
                {
                    ModelState.AddModelError("StudentID", "出错了。\n" + err.Message);
                }
                return RedirectToAction("Index",new { id = Id });
            }
            return View();
        }

        //public ActionResult GetUser(string UserName = "")
        //{

        //    List<tb_teacher> list = new List<tb_teacher>();
        //    DataTable dt = MyDBHelper.ExecuteQuery("select u.id,u.UserName,u.PassWord,r.rolename from [User] u left join userrole us on u.id = us.userid left join role r on us.roleid = r.id where u.UserName like '%" + UserName + "%'");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        list.Add(new (tb) { id = dt.Rows[i]["id"].ToString(), UserName = dt.Rows[i]["UserName"].ToString(), PassWord = dt.Rows[i]["PassWord"].ToString(), rolename = dt.Rows[i]["rolename"].ToString() });
        //    }
        //    LayuiModel m = new LayuiModel() { code = 0, count = list.Count, data = list };
        //    //var json = JsonConvert.SerializeObject(list);
        //    return Json(m);
        //}

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
