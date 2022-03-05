using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Canus.Models;
using Microsoft.AspNet.Identity;

namespace Canus.Controllers
{
    public class VentasController : Controller
    {
        private Canuscontext db = new Canuscontext();

        // GET: Ventas
        public ActionResult Index()
        {
            var ventas = db.Ventas.Include(v => v.Estetica).Include(v => v.Estilista).Include(v => v.Servicio);
            var esteticas = new SelectList(db.Esteticas, "Id", "Nombre");
            var estilistas = new SelectList(db.Estilistas, "Id", "Nombre");
            var servicios = new SelectList(db.Servicios, "Id", "Nombre");
            ViewBag.esteticaid = esteticas;
            ViewBag.estilistaid = estilistas;
            ViewBag.servicioid = servicios;
            Session["Sales"] = ventas.ToList();
            return View(ventas.ToList());
        }

        //Post: Ventas
        [HttpPost]
        public ActionResult Index(FormCollection esteticaid, FormCollection estilistaid, FormCollection servicioid, string datesearch)
        {
            var ventas = db.Ventas.Include(v => v.Estetica).Include(v => v.Estilista).Include(v => v.Servicio);
            int tiendas = 0;
            int empleados = 0;
            int productos = 0;
            var esteticas = new SelectList(db.Esteticas, "Id", "Nombre", tiendas);
            var estilistas = new SelectList(db.Estilistas, "Id", "Nombre", empleados);
            var servicios = new SelectList(db.Servicios, "Id", "Nombre", productos);
            ViewBag.esteticaid = esteticas;
            ViewBag.estilistaid = estilistas;
            ViewBag.servicioid = servicios;
            string esteticaselected = esteticaid["esteticaid"].ToString();
            if (esteticaselected != "")
            {                
                int selectid = 0;
                int.TryParse(esteticaselected, out selectid);
                ventas = ventas.Where(v => v.EsteticaId == selectid);
            }
            string estilistaselected = estilistaid["estilistaid"].ToString();
            if (estilistaselected != "")
            {
                int selectid = 0;
                int.TryParse(estilistaselected, out selectid);
                ventas = ventas.Where(f => f.EstilistaId == selectid);
            }
            string servicioselected = servicioid["servicioid"].ToString();
            if (servicioselected != "")
            {
                int selectid = 0;
                int.TryParse(servicioselected, out selectid);
                ventas = ventas.Where(f => f.ServicioId == selectid);
            }
            if (!String.IsNullOrEmpty(datesearch))
            {
                DateTime dateforsearch = Convert.ToDateTime(datesearch);
                ventas = ventas.Where(f => f.Fecha.Equals(dateforsearch));
            }
            if (ventas.Count() != 0)
            {
                decimal Importe = ventas.Sum(s => s.Precio);
                ViewBag.Total = Importe.ToString();
            }
            else
            {
                ViewBag.Total = "0.00";
            }
            
            Session["Sales"] = ventas.ToList();
            return View(ventas.ToList());
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: Ventas/Create
        public ActionResult Create()
        {
            ViewBag.EsteticaId = new SelectList(db.Esteticas, "Id", "Nombre");
            ViewBag.EstilistaId = new SelectList(db.Estilistas, "Id", "Nombre");
            ViewBag.ServicioId = new SelectList(db.Servicios, "Id", "Nombre");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,EsteticaId,EstilistaId,ServicioId,Precio")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                var RegistradoActual = User.Identity.GetUserName();
                var UsuarioActual = (from u in db.AspNetUsers.Where(u => u.UserName == RegistradoActual) select u).First();
                var EstilistaActual = (from e in db.Estilistas.Where(e => e.AspNetUserId == UsuarioActual.Id) select e).First();
                var EsteticaActual = (from e in db.Esteticas.Where(e => e.Id == EstilistaActual.EsteticaId) select e).First();
                Servicio ServicioSeleccionado = new Servicio();
                db.Ventas.Add(venta);
                ServicioSeleccionado.Id = venta.ServicioId;
                //venta.Precio = venta.Servicio.Precio;
                var ServicioActual = (from s in db.Servicios.Where(s => s.Id.Equals(ServicioSeleccionado.Id)) select s).First();
                venta.Precio = ServicioActual.Precio;
                venta.EstilistaId = EstilistaActual.Id;
                venta.EsteticaId = EsteticaActual.Id;
                venta.Fecha = DateTime.Now;  
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EsteticaId = new SelectList(db.Esteticas, "Id", "Nombre", venta.EsteticaId);
            ViewBag.EstilistaId = new SelectList(db.Estilistas, "Id", "Nombre", venta.EstilistaId);
            ViewBag.ServicioId = new SelectList(db.Servicios, "Id", "Nombre", venta.ServicioId);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.EsteticaId = new SelectList(db.Esteticas, "Id", "Nombre", venta.EsteticaId);
            ViewBag.EstilistaId = new SelectList(db.Estilistas, "Id", "Nombre", venta.EstilistaId);
            ViewBag.ServicioId = new SelectList(db.Servicios, "Id", "Nombre", venta.ServicioId);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,EsteticaId,EstilistaId,ServicioId,Precio")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EsteticaId = new SelectList(db.Esteticas, "Id", "Nombre", venta.EsteticaId);
            ViewBag.EstilistaId = new SelectList(db.Estilistas, "Id", "Nombre", venta.EstilistaId);
            ViewBag.ServicioId = new SelectList(db.Servicios, "Id", "Nombre", venta.ServicioId);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venta venta = db.Ventas.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venta venta = db.Ventas.Find(id);
            db.Ventas.Remove(venta);
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
