using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TallerFinal.Models;
using TallerFinal.DAL;

namespace TallerFinal.Controllers
{
    public class UbicacionController : Controller
    {
        private TallerFinalContext db = new TallerFinalContext();

        // GET: /Ubicacion/
        public ActionResult Index()
        {
            var ubicacions = db.Ubicacions.Include(u => u.Persona);
            return View(ubicacions.ToList());
        }

        // GET: /Ubicacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicacion ubicacion = db.Ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        // GET: /Ubicacion/Create
        public ActionResult Create()
        {
            ViewBag.PersonaId = new SelectList(db.Personas, "PersonaId", "Procedencia");
            return View();
        }

        // POST: /Ubicacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UbicacionId,Latitud,Longitud,Fecha,PersonaId")] Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.Ubicacions.Add(ubicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonaId = new SelectList(db.Personas, "PersonaId", "Procedencia", ubicacion.PersonaId);
            return View(ubicacion);
        }

        // GET: /Ubicacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicacion ubicacion = db.Ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaId = new SelectList(db.Personas, "PersonaId", "Procedencia", ubicacion.PersonaId);
            return View(ubicacion);
        }

        // POST: /Ubicacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UbicacionId,Latitud,Longitud,Fecha,PersonaId")] Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonaId = new SelectList(db.Personas, "PersonaId", "Procedencia", ubicacion.PersonaId);
            return View(ubicacion);
        }

        // GET: /Ubicacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicacion ubicacion = db.Ubicacions.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        // POST: /Ubicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ubicacion ubicacion = db.Ubicacions.Find(id);
            db.Ubicacions.Remove(ubicacion);
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
