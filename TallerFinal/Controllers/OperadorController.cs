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
    public class OperadorController : Controller
    {
        private TallerFinalContext db = new TallerFinalContext();

        // GET: /Operador/
        public async Task<ActionResult> Index()
        {
            var personas = db.Personas.Where(m => m.Tipo == "Operador");
            return View(await personas.ToListAsync());
        }

        // GET: /Operador/Details/5
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

        // GET: /Operador/Create
        public ActionResult Create()
        {
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Direccion");
            return View();
        }

        // POST: /Operador/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="PersonaId,CI,Procedencia,Nombre,Paterno,Materno,Direccion,Telefono,Celular,Tipo,SucursalId")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                persona.PersonaId = "op-" + persona.CI.ToString();
                persona.Tipo = "Operador";
                db.Personas.Add(persona);
                await db.SaveChangesAsync();
                Usuario usu = new Usuario();
                usu.PersonaId = persona.PersonaId;
                usu.Username = "op-" + persona.CI.ToString();
                usu.Password = "op-" + persona.CI.ToString();
                db.Usuarios.Add(usu);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Direccion", persona.SucursalId);
            return View(persona);
        }

        // GET: /Operador/Edit/5
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
            ViewBag.PersonaId = new SelectList(db.Licencia_Conducir, "PersonaId", "Categoria", persona.PersonaId);
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Direccion", persona.SucursalId);
            ViewBag.PersonaId = new SelectList(db.Usuarios, "PersonaId", "Username", persona.PersonaId);
            ViewBag.PersonaId = new SelectList(db.Vehiculoes, "PersonaId", "Nro_Placa", persona.PersonaId);
            return View(persona);
        }

        // POST: /Operador/Edit/5
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
            ViewBag.PersonaId = new SelectList(db.Licencia_Conducir, "PersonaId", "Categoria", persona.PersonaId);
            ViewBag.SucursalId = new SelectList(db.Sucursals, "SucursalId", "Direccion", persona.SucursalId);
            ViewBag.PersonaId = new SelectList(db.Usuarios, "PersonaId", "Username", persona.PersonaId);
            ViewBag.PersonaId = new SelectList(db.Vehiculoes, "PersonaId", "Nro_Placa", persona.PersonaId);
            return View(persona);
        }

        // GET: /Operador/Delete/5
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

        // POST: /Operador/Delete/5
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
