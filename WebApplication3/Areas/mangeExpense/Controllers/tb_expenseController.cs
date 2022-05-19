using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Areas.mangeExpense.Controllers
{
    public class tb_expenseController : Controller
    {
        private JiaowuContext db = new JiaowuContext();

        // GET: mangeExpense/tb_expense
        public ActionResult Index()
        {
            if(Request.IsAjaxRequest())
            {
                return PartialView(db.tb_expense.ToList());
            }
            return View(db.tb_expense.ToList());
        }
        // GET: mangeExpense/tb_expense/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_expense tb_expense = db.tb_expense.Find(id);
            if (tb_expense == null)
            {
                return HttpNotFound();
            }
            return View(tb_expense);
        }

        // GET: mangeExpense/tb_expense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mangeExpense/tb_expense/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpenseNo,ExpenseName,ExpenseNum")] tb_expense tb_expense)
        {
            if (ModelState.IsValid)
            {
                db.tb_expense.Add(tb_expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_expense);
        }

        // GET: mangeExpense/tb_expense/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_expense tb_expense = db.tb_expense.Find(id);
            if (tb_expense == null)
            {
                return HttpNotFound();
            }
            return View(tb_expense);
        }

        // POST: mangeExpense/tb_expense/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性。有关
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseNo,ExpenseName,ExpenseNum")] tb_expense tb_expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_expense);
        }

        // GET: mangeExpense/tb_expense/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_expense tb_expense = db.tb_expense.Find(id);
            if (tb_expense == null)
            {
                return HttpNotFound();
            }
            return View(tb_expense);
        }

        // POST: mangeExpense/tb_expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tb_expense tb_expense = db.tb_expense.Find(id);
            db.tb_expense.Remove(tb_expense);
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
