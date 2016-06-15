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
using System.Collections;

namespace TallerFinal.Controllers
{
    public class ConductorController : Controller
    {
        private TallerFinalContext db = new TallerFinalContext();

        // GET: /Conductor/
        public async Task<ActionResult> Index()
        {
            var personas = db.Personas.Where(p=> p.Tipo=="Conductor").ToListAsync();
            return View(await personas);
        }

        // GET: /Conductor/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = await db.Personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: /Conductor/Create
        public ActionResult Create()
        {
            var op = Session["empresa"].ToString();
            ViewBag.SucursalId = new SelectList(db.Sucursals.Where(m => m.EmpresaId==op).AsEnumerable(), "SucursalId", "Direccion");
            return View();
        }

        // POST: /Conductor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="PersonaId,CI,Procedencia,Nombre,Paterno,Materno,Direccion,Telefono,Celular,Tipo,SucursalId")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.PersonaId = "cond-" + persona.CI.ToString();
                persona.Tipo = "Conductor";
                db.Personas.Add(persona);
                await db.SaveChangesAsync();
                Usuario usu = new Usuario();
                usu.PersonaId = persona.PersonaId;
                usu.Username = persona.PersonaId;
                usu.Password = persona.PersonaId;
                db.Usuarios.Add(usu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Direccion");
            return View(persona);
        }

        // GET: /Conductor/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = await db.Personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }

            ViewBag.SucursalId = new SelectList(db.Sucursals.Where<Sucursal>(m => m.Empresa.EmpresaId == Session["empresa"].ToString()), "SucursalId", "Direccion");
            return View(persona);
        }

        // POST: /Conductor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="PersonaId,CI,Procedencia,Nombre,Paterno,Materno,Direccion,Telefono,Celular,Tipo,SucursalId")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var op = Session["propietario"].ToString();
            ViewBag.SucursalId = new SelectList(db.Sucursals.Where(m => m.Empresa.PersonaId == op).AsEnumerable(), "SucursalId", "Direccion");
            return View(persona);
        }

        // GET: /Conductor/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = await db.Personas.FindAsync(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: /Conductor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Persona persona = await db.Personas.FindAsync(id);
            db.Personas.Remove(persona);
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
