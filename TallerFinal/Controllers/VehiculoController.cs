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
    public class VehiculoController : Controller
    {
        private TallerFinalContext db = new TallerFinalContext();

        // GET: /Vehiculo/
        public async Task<ActionResult> Index()
        {
            var vehiculoes = db.Vehiculoes;
            return View(await vehiculoes.ToListAsync());
        }

        // GET: /Vehiculo/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = await db.Vehiculoes.FindAsync(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // GET: /Vehiculo/Create
        public ActionResult Create(string personaid)
        {
            Persona persona = db.Personas.Single<Persona>(m => m.PersonaId == personaid);
            return View(new Vehiculo { Persona = persona });
        }

        // POST: /Vehiculo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="PersonaId,Nro_Placa,Marca,Modelo,Dia_Restriccion")] Vehiculo vehiculo,string personaid)
        {
            if (ModelState.IsValid)
            {
                Persona persona = db.Personas.Single<Persona>(m => m.PersonaId == personaid);
                vehiculo.Persona = persona;
                db.Vehiculoes.Add(vehiculo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Conductor");
            }

            return View(vehiculo);
        }

        // GET: /Vehiculo/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = await db.Vehiculoes.FindAsync(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: /Vehiculo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="PersonaId,Nro_Placa,Marca,Modelo,Dia_Restriccion")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vehiculo);
        }

        // GET: /Vehiculo/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculo vehiculo = await db.Vehiculoes.FindAsync(id);
            if (vehiculo == null)
            {
                return HttpNotFound();
            }
            return View(vehiculo);
        }

        // POST: /Vehiculo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Vehiculo vehiculo = await db.Vehiculoes.FindAsync(id);
            db.Vehiculoes.Remove(vehiculo);
            await db.SaveChangesAsync();
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
