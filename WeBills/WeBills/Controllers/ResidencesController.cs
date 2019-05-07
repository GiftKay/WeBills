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
//Web Based Solution for online bill payments.

namespace WeBills.Controllers
{
    public class ResidencesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Residences
        public ActionResult Index()
        {
            var cuser = db.Users.ToList().Find(x => x.Email == User.Identity.Name);

            var res = db.residents.ToList().FindAll(x => x.Id == cuser.Id);
            return View(res);
        }

        // GET: Residences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = db.residents.Find(id);
            if (residence == null)
            {
                return HttpNotFound();
            }
            return View(residence);
        }

        // GET: Residences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Residences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "homeid,citytown,surbub,Street,code,img")] Residence residence)
        {
            var cuser = db.Users.ToList().Find(x => x.Email == User.Identity.Name);
            residence.Id = cuser.Id;
            if (ModelState.IsValid)
            {

                db.residents.Add(residence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(residence);
        }

        // GET: Residences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = db.residents.Find(id);
            if (residence == null)
            {
                return HttpNotFound();
            }
            return View(residence);
        }

        // POST: Residences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "homeid,citytown,surbub,Street,code")] Residence residence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(residence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(residence);
        }

        // GET: Residences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Residence residence = db.residents.Find(id);
            if (residence == null)
            {
                return HttpNotFound();
            }
            return View(residence);
        }

        // POST: Residences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Residence residence = db.residents.Find(id);
            db.residents.Remove(residence);
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
        public ActionResult Managetasks(int id)
        {
            Residence r = db.residents.ToList().Find(x => x.homeid == id);
            ViewBag.house = r.citytown + " " + r.surbub + " " + r.Street + " " + r.code;
            Session["Hid"] = id;
            return View();
        }

        public ActionResult paybills()
        {
            LogicClass lg = new LogicClass();
            double el = lg.getRandom(0, 15000);
            ViewBag.outt = el;
            return View();
        }

        public ActionResult Stats()
        {
            //Total Number of houses and Total number of clients
            int tothouses = db.residents.ToList().Count(); ViewBag.totHs = tothouses;
            int totclients = db.clients.ToList().Count(); ViewBag.totCs = totclients;

            // Total Sum of Water Cost and Total Sum of Electricity Cost
            double watersum = 0, electrisum = 0;
            foreach (var item in db.bill.ToList())
            {
                watersum = Convert.ToDouble(item.waterCost++);
                electrisum = Convert.ToDouble(item.electricCost++);
            }
            ViewBag.wsum = watersum;
            ViewBag.esum = electrisum;

            //Total number of Bills Water and Electricity
            double totbills = watersum + electrisum; ViewBag.bsum = totbills;


            // Clients with more than 1 house
            int idcount = 0;
            List<Client> clients2res = new List<Client>();
            foreach (var item in db.clients.ToList())
            {
                foreach (var res in db.residents.ToList())
                {
                    if (item.userID.Equals(res.Id))
                    {
                        idcount++;
                    }
                    if (idcount > 1)
                    {
                        Client c = db.clients.ToList().Find(x => x.userID == res.Id);
                        clients2res.Add(c);
                    }
                }
                idcount = 0;
            }
            ViewBag.C2 = clients2res.ToList().Count();

            return View();
        }
        [HttpPost]
        public ActionResult Stats(FormCollection fc)
        {

            //Total Number of houses and Total number of clients
            int tothouses = db.residents.ToList().Count(); ViewBag.totHs = tothouses;
            int totclients = db.clients.ToList().Count(); ViewBag.totCs = totclients;

            // Total Sum of Water Cost and Total Sum of Electricity Cost
            double watersum = 0, electrisum = 0;
            foreach (var item in db.bill.ToList())
            {
                watersum = Convert.ToDouble(item.waterCost++);
                electrisum = Convert.ToDouble(item.electricCost++);
            }
            ViewBag.wsum = watersum;
            ViewBag.esum = electrisum;

            //Total number of Bills Water and Electricity
            double totbills = watersum + electrisum; ViewBag.bsum = totbills;


            // Clients with more than 1 house
            int idcount = 0;
            List<Client> clients2res = new List<Client>();
            foreach (var item in db.clients.ToList())
            {
                foreach (var res in db.residents.ToList())
                {
                    if (item.userID.Equals(res.Id))
                    {
                        idcount++;
                    }
                    if (idcount > 1)
                    {
                        Client c = db.clients.ToList().Find(x => x.userID == res.Id);
                        clients2res.Add(c);
                    }
                }
                idcount = 0;
            }
            ViewBag.C2 = clients2res.ToList().Count();

            var frt = fc["chktoths"].Contains("true");
            if (frt.Equals(true))
            {
                return RedirectToAction("Create");
            }

            return View();
        }
    }
}