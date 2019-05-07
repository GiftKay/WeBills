using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeBills.Models;
using WeBills.Logic;

namespace WeBills.Controllers
{
    public class billsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: bills
        public ActionResult Index()
        {
            int iid = Convert.ToInt16(Session["Hid"]);
            List<bills> bil = db.bill.ToList().FindAll(x => x.homeid == iid);
            LogicClass lg = new LogicClass();

            if (bil.Count().Equals(0))
            {
                bills b = new bills();
                b.homeid = iid;
                b.waterCost = Math.Round(lg.getRandom(500, 1000), 2, MidpointRounding.AwayFromZero);
                b.electricCost = Math.Round(lg.getRandom(500, 15000), 2, MidpointRounding.AwayFromZero);

                db.bill.Add(b);
                db.SaveChanges();

                List<bills> bi = db.bill.ToList().FindAll(x => x.homeid == iid);
                return View(bi);
            }

            foreach (var item in bil)
            {
                if (item.waterCost.Equals(0) && item.electricCost.Equals(0))
                {
                    item.electricCost = Math.Round(lg.getRandom(500, 1000), 2, MidpointRounding.AwayFromZero);
                    item.waterCost = Math.Round(lg.getRandom(500, 1500), 2, MidpointRounding.AwayFromZero);
                    db.SaveChanges();
                }
            }


            return View(bil);
        }

        // GET: bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bills bills = db.bill.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            return View(bills);
        }

        // GET: bills/Create
        public ActionResult Create()
        {
            ViewBag.homeid = new SelectList(db.residents, "homeid", "citytown");
            return View();
        }

        // POST: bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "billid,homeid,waterCost,electricCost")] bills bills)
        {
            if (ModelState.IsValid)
            {
                db.bill.Add(bills);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.homeid = new SelectList(db.residents, "homeid", "citytown", bills.homeid);
            return View(bills);
        }

        // GET: bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bills bills = db.bill.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            ViewBag.homeid = new SelectList(db.residents, "homeid", "citytown", bills.homeid);
            return View(bills);
        }

        // POST: bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "billid,homeid,waterCost,electricCost")] bills bills)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bills).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.homeid = new SelectList(db.residents, "homeid", "citytown", bills.homeid);
            return View(bills);
        }

        // GET: bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bills bills = db.bill.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            return View(bills);
        }

        // POST: bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bills bills = db.bill.Find(id);
            db.bill.Remove(bills);
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
