using personal.Models;
using personal.Repositório;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal.Controllers
{
    public class FiltroController : Controller
    {
        // GET: Filtro
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Precos(String name, int servico)
        {
            List<Colaborador> colab = Filtro.precoListaPersonal(servico, name);
            Session["servico"] = 1;
            ViewBag.ItemData = colab.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Formacao(int id, int servico)
        {
            List<Colaborador> colab = Filtro.formacaoListaPersonal(servico, id);
            Session["servico"] = 1;
            ViewBag.ItemData = colab.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
    }
}