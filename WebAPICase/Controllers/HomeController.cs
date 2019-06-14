using System;
using System.Collections.Generic;
using System.IO;
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

        //Objeto API HISTORICO
        HistoricoController apiHistorico = new HistoricoController();

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

        public ActionResult Historico()
        {
            if (Session["NomeUsuario"] != null)
            {
                var listHistorico = apiHistorico.Get();

                return View(listHistorico);
            }
            else
            {
                TempData["warning"] = "Sessão não foi iniciada!!";

                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult ImportarHistorico(HttpPostedFileBase[] Arquivos)
        {
            if (Session["NomeUsuario"] != null)
            {
                foreach (var arquivo in Arquivos)
                {
                    List<Historico> listHistorico = new List<Historico>();

                    string conteudo = string.Empty;
                    string registro = string.Empty;

                    using (StreamReader reader = new StreamReader(arquivo.InputStream))
                    {
                        conteudo = reader.ReadLine();

                    }
                }

                return View("Historico");
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
