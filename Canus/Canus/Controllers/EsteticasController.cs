using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Canus.Models;

namespace Canus.Controllers
{
    public class EsteticasController : Controller
    {
        private Canuscontext db = new Canuscontext();

        // GET: Esteticas
        public ActionResult Index()
        {
            //var esteticas = db.Esteticas.Include(e => e.Estilista);
            //return View(esteticas.ToList());
            return View(db.Esteticas.ToList());
        }

        // GET: Esteticas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estetica estetica = db.Esteticas.Find(id);
            if (estetica == null)
            {
                return HttpNotFound();
            }
            return View(estetica);
        }

        // GET: Esteticas/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Estilistas, "Id", "Nombre");
            return View();
        }

        // POST: Esteticas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] Estetica estetica)
        {
            if (ModelState.IsValid)
            {
                db.Esteticas.Add(estetica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Estilistas, "Id", "Nombre", estetica.Id);
            return View(estetica);
        }

        // GET: Esteticas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estetica estetica = db.Esteticas.Find(id);
            if (estetica == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Estilistas, "Id", "Nombre", estetica.Id);
            return View(estetica);
        }

        // POST: Esteticas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] Estetica estetica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estetica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Estilistas, "Id", "Nombre", estetica.Id);
            return View(estetica);
        }

        // GET: Esteticas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estetica estetica = db.Esteticas.Find(id);
            if (estetica == null)
            {
                return HttpNotFound();
            }
            return View(estetica);
        }

        // POST: Esteticas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estetica estetica = db.Esteticas.Find(id);
            db.Esteticas.Remove(estetica);
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
