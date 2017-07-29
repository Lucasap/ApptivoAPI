using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API.Models;

namespace API.Controllers
{
    public class ApptivoController : Controller
    {
        // GET: Apptivo
        public ActionResult Registro()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Registro(Usuario Nusuario)
        {
            Nusuario.AgregarUsuario(Nusuario.Email);
            ViewBag.Nombre = Nusuario.Nombre;
            ViewBag.Email = Nusuario.Email;
            //return RedirectToAction("RegistroCorrecto",Nusuario);
            return View("RegistroCorrecto", Nusuario);
        }
        public ActionResult RegistroCorrecto()
        {
            return View();
        }
    }
}