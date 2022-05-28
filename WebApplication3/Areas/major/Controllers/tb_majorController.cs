using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Areas.major.Controllers
{
    public class tb_majorController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: major/tb_major
        public ActionResult Index()
        {
            var tb_major = db.tb_major.Include(t => t.tb_dept);
            if(Request.IsAjaxRequest())
            {
                return PartialView(tb_major.ToList());
            }
            return View(tb_major.ToList());
        }

        // GET: major/tb_major/Details/5
        public ActionResult Details(string id)
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

        // GET: major/tb_major/Create
        public ActionResult Create()
        {
            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName");
            return View();
        }

        // POST: major/tb_major/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MajorNo,DeptNo,MajorName,MajorAssistant,MajorTel,MjorDetail")] tb_major tb_major)
        {
            if (ModelState.IsValid)
            {
                db.tb_major.Add(tb_major);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName", tb_major.DeptNo);
            return View(tb_major);
        }

        // GET: major/tb_major/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName", tb_major.DeptNo);
            return View(tb_major);
        }

        // POST: major/tb_major/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MajorNo,DeptNo,MajorName,MajorAssistant,MajorTel,MjorDetail")] tb_major tb_major)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_major).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptNo = new SelectList(db.tb_dept, "DeptNo", "DeptName", tb_major.DeptNo);
            return View(tb_major);
        }

        // GET: major/tb_major/Delete/5
        public ActionResult Delete(string id)
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

        // POST: major/tb_major/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_major tb_major = db.tb_major.Find(id);
            db.tb_major.Remove(tb_major);
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
