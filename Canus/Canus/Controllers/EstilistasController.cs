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
    public class EstilistasController : Controller
    {
        private Canuscontext db = new Canuscontext();

        // GET: Estilistas
        public ActionResult Index()
        {
            var estilistas = db.Estilistas.Include(e => e.Estetica);
            return View(estilistas.ToList());
        }

        // GET: Estilistas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estilista estilista = db.Estilistas.Find(id);
            if (estilista == null)
            {
                return HttpNotFound();
            }
            return View(estilista);
        }

        // GET: Estilistas/Create
        public ActionResult Create()
        {
            ViewBag.EsteticaId = new SelectList(db.Esteticas, "Id", "Nombre");
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName");
            return View();
        }

        // POST: Estilistas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,EsteticaId")] Estilista estilista)
        {
            if (ModelState.IsValid)
            {
                db.Estilistas.Add(estilista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EsteticaId = new SelectList(db.Esteticas, "Id", "Nombre", estilista.EsteticaId);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", estilista.AspNetUserId);
            return View(estilista);
        }

        // GET: Estilistas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estilista estilista = db.Estilistas.Find(id);
            if (estilista == null)
            {
                return HttpNotFound();
            }
            ViewBag.EsteticaId = new SelectList(db.Esteticas, "Id", "Nombre", estilista.EsteticaId);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", estilista.AspNetUserId);
            return View(estilista);
        }

        // POST: Estilistas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,EsteticaId")] Estilista estilista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estilista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EsteticaId = new SelectList(db.Esteticas, "Id", "Nombre", estilista.EsteticaId);
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "UserName", estilista.AspNetUserId);
            return View(estilista);
        }

        // GET: Estilistas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estilista estilista = db.Estilistas.Find(id);
            if (estilista == null)
            {
                return HttpNotFound();
            }
            return View(estilista);
        }

        // POST: Estilistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estilista estilista = db.Estilistas.Find(id);
            db.Estilistas.Remove(estilista);
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
