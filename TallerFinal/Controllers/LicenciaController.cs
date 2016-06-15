using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TallerFinal.Models;
using TallerFinal.DAL;

namespace TallerFinal.Controllers
{
    public class LicenciaController : Controller
    {
        private TallerFinalContext db = new TallerFinalContext();

        // GET: /Licencia/
        public async Task<ActionResult> Index()
        {
            var licencia_conducir = db.Licencia_Conducir.ToListAsync();
            return View(await licencia_conducir);
        }

        // GET: /Licencia/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencia_Conducir licencia_conducir = await db.Licencia_Conducir.FindAsync(id);
            if (licencia_conducir == null)
            {
                return HttpNotFound();
            }
            return View(licencia_conducir);
        }

        // GET: /Licencia/Create
        public ActionResult Create(string personaid)
        {
            Persona persona = db.Personas.Single<Persona>(m => m.PersonaId == personaid);
            return View(new Licencia_Conducir { Persona = persona });
        }

        // POST: /Licencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="PersonaId,Categoria,Fecha_Expiracion")] Licencia_Conducir licencia_conducir,string personaid)
        {
            if (ModelState.IsValid)
            {
                Persona persona = db.Personas.Single<Persona>(m => m.PersonaId == personaid);
                licencia_conducir.Persona = persona;
                db.Licencia_Conducir.Add(licencia_conducir);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Conductor");
            }

            return View(licencia_conducir);
        }

        // GET: /Licencia/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencia_Conducir licencia_conducir = await db.Licencia_Conducir.FindAsync(id);
            if (licencia_conducir == null)
            {
                return HttpNotFound();
            }
            return View(licencia_conducir);
        }

        // POST: /Licencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="PersonaId,Categoria,Fecha_Expiracion")] Licencia_Conducir licencia_conducir)
        {
            if (ModelState.IsValid)
            {
                db.Entry(licencia_conducir).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Conductor");
            }
            return View(licencia_conducir);
        }

        // GET: /Licencia/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencia_Conducir licencia_conducir = await db.Licencia_Conducir.FindAsync(id);
            if (licencia_conducir == null)
            {
                return HttpNotFound();
            }
            return View(licencia_conducir);
        }

        // POST: /Licencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Licencia_Conducir licencia_conducir = await db.Licencia_Conducir.FindAsync(id);
            db.Licencia_Conducir.Remove(licencia_conducir);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Conductor");
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
