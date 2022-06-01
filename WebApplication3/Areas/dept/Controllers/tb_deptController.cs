using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Areas.dept.Controllers
{
    public class tb_deptController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: dept/tb_dept
        public ActionResult Index()
        {
            if(Request.IsAjaxRequest())
            {
                return PartialView(db.tb_dept.ToList());
            }
            return View(db.tb_dept.ToList());
        }

        // GET: dept/tb_dept/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_dept tb_dept = db.tb_dept.Find(id);
            if (tb_dept == null)
            {
                return HttpNotFound();
            }
            return View(tb_dept);
        }

        // GET: dept/tb_dept/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: dept/tb_dept/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeptNo,DeptName,DeptChairman,DeptTel,DeptDesc")] tb_dept tb_dept)
        {
            if (ModelState.IsValid)
            {
                db.tb_dept.Add(tb_dept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_dept);
        }

        // GET: dept/tb_dept/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_dept tb_dept = db.tb_dept.Find(id);
            if (tb_dept == null)
            {
                return HttpNotFound();
            }
            return View(tb_dept);
        }

        // POST: dept/tb_dept/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeptNo,DeptName,DeptChairman,DeptTel,DeptDesc")] tb_dept tb_dept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_dept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_dept);
        }

        // GET: dept/tb_dept/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_dept tb_dept = db.tb_dept.Find(id);
            if (tb_dept == null)
            {
                return HttpNotFound();
            }
            return View(tb_dept);
        }

        // POST: dept/tb_dept/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_dept tb_dept = db.tb_dept.Find(id);
            db.tb_dept.Remove(tb_dept);
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
