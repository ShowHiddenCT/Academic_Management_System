using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.logic;
using WebApplication3.Models;

namespace WebApplication3.Areas.manageStudent.Controllers
{
    public class MangeStudentController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: manageStudent/MangeStudent
        public ActionResult Index()
        {
            if (Request.HttpMethod == "GET")
            {
                if(Request.IsAjaxRequest())
                {
                    return PartialView();
                }
                else
                {
                    return View();
                }
            }
            else
            {
                List<SelectStudent> ListStudent = new List<SelectStudent>();
                string StudentNo = null;
                string StudentMajor = null;
                string StudentDept = null;
            
                StudentNo = Request.Form["No"];
                StudentMajor = Request.Form["Major"];
                StudentDept = Request.Form["Dept"];
                var temp = from tbstudent in db.tb_student
                           join tbmajor in db.tb_major on tbstudent.MajorNo equals tbmajor.MajorNo
                           join tbdept in db.tb_dept on tbmajor.DeptNo equals tbdept.DeptNo
                           select new SelectStudent()
                           {
                               StudentNo = tbstudent.StudentNo,
                               StudentName = tbstudent.StudentName,
                               StudentSex = tbstudent.StudentSex,
                               StudentBirthday=tbstudent.StudentBirthday.ToString(),
                               StudentId=tbstudent.StudentId,
                               StudentTel=tbstudent.StudentTel,
                               Major = tbmajor.MajorName,
                               College = tbdept.DeptName,
                           };
                if (StudentNo != "")
                {
                    temp = temp.Where(m => m.StudentNo == StudentNo);
                }
                else
                {
                    if (StudentMajor !="")
                        temp = temp.Where(m => m.Major == StudentMajor);
                    if (StudentDept != "")
                        temp = temp.Where(m => m.College == StudentDept);
                }

                // var inTotalRow = temp.Count();
                ListStudent = temp.ToList();
                //ListStudent.Clear();
                return PartialView(temp.ToList());
            }
        }

        public ActionResult AddStudents(HttpPostedFileBase StudentsFile)
        {
            DataTable excelTable = new DataTable();
            var fileName = StudentsFile.FileName;
            var filePath = Server.MapPath(string.Format("~/{0}", "excel"));
            string path = Path.Combine(filePath, fileName);
            StudentsFile.SaveAs(path);

            excelTable = ImportExcel.GetExcelDataTable(path);
            var RowLength = excelTable.Rows.Count;


            for (int i = 0; i < RowLength; i++)
            {
                tb_student stu = new tb_student();
                stu.StudentNo = excelTable.Rows[i][0].ToString();
                stu.Studentphoto = null;

                stu.MajorNo = excelTable.Rows[i][1].ToString();
                stu.StudentName = excelTable.Rows[i][2].ToString();
                stu.StudentSex = excelTable.Rows[i][3].ToString();
                stu.StudentBirthday = Convert.ToDateTime(excelTable.Rows[i][4].ToString());
                stu.StudentTel = excelTable.Rows[i][5].ToString();
                stu.StudentId = excelTable.Rows[i][6].ToString();
                stu.StudentPasswd = stu.StudentId.Substring(9, 6);
                db.tb_student.Add(stu);
                db.SaveChanges();
            }
            return this.JavaScript("alert('添加成功')");
        }

        // GET: manageStudent/MangeStudent/Details/5
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

        // GET: manageStudent/MangeStudent/Create
        public ActionResult Create()
        {
            tb_student tb_student = new tb_student();
            ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "MajorName");
            return View(tb_student);
        }

        // POST: manageStudent/MangeStudent/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentNo,StudentPasswd,MajorNo,StudentName,StudentSex,StudentBirthday,Studentphoto,StudentId,StudentTel")] tb_student tb_student)
        {
            if (ModelState.IsValid)
            {
                tb_student.StudentSex = Request.Form["sex"];
                tb_student.StudentBirthday = DateTime.Parse(Request.Form["BirthDate"]);
                db.tb_student.Add(tb_student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "MajorName", tb_student.MajorNo);
            return View(tb_student);
        }

        // GET: manageStudent/MangeStudent/Edit/5
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
            ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "MajorName", tb_student.MajorNo);
            return View(tb_student);
        }

        // POST: manageStudent/MangeStudent/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentNo,StudentPasswd,MajorNo,StudentName,StudentSex,StudentBirthday,Studentphoto,StudentId,StudentTel")] tb_student tb_student)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(tb_student).State = EntityState.Modified;
                tb_student.StudentSex= Request.Form["sex"];
                tb_student.StudentBirthday =DateTime.Parse(Request.Form["BirthDate"]);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorNo = new SelectList(db.tb_major, "MajorNo", "MajorName", tb_student.MajorNo);
            return View(tb_student);
        }

        // GET: manageStudent/MangeStudent/Delete/5
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

        // POST: manageStudent/MangeStudent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_student tb_student = db.tb_student.Find(id);
            db.tb_student.Remove(tb_student);
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
