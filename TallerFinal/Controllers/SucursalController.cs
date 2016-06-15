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
    public class SucursalController : Controller
    {
        private TallerFinalContext db = new TallerFinalContext();

        // GET: /Sucursal/
        public async Task<ActionResult> Index()
        {
            var sucursals = db.Sucursals.Include(s => s.Empresa);
            return View(await sucursals.ToListAsync());
        }

        // GET: /Sucursal/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = await db.Sucursals.FindAsync(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // GET: /Sucursal/Create
        public ActionResult Create(string empresaid)
        {
            Empresa persona = db.Empresas.Single<Empresa>(m => m.EmpresaId == empresaid);
            return View(new Sucursal { Empresa = persona });
        }

        // POST: /Sucursal/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SucursalId,Direccion,Telefono,EmpresaId")] Sucursal sucursal, string empresaid)
        {
            if (ModelState.IsValid)
            {
                Empresa persona = db.Empresas.Single<Empresa>(m => m.EmpresaId == empresaid);
                sucursal.Empresa = persona;
                db.Sucursals.Add(sucursal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Empresa");
            }
            return View(sucursal);
        }

        // GET: /Sucursal/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = await db.Sucursals.FindAsync(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "Nombre", sucursal.EmpresaId);
            return View(sucursal);
        }

        // POST: /Sucursal/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="SucursalId,Direccion,Telefono,EmpresaId")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sucursal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Empresa");
            }
            ViewBag.EmpresaId = new SelectList(db.Empresas, "EmpresaId", "Nombre", sucursal.EmpresaId);
            return View(sucursal);
        }

        // GET: /Sucursal/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sucursal sucursal = await db.Sucursals.FindAsync(id);
            if (sucursal == null)
            {
                return HttpNotFound();
            }
            return View(sucursal);
        }

        // POST: /Sucursal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sucursal sucursal = await db.Sucursals.FindAsync(id);
            db.Sucursals.Remove(sucursal);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Empresa");
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
