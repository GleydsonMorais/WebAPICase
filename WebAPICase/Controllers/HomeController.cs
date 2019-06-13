using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPICase.Data;
using WebAPICase.Models;

namespace WebAPICase.Controllers
{
    public class HomeController : Controller
    {
        //Objeto API USUARIO
        UsuarioController apiUsuario = new UsuarioController();

        //DATACONTEXT
        DataContext dataContext = new DataContext();

        //Objeto USUARIO
        Usuario usuario = new Usuario();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string senha)
        {
            this.usuario = dataContext.Usuario.Where(u => u.usuario == usuario && u.senha == senha).FirstOrDefault();

            if (this.usuario != null)
            {
                Session["NomeUsuario"] = this.usuario.nome;

                return View("Admin");
            }
            else
            {
                TempData["error"] = "Usuario não encontrado!!";

                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return View("Index");
        }

        public ActionResult Admin()
        {
            if (Session["NomeUsuario"] != null)
            {
                return View();
            }
            else
            {
                TempData["warning"] = "Sessão não foi iniciada!!";

                return View("Index");
            }
        }

        public ActionResult Usuario()
        {
            if (Session["NomeUsuario"] != null)
            {
                var listUsusario = apiUsuario.GetAllUsuario();

                return View(listUsusario);
            }
            else
            {
                TempData["warning"] = "Sessão não foi iniciada!!";

                return View("Index");
            }
        }
    }

    //TempData["warning"] = "Mensagem de warning!!";
    //TempData["success"] = "Mensagem de sucesso!!";
    //TempData["info"] = "Mensagem de informação!!";
    //TempData["error"] = "Mensagem de erro!!";
}
