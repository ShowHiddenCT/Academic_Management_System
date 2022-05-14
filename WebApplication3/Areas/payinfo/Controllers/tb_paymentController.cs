using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Areas.payinfo.Controllers
{
    public class tb_paymentController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: payinfo/tb_payment
        public ActionResult Index()
        {
            var tb_payment = db.tb_payment.Include(t => t.tb_student);
            if(Request.IsAjaxRequest())
            {
                return PartialView(tb_payment.ToList());
            }
            return View(tb_payment.ToList());
        }

        // GET: payinfo/tb_payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_payment tb_payment = db.tb_payment.Find(id);
            if (tb_payment == null)
            {
                return HttpNotFound();
            }
            return View(tb_payment);
        }

        // GET: payinfo/tb_payment/Create
        public ActionResult Create()
        {
            ViewBag.StudentNo = new SelectList(db.tb_student, "StudentNo", "StudentName");
            return View();
        }

        // POST: payinfo/tb_payment/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentNo")] tb_payment tb_payment)
        {
            if (ModelState.IsValid)
            {
                db.tb_payment.Add(tb_payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentNo = new SelectList(db.tb_student, "StudentNo", "StudentName", tb_payment.StudentNo);
            return View(tb_payment);
        }

        // GET: payinfo/tb_payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_payment tb_payment = db.tb_payment.Find(id);
            if (tb_payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentNo = new SelectList(db.tb_student, "StudentNo", "StudentName", tb_payment.StudentNo);
            return View(tb_payment);
        }

        // POST: payinfo/tb_payment/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentNo")] tb_payment tb_payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentNo = new SelectList(db.tb_student, "StudentNo", "StudentName", tb_payment.StudentNo);
            return View(tb_payment);
        }

        // GET: payinfo/tb_payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_payment tb_payment = db.tb_payment.Find(id);
            if (tb_payment == null)
            {
                return HttpNotFound();
            }
            return View(tb_payment);
        }

        // POST: payinfo/tb_payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_payment tb_payment = db.tb_payment.Find(id);
            db.tb_payment.Remove(tb_payment);
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
