using personal.Models;
using personal.Repositório;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personal.Controllers
{
    public class ServicosController : Controller
    {
        //1 - MUSCULACAO
        //2 - NATAÇÃO
        //3 - CICLISMO
        //4 - CORRIDA
        //5 - MUAY THAI
        //6 - ZUMBA

        // GET: Servicos
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MeusServicos()
        {
            return View();
        }
        public ActionResult Autorizar()
        {
            List<Colaborador> lista = listagem.listaAutorizaColaborador();
            return View(lista);
        }
        public ActionResult AutorizarEsporte()
        {
            List<Esporte> lista = listagem.listaAutorizaEsporte();
            ViewBag.ItemData = lista.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public void salvaAutorizacao(int idColab)
        {
            listagem.salvaAutorizacao(idColab);
        }
        public void bloqueiaAutorizacao(int idColab)
        {
            listagem.bloqueiaAutorizacao(idColab);
        }
        public ActionResult MuayThai()
        {
            List<Colaborador> colab = listagem.listaPersonal(1);
            Session["servico"] = 1;
            if (colab.Count == 0)
            {
                ViewBag.Message = "Atualmente não encontramos nenhum personal que ofereça essa modalidade.";
            }
            ViewBag.ItemData = colab.ToList();
            return View("~/Views/Servicos/Index.cshtml");
        }
        public ActionResult Funcional()
        {
            List<Colaborador> colab = listagem.listaPersonal(2);
            Session["servico"] = 2;
            if (colab.Count == 0)
            {
                ViewBag.Message = "Atualmente não encontramos nenhum personal que ofereça essa modalidade.";
            }
            ViewBag.ItemData = colab.ToList();
            return View("~/Views/Servicos/Index.cshtml");
        }
        public ActionResult Corrida()
        {
            List<Colaborador> colab = listagem.listaPersonal(3);
            Session["servico"] = 3;
            if (colab.Count == 0)
            {
                ViewBag.Message = "Atualmente não encontramos nenhum personal que ofereça essa modalidade.";
            }
            ViewBag.ItemData = colab.ToList();
            return View("~/Views/Servicos/Index.cshtml");
        }
        public ActionResult Slackline()
        {
            List<Colaborador> colab = listagem.listaPersonal(4);
            Session["servico"] = 4;
            if (colab.Count == 0)
            {
                ViewBag.Message = "Atualmente não encontramos nenhum personal que ofereça essa modalidade.";
            }
            ViewBag.ItemData = colab.ToList();
            return View("~/Views/Servicos/Index.cshtml");
        }
        public ActionResult Surf()
        {
            List<Colaborador> colab = listagem.listaPersonal(5);
            Session["servico"] = 5;
            if (colab.Count == 0)
            {
                ViewBag.Message = "Atualmente não encontramos nenhum personal que ofereça essa modalidade.";
            }
            ViewBag.ItemData = colab.ToList();
            return View("~/Views/Servicos/Index.cshtml");
        }
        public ActionResult Musculacao()
        {
            List<Colaborador> colab = listagem.listaPersonal(6);
            Session["servico"] = 6;
            if (colab.Count == 0)
            {
                ViewBag.Message = "Atualmente não encontramos nenhum personal que ofereça essa modalidade.";
            }
            ViewBag.ItemData = colab.ToList();
            return View("~/Views/Servicos/Index.cshtml");
        }
        
        public ActionResult SaibaMais(int id)
        {
            var tes = Session["servico"];
            Colaborador list = listagem.listaSaibaMais(id);
            list.DATA_NASCIMENTO_FORMATADA = list.DATA_NASCIMENTO.ToString("dd/MM/yyyy");
            return View(list);
        }
        [HttpPost]
        public void NovaModalidade(String esporte, CadastroHorarios model)
        {
            int now = 0;
            int final = 0;
            DateTime dataFormatada;
            DateTime dataTransf;
            DateTime dataFinal;
            listagem.insereModalidades(int.Parse(Session["ID"].ToString()), int.Parse(esporte));
            int x = 0;
            for (int i = 0; i < ((IList)model.data).Count; i++)
            {


                
                if (model.data[i] == "")
                {
                    if (model.frequencia[x] == "")
                    {
                        model.frequencia[x] = "mensal";
                    }
                    //String dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                    //dataTransf = DateTime.Parse(dataAtual);
                    //dataFinal = dataTransf.AddMonths(model.meses[x]);
                    //String data;
                    //String dataForm;
                    //CultureInfo culture = new CultureInfo("pt-BR");
                    //DateTimeFormatInfo dtfi = culture.DateTimeFormat;



                    for (int y = 0; y < model.horarios.Count; y++)
                    {
                        if (model.horarios[y] != "end")
                        {
                            String dataAtual = DateTime.Now.ToString("dd/MM/yyyy");
                            dataTransf = DateTime.Parse(dataAtual);
                            dataFinal = dataTransf.AddMonths(model.meses[x]+1);
                            String data;
                            String dataForm;
                            CultureInfo culture = new CultureInfo("pt-BR");
                            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                            dataForm = dataTransf.ToString("dd/MM/yyyy") + " " + model.horarios[y];
                            dataTransf = DateTime.Parse(dataForm);
                            while (dataTransf <= dataFinal)
                            {
                                data = dtfi.GetDayName(dataTransf.DayOfWeek);
                                now = DateTime.Now.Month;
                                final = dataFinal.Month;
                                if (dataTransf.Month != now && dataTransf.Month != final)
                                {
                                    if (model.diaSemana[x] == data)
                                    {

                                        ModalidadesAulas.NovaModalidade(int.Parse(esporte), int.Parse(Session["ID"].ToString()), dataTransf, model.frequencia[i], model.meses[x], model.diaSemana[x], @Session["ID"].ToString(), Session["TIPO"].ToString());
                                    }
                                }
                                dataTransf = dataTransf.AddDays(1);

                            }
                            model.horarios[y] = "";
                        }
                        else
                        {
                            model.horarios[y] = "";
                            y = model.horarios.Count;
                        }
                        
                    }
                }
                else 
                {
                    if (model.frequencia[x] == "")
                    {
                        model.frequencia[x] = "diaria";
                    }
                    for (int g = 0; g < model.horarios.Count; g++)
                    {
                        if (model.horarios[g] != "end" && model.horarios[g] != "")
                        {
                            string newFormat = DateTime.ParseExact(model.data[i], "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                            dataFormatada = DateTime.Parse(model.data[i] + " " + model.horarios[g].ToString());
                            ModalidadesAulas.NovaModalidade(int.Parse(esporte), int.Parse(Session["ID"].ToString()), dataFormatada, model.frequencia[i].ToString(), int.Parse(model.meses[x].ToString()), model.diaSemana[x].ToString(), @Session["ID"].ToString(), Session["TIPO"].ToString());
                            x++;
                            model.horarios[g] = "";
                        }
                        else if(model.horarios[g] == "end")
                        {
                            model.horarios[g] = "";
                            g = model.horarios.Count;
                        }
                    }
                }
            }
            List<HorariosColaborador> horarios = ModalidadesAulas.ListaHorariosColaborador(Session["ID"].ToString(), @Session["TIPO"].ToString());
            ViewBag.ItemData = horarios.ToList();
            Response.Redirect("~/Home/Modalidades");
        }
        public ActionResult BuscarDiasDisponiveis(String id, String idEsp)
        {
            List<HorariosColaborador> horariosDisp = listagem.Dias(id, idEsp);
            ViewBag.ItemData = horariosDisp.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuscarHorariosDisponiveis(String dia, int idColab)
        {
            List<HorariosColaborador> horariosDisp = listagem.Horarios(dia, idColab);
            ViewBag.ItemData = horariosDisp.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuscarDiaSemanaDisponiveis(int id)
        {
            List<RangeHorarios> dias = listagem.diasSemana(id);
            ViewBag.ItemData = dias.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuscarMesesDisponiveis(String diaSemana)
        {
            List<RangeHorarios> Meses = listagem.Meses(diaSemana);
            for (int i = 0; i < Meses.Count(); i++)
            {
                string mesExtenso = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(int.Parse(Meses[i].DiaSemana)).ToLower();
                Meses[i].MesExtenso = char.ToUpper(mesExtenso[0]) + mesExtenso.Substring(1);
            }
            ViewBag.ItemData = Meses.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BuscarMesesHorariosDisponiveis(String diaSemana, String mesAg)
        {
            List<RangeHorarios> MesesHorarios = listagem.retornaHorarios(diaSemana, mesAg);
            ViewBag.ItemData = MesesHorarios.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public void SalvarValores(String valorDiario, String valorMensal, int idColab, int idEsporte)
        {
            listagem.salvaValores(valorDiario, valorMensal, idColab, idEsporte, @Session["ID"].ToString(), Session["TIPO"].ToString());
        }

        public void SalvaAgendamento(String data, String horario, int idColab)
        {
            listagem.SalvaAgendamento(data, horario, int.Parse(Session["ID"].ToString()), idColab, int.Parse(Session["servico"].ToString()), Session["TIPO"].ToString());
            listagem.AlteraStatus(data, horario, int.Parse(Session["ID"].ToString()), idColab, int.Parse(Session["servico"].ToString()), Session["TIPO"].ToString());
            Colaborador list = listagem.listaSaibaMais(idColab);
            String esporte = listagem.listaEsporte(int.Parse(Session["servico"].ToString()));
            listagem.envioEmail(list.EMAIL, list.NOME, Session["USUARIO"].ToString(), data, horario, esporte);
        }
        public void SalvaAgendamentoMensal(String data, String horario, String mes, int idColab)
        {
            List<Agendamentos> listaHorarios = listagem.ListaHorariosDias(data, horario, mes, idColab, int.Parse(Session["servico"].ToString()));
            for (int i = 0; i < listaHorarios.Count; i++)
            {
                listagem.SalvaAgendamentoMensal(listaHorarios[i].DATA, idColab, int.Parse(Session["ID"].ToString()), int.Parse(Session["servico"].ToString()), Session["TIPO"].ToString());
            }
            for (int i = 0; i < listaHorarios.Count; i++)
            {
                listagem.AlteraStatusMensal(data, listaHorarios[i], int.Parse(Session["ID"].ToString()), idColab, int.Parse(Session["servico"].ToString()), Session["TIPO"].ToString());
            }
            Colaborador list = listagem.listaSaibaMais(idColab);
            String esporte = listagem.listaEsporte(int.Parse(Session["servico"].ToString()));
            listagem.envioEmailAsync(list.EMAIL,list.NOME, Session["USUARIO"].ToString(), mes, data,horario, esporte);
        }
        public ActionResult HorariosDaSemana(int idColab, int idEsporte)
        {
            List<Agendamentos> listaAgendamentos = listagem.ListaHorariosDaSemana(idColab, idEsporte);
            for (int i = 0; i < listaAgendamentos.Count; i++)
            {
                listaAgendamentos[i].DATAFORMATADA = listaAgendamentos[i].DATA.ToString("dd/MM/yyyy");
            }
            ViewBag.ItemData = listaAgendamentos.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HorariosFuturos(int idColab, int idEsporte)
        {
            List<Agendamentos> listaAgendamentos = listagem.ListaHorariosFuturos(idColab, idEsporte);
            for (int i = 0; i < listaAgendamentos.Count; i++)
            {
                listaAgendamentos[i].DATAFORMATADA = listaAgendamentos[i].DATA.ToString("dd/MM/yyyy");
            }
            ViewBag.ItemData = listaAgendamentos.ToList();
            return Json(ViewBag.ItemData, JsonRequestBehavior.AllowGet);
        }
        public void NovoEsporte(String novoEsporte)
        {
            listagem.novoEsporte(novoEsporte, Session["ID"].ToString(), Session["TIPO"].ToString());
            Response.Redirect("~/Home/Modalidades");
        }

    }
}