using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerFinal.DAL;
using TallerFinal.Models;

namespace TallerFinal.Controllers
{
    public class LoginController : Controller
    {
        TallerFinalContext db = new TallerFinalContext();
        //
        // GET: /Login/
        public ActionResult Login()
        { return View(); }
        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            if (db.Usuarios.Where(m => m.Username == user && m.Password == pass && m.Persona.Tipo == "Administrador").ToList().Count > 0)
            {
                Session["user"] = new Usuario { Username = user };
                Session["tipo"] = "Administrador";
                return RedirectToAction("Index", "Administrador");
            }
            if (db.Usuarios.Where(m => m.Username == user && m.Password == pass && m.Persona.Tipo == "Propietario").ToList().Count > 0)
            {
                Empresa empresa = db.Empresas.Single(m => m.Persona.Usuario.Username == user);
                Session["tipo"] = "Propietario";
                Session["user"] = new Usuario { Username = user };
                Session["empresa"] = empresa.EmpresaId;
                return RedirectToAction("Index", "Conductor");
            }
            if (db.Usuarios.Where(m => m.Username == user && m.Password == pass && m.Persona.Tipo == "Operador").ToList().Count > 0)
            {
                Session["tipo"] = "Operador";
                Session["user"] = new Usuario { Username = user };
                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                ViewBag.Message = "Usuario no valido";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
	}
}