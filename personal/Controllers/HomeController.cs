using personal.Models;
using personal.Repositório;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Mod()
        {
            return View("~/Views/Home/Servicos.cshtml");
        }
        public ActionResult Agendamentos()
        {
            List<Agendamentos> ag = listagem.listaAgendamentos(int.Parse(Session["ID"].ToString()));
            return View(ag);
        }
        public ActionResult AgendamentosConcluidos()
        {
            List<Agendamentos> ag = listagem.listaAgendamentosConcluidos(int.Parse(Session["ID"].ToString()));
            for (int i = 0; i < ag.Count; i++)
            {
                ag[i].DATAFORMATADA = ag[i].DATA.ToString();
            }
            ViewBag.ItemData = ag.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Avaliar()
        {
            List<Agendamentos> ag = listagem.listaAgendamentosAvaliar(int.Parse(Session["ID"].ToString()));
            ViewBag.ItemData = ag.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public void SalvarAvaliacao(int idEsp, int idPersonal, String stars, String dtHora)
        {
            listagem.SalvaAvaliacao(idEsp, idPersonal, stars, int.Parse(Session["ID"].ToString()), dtHora, @Session["ID"].ToString(), Session["TIPO"].ToString());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult NovoUsuario()
        {
            return View();
        }
        public ActionResult NovoColaborador()
        {
            List<Uf> list = listagem.listaUF();
            return View(list); ;
        }
        public ActionResult Modalidades()
        {
            List<HorariosColaborador> horarios = ModalidadesAulas.ListaHorariosColaborador(Session["ID"].ToString(), @Session["TIPO"].ToString());
            ViewBag.ItemData = horarios.ToList();
            return View();
        }
        public JsonResult Horarios(int id)
        {
            List<HorariosColaborador> horarios = ModalidadesAulas.ListaHorarioPorModalidade(id, @Session["ID"].ToString());
            ViewBag.ItemData = horarios.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ExcluirHorarios(String ids)
        {
            ModalidadesAulas.excluirHorario(ids, Session["ID"].ToString(), Session["TIPO"].ToString());
            return View();
        }
        public ActionResult ExcluirModalidade(int esportes)
        {
            ModalidadesAulas.excluirModalidade(esportes, Session["ID"].ToString(), Session["TIPO"].ToString());
            List<HorariosColaborador> horarios = ModalidadesAulas.ListaHorariosColaborador(Session["ID"].ToString(), @Session["TIPO"].ToString());
            ViewBag.ItemData = horarios.ToList();
            return View("~/Views/Home/Modalidades.cshtml");
        }
        public ActionResult MeuPerfil()
        {
            List<Colaborador> perfil = listagem.listaPerfil(@Session["TIPO"].ToString(), Session["ID"].ToString());
            ViewBag.ItemData = perfil.ToList();
            return View();
        }
        public ActionResult LogOut()
        {
            Session["USUARIO"] = "";
            Session["ID"] = "";
            Session["TIPO"] = "";
            return View("~/Views/Home/Index.cshtml");

        }
    }
}