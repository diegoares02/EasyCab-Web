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
using System.IO;

namespace TallerFinal.Controllers
{
    public class ServicioController : Controller
    {
        private TallerFinalContext db = new TallerFinalContext();

        // GET: /Servicio/
        public async Task<ActionResult> Index()
        {
            int sum = 0;
            var servicios = db.Servicios.Include(s => s.Cliente).Include(s => s.Persona).Include(s => s.Vehiculo);            
            return View(await servicios.ToListAsync());
        }

        // GET: /Servicio/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = await db.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // GET: /Servicio/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nombre");
            ViewBag.Operador = new SelectList(db.Personas.Where(m => m.Tipo=="Operador").AsEnumerable(), "PersonaId", "PersonaId");
            ViewBag.Conductor = new SelectList(db.Vehiculoes, "PersonaId", "Nro_Placa");
            return View();
        }

        // POST: /Servicio/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="ServicioId,Tarifa,Fecha,DireccionOrigen,DireccionDestino,ClienteId,Operador,Conductor")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Servicios.Add(servicio);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nombre", servicio.ClienteId);
            ViewBag.Operador = new SelectList(db.Personas, "PersonaId", "Procedencia", servicio.Operador);
            ViewBag.Conductor = new SelectList(db.Vehiculoes, "PersonaId", "Nro_Placa", servicio.Conductor);
            return View(servicio);
        }

        // GET: /Servicio/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = await db.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nombre", servicio.ClienteId);
            ViewBag.Operador = new SelectList(db.Personas, "PersonaId", "Procedencia", servicio.Operador);
            ViewBag.Conductor = new SelectList(db.Vehiculoes, "PersonaId", "Nro_Placa", servicio.Conductor);
            return View(servicio);
        }

        // POST: /Servicio/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="ServicioId,Tarifa,Fecha,DireccionOrigen,DireccionDestino,ClienteId,Operador,Conductor")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicio).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "Nombre", servicio.ClienteId);
            ViewBag.Operador = new SelectList(db.Personas, "PersonaId", "Procedencia", servicio.Operador);
            ViewBag.Conductor = new SelectList(db.Vehiculoes, "PersonaId", "Nro_Placa", servicio.Conductor);
            return View(servicio);
        }

        // GET: /Servicio/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servicio servicio = await db.Servicios.FindAsync(id);
            if (servicio == null)
            {
                return HttpNotFound();
            }
            return View(servicio);
        }

        // POST: /Servicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Servicio servicio = await db.Servicios.FindAsync(id);
            db.Servicios.Remove(servicio);
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
