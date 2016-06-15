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
    public class EmpresaController : Controller
    {
        private TallerFinalContext db = new TallerFinalContext();

        // GET: /Empresa/
        public async Task<ActionResult> Index()
        {
            var empresas = db.Empresas.ToListAsync();
            return View(await empresas);
        }

        // GET: /Empresa/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: /Empresa/Create
        public ActionResult Create(string personaid)
        {
            Persona persona = db.Personas.Single<Persona>(m => m.PersonaId == personaid);
            return View(new Empresa { Persona = persona });
        }

        // POST: /Empresa/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmpresaId,Nombre,NIT,PersonaId")] Empresa empresa, string personaid)
        {
            if (ModelState.IsValid)
            {
                empresa.EmpresaId = "emp-" + empresa.NIT.ToString();
                Persona persona = db.Personas.Single<Persona>(m => m.PersonaId == personaid);
                empresa.Persona = persona;
                db.Empresas.Add(empresa);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Propietario");
            }

            return View(empresa);
        }

        // GET: /Empresa/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: /Empresa/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="EmpresaId,Nombre,NIT,PersonaId")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Propietario");
            }
            return View(empresa);
        }

        // GET: /Empresa/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: /Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Empresa empresa = await db.Empresas.FindAsync(id);
            db.Empresas.Remove(empresa);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Propietario");
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
