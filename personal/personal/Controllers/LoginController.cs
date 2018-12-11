using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
using System.Security;
using personal.Models;
using System.Data.SqlClient;
using personal.Repositório;
using System.Configuration;
using System.IO;

namespace personal.Controllers
{
    public class LoginController : Controller
    {
        private static Security security = new Security();
        // GET: Login
        [HttpPost]
        public ActionResult Index(String verificacao, String email, String password)
        {
            //aluno
            if (verificacao == "on")
            {
                String retornoCrip = security.Criptografa(password);
                SqlDataReader retorno = novoLogin.consultaLogin(email, retornoCrip, 'U');
                
                if (retorno.HasRows)
                {
                    String usuario = novoLogin.consultaControle(email,"U");
                    String[] value = usuario.Split('/');
                    
                    Session["USUARIO"] = value[0];
                    Session["ID"] = value[1];
                    Session["TIPO"] = 'U';
                    
                    //existe o usuário mencionado
                    ViewBag.Message = "• Usuário ou senha incorreto.";
                    return View("~/Views/Home/Servicos.cshtml");
                }
            }
            else
            {
                //personal trainer
                String retornoCrip = security.Criptografa(password);
                SqlDataReader retorno = novoLogin.consultaLogin(email, retornoCrip, 'P');

                if (retorno.HasRows)
                {
                    String usuario = novoLogin.consultaControle(email,"P");

                    String[] value = usuario.Split('/');

                    Session["USUARIO"] = value[0];
                    Session["ID"] = value[1];
                    Session["TIPO"] = "P";
                    //existe o usuário mencionado
                    return View("~/Views/Home/Servicos.cshtml");
                }
            }

            // usuário inexistente
            ViewBag.Message = "• Usuário ou senha incorreto.";
            return View("~/Views/Home/Login.cshtml");
        }
        public ActionResult NovoLogin(String nome, String email, String cpf, String dtNascimento, String senhaLogin)
        {
            bool cpfValidado = security.validaCPF(cpf);
            bool emailValidado = security.validaEmail(email);
            if (cpfValidado.Equals(false) )
            {
                //cpf inválido
                ViewBag.Message = "• CPF inválido.";
                return View("~/Views/Home/NovoUsuario.cshtml");
                
            }else if (emailValidado.Equals(true))
            {
                //email já existente
                ViewBag.Message = "• Email já cadastrado.";
                return View("~/Views/Home/NovoUsuario.cshtml");
                
            }
            String retornoCrip = security.Criptografa(senhaLogin);
            String query = novoLogin.insereLoginUsuario(nome,email,cpf,dtNascimento, retornoCrip);
            String[] value = query.Split('/');

            String id = novoLogin.consultaIdUsuario(cpf);
            Session["ID"] = id;
            Session["Usuario"] = nome;
            Session["TIPO"] = "U";

            novoLogin.insereRegistroUsuario(value[0],value[1], @Session["ID"].ToString(), Session["TIPO"].ToString());


            return View("~/Views/Home/Servicos.cshtml");
        }

        [HttpPost]
        public ActionResult NovoColaborador(String nome, String cpf, String cref, String uf, String email, String dtNascimento, String especialidade, String formacao, String sobre, String senhaLogin, HttpPostedFileBase file, String metodologia)
        {
            string filename = "";
            string filepath = "";
            
            bool cpfValidado = security.validaCPF(cpf);
            bool emailValidado = security.validaEmailColaborador(email);
            if (cpfValidado.Equals(false))
            {
                //    //cpf inválido
                ViewBag.Message = "• CPF inválido";
                return View("~/Views/Home/NovoColaborador.cshtml");
            }
            else if (emailValidado.Equals(true))
            {
                //email já existente
                ViewBag.Message = "• Email existente";
                return View("~/Views/Home/NovoColaborador.cshtml");

            }
            try
            {
                if (file.ContentLength > 0)
                {
                    filename = Path.GetFileName(file.FileName);
                    filepath = Path.Combine(Server.MapPath("~/FileUpload"), filename);
                    file.SaveAs(filepath);
                }
            }
            catch
            {
                ViewBag.Message = "• Erro ao salvar arquivo";
                return View();
            }
            String retornoCrip = security.Criptografa(senhaLogin);
            String query = novoLogin.insereLoginPersonal(nome, email, cpf, cref, uf, dtNascimento, especialidade, formacao, sobre, retornoCrip, filename, filepath,metodologia);
            String[] value = query.Split('/');

            String id = novoLogin.consultaId(cpf);
            Session["ID"] = id;
            Session["Usuario"] = nome;
            Session["TIPO"] = "P";

            novoLogin.insereRegistroUsuario(value[0], value[1], @Session["ID"].ToString(), Session["TIPO"].ToString());

            List<HorariosColaborador> horarios = ModalidadesAulas.ListaHorariosColaborador(Session["ID"].ToString(), @Session["TIPO"].ToString());
            ViewBag.ItemData = horarios.ToList();
            return View("~/Views/Home/Modalidades.cshtml");
        }
       

    }
} 