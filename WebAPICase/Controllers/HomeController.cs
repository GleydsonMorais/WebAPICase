using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
                var listUsusario = apiUsuario.Get();

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

        //Função que importar aquivor BRUTO
        [HttpPost]
        public ActionResult ImportarHistorico(HttpPostedFileBase arquivo)
        {
            if (Session["NomeUsuario"] != null)
            {
                char separador = ';';
                Encoding encoding = Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage);
                StreamReader leitor = null;
                string[] dados = null;
                string linha = null;
                string[] campos = null;

                try
                {
                    leitor = new StreamReader(arquivo.InputStream, encoding);
                    linha = leitor.ReadLine();
                    campos = linha.Split(separador);

                    linha = leitor.ReadLine();

                    List<Historico> listHistorico = new List<Historico>();

                    do
                    {
                        Historico historico = new Historico();

                        dados = linha.Split(separador);
                        decimal valorCompra = 0;

                        if (dados.Length == 11)
                        {
                            for (int i = 0; i < dados.Length && i < campos.Length; i++)
                            {

                                switch (campos[i].ToUpper())
                                {
                                    case "REGIÃO - SIGLA":
                                        historico.regiao = dados[i].ToUpper().Trim();
                                        break;
                                    case "ESTADO - SIGLA":
                                        historico.estado = dados[i].ToUpper().Trim();
                                        break;
                                    case "MUNICÍPIO":
                                        historico.municipio = dados[i].ToUpper();
                                        break;
                                    case "REVENDA":
                                        historico.revenda = dados[i].ToUpper();
                                        break;
                                    case "INSTALAÇÃO - CÓDIGO":
                                        historico.instalacaoCodigo = dados[i];
                                        break;
                                    case "PRODUTO":
                                        historico.produto = dados[i].ToUpper();
                                        break;
                                    case "DATA DA COLETA":
                                        historico.dataColeta = DateTime.Parse(dados[i]);
                                        break;
                                    case "VALOR DE COMPRA":
                                        if (dados[i] != "")
                                        {
                                            historico.valorCompra = Convert.ToDecimal(dados[i]);
                                        }
                                        else
                                        {
                                            historico.valorCompra = valorCompra;
                                        }
                                        break;
                                    case "VALOR DE VENDA":
                                        historico.valorVenda = Convert.ToDecimal(dados[i]);
                                        break;
                                    case "UNIDADE DE MEDIDA":
                                        historico.undMedida = dados[i];
                                        break;
                                    case "BANDEIRA":
                                        historico.bandeira = dados[i].ToUpper();
                                        break;
                                }
                            }

                            //adiciona o hostorio a lista
                            listHistorico.Add(historico);
                        }

                        //adiciona o historico no banco pela API
                        //apiHistorico.Post(historico);

                        //le a proxima linha
                        linha = leitor.ReadLine();

                    } while (!string.IsNullOrEmpty(linha));

                    //Resultado - adiciona o historico no banco pela API
                    //TempData["success"] = "Historico importado com sucesso!!";
                    //return RedirectToAction("Historico");

                    //adiciona a lista de historico direto pelo entity framework
                    if (listHistorico.Count() > 0)
                    {
                        dataContext.Historico.AddRange(listHistorico);
                        dataContext.SaveChanges();

                        TempData["success"] = "Historico importado com sucesso!!";

                        return RedirectToAction("Historico");
                    }
                    else
                    {
                        TempData["warning"] = "O arquivo esta em braco!!";

                        return View("Admin");
                    }
                }
                catch (Exception e)
                {
                    TempData["warning"] = "Erro ao importar o arquivo de historico, tente novamente!!";

                    return View("Admin");
                }
            }
            else
            {
                TempData["warning"] = "Sessão não foi iniciada!!";

                return View("Index");
            }
        }

        //Função que importar arquivo modificado
        //[HttpPost]
        //public ActionResult ImportarHistorico(HttpPostedFileBase arquivo)
        //{
        //    if (Session["NomeUsuario"] != null)
        //    {
        //        char separador = ';';
        //        Encoding encoding = Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage);
        //        StreamReader leitor = null;
        //        string[] dados = null;
        //        string linha = null;
        //        string[] campos = null;

        //        try
        //        {
        //            leitor = new StreamReader(arquivo.InputStream, encoding);
        //            linha = leitor.ReadLine();
        //            campos = linha.Split(separador);

        //            linha = leitor.ReadLine();

        //            List<Historico> listHistorico = new List<Historico>();

        //            do
        //            {
        //                Historico historico = new Historico();

        //                dados = linha.Split(separador);
        //                decimal valorCompra = 0;

        //                for (int i = 0; i < dados.Length && i < campos.Length ; i++)
        //                {

        //                    switch (campos[i].ToUpper())
        //                    {
        //                        case "REGIÃO - SIGLA":
        //                            historico.regiao = dados[i].ToUpper();
        //                            break;
        //                        case "ESTADO - SIGLA":
        //                            historico.estado = dados[i].ToUpper();
        //                            break;
        //                        case "MUNICÍPIO":
        //                            historico.municipio = dados[i].ToUpper();
        //                            break;
        //                        case "REVENDA":
        //                            historico.revenda = dados[i].ToUpper();
        //                            break;
        //                        case "INSTALAÇÃO - CÓDIGO":
        //                            historico.instalacaoCodigo = dados[i];
        //                            break;
        //                        case "PRODUTO":
        //                            historico.produto = dados[i].ToUpper();
        //                            break;
        //                        case "DATA DA COLETA":
        //                            historico.dataColeta = DateTime.Parse(dados[i]);
        //                            break;
        //                        case "VALOR DE COMPRA":
        //                            if (dados[i] != "")
        //                            {
        //                                historico.valorCompra = Convert.ToDecimal(dados[i]);
        //                            }
        //                            else
        //                            {
        //                                historico.valorCompra = valorCompra;
        //                            }
        //                            break;
        //                        case "VALOR DE VENDA":
        //                            historico.valorVenda = Convert.ToDecimal(dados[i]);
        //                            break;
        //                        case "UNIDADE DE MEDIDA":
        //                            historico.undMedida = dados[i];
        //                            break;
        //                        case "BANDEIRA":
        //                            historico.bandeira = dados[i].ToUpper();
        //                            break;
        //                    }
        //                }

        //                //adiciona o hostorio a lista
        //                //listHistorico.Add(historico);

        //                //adiciona o historico no banco
        //                apiHistorico.Post(historico);

        //                //le a proxima linha
        //                linha = leitor.ReadLine();

        //            } while (!string.IsNullOrEmpty(linha));

        //            TempData["success"] = "Historico importado com sucesso!!";

        //            return RedirectToAction("Historico");

        //            //if (listHistorico.Count() > 0)
        //            //{
        //            //    dataContext.Historico.AddRange(listHistorico);
        //            //    dataContext.SaveChanges();

        //            //    TempData["success"] = "Historico importado com sucesso!!";

        //            //    return RedirectToAction("Historico");
        //            //}
        //            //else
        //            //{
        //            //    TempData["warning"] = "O arquivo esta em braco!!";

        //            //    return View("Admin");
        //            //}
        //        }
        //        catch (Exception e)
        //        {
        //            TempData["warning"] = "Erro ao importar o arquivo de historico, tente novamente!!";

        //            return View("Admin");
        //        }
        //    }
        //    else
        //    {
        //        TempData["warning"] = "Sessão não foi iniciada!!";

        //        return View("Index");
        //    }
        //}
    }

    //TempData["warning"] = "Mensagem de warning!!";
    //TempData["success"] = "Mensagem de sucesso!!";
    //TempData["info"] = "Mensagem de informação!!";
    //TempData["error"] = "Mensagem de erro!!";
}
