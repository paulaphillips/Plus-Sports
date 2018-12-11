using personal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace personal.Repositório
{
    public class listagem
    {
        public static List<Uf> listaUF()
        {
            List<Uf> ufs = new List<Uf>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select * from TB_UF";

            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    ufs.Add(new Uf()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("ID")),
                        Estado = reader.GetString(reader.GetOrdinal("ESTADO")),
                        Sigla = reader.GetString(reader.GetOrdinal("SIGLA")),

                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return ufs;
        }
        public static List<Colaborador> listaPersonal(int modalidade)
        {
            List<Colaborador> colaboradores = new List<Colaborador>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select distinct cast(round((h.estrelas/h.divisao) * 2, 0) / 2 as decimal(6,2))  as soma, c.ID_COLABORADOR, c.NOME, C.SOBRE, c.FORMACAO, c.CREF, c.UF_CREF, c.IMAGE_NAME, CAST(f.ID AS decimal(6,2)) as id from tb_colaborador as C join TB_FORMACAO as f on f.DESC_FORMACAO = c.FORMACAO inner join tb_modalidades as m on c.id_colaborador = m.id_colaborador inner join (select id_colaborador, sum(rating_stars) as estrelas,Count(id_colaborador) as divisao from tb_horarios_colaborador group by id_colaborador) h on c.id_colaborador = h.id_colaborador order by soma desc, id desc";


            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    colaboradores.Add(new Colaborador()
                    {
                        ID_COLABORADOR = reader.GetInt32(reader.GetOrdinal("ID_COLABORADOR")),
                        NOME = reader.GetString(reader.GetOrdinal("NOME")),
                        SOBRE = reader.GetString(reader.GetOrdinal("SOBRE")),
                        CREF = reader.GetString(reader.GetOrdinal("CREF")),
                        UF_CREF = reader.GetString(reader.GetOrdinal("UF_CREF")),
                        FORMACAO = reader.GetString(reader.GetOrdinal("FORMACAO")),
                        IMAGE_NAME = reader.GetString(reader.GetOrdinal("IMAGE_NAME")),
                        RATING_STARS = reader.GetDecimal(reader.GetOrdinal("SOMA")).ToString(),
                        ID_ESPORTE = modalidade,
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return colaboradores;
        }
        public static Colaborador listaSaibaMais(int id)
        {
            Colaborador lista = new Colaborador();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select ID_COLABORADOR,EMAIL , NOME,DATA_NASCIMENTO,CREF,UF_CREF, FORMACAO ,ESPECIALIDADE, SOBRE, IMAGE_NAME, METODOLOGIA, STATUS from TB_COLABORADOR where ID_COLABORADOR =" + id;


            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {

                    lista.ID_COLABORADOR = reader.GetInt32(reader.GetOrdinal("ID_COLABORADOR"));
                    lista.EMAIL = reader.GetString(reader.GetOrdinal("EMAIL"));
                    lista.NOME = reader.GetString(reader.GetOrdinal("NOME"));
                    lista.DATA_NASCIMENTO = reader.GetDateTime(reader.GetOrdinal("DATA_NASCIMENTO"));
                    lista.CREF = reader.GetString(reader.GetOrdinal("CREF"));
                    lista.UF_CREF = reader.GetString(reader.GetOrdinal("UF_CREF"));
                    lista.ESPECIALIDADE = reader.GetString(reader.GetOrdinal("ESPECIALIDADE"));
                    lista.FORMACAO = reader.GetString(reader.GetOrdinal("FORMACAO"));
                    lista.SOBRE = reader.GetString(reader.GetOrdinal("SOBRE"));
                    lista.IMAGE_NAME = reader.GetString(reader.GetOrdinal("IMAGE_NAME"));
                    lista.METODOLOGIA = reader.GetString(reader.GetOrdinal("METODOLOGIA"));
                    lista.STATUS = reader.GetString(reader.GetOrdinal("STATUS"));

                }
            }
            comando.Dispose();
            connection.Close();

            return lista;
        }
        public static List<Colaborador> listaPerfil(String tipo, String ID)
        {
            String sSql = "";
            Colaborador lista = new Colaborador();
            List<Colaborador> listaColab = new List<Colaborador>(); ;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            if (tipo == "U")
            {
                sSql = "select NOME,EMAIL,CPF, DATA_NASCIMENTO from TB_USUARIO where ID_USUARIO =" + ID;

                var comando = new SqlCommand(sSql, connection);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        lista.NOME = reader.GetString(reader.GetOrdinal("NOME"));
                        lista.EMAIL = reader.GetString(reader.GetOrdinal("EMAIL"));
                        lista.CPF = reader.GetString(reader.GetOrdinal("CPF"));

                        //lista.DATA_NASCIMENTO = reader.GetDateTime(reader.GetOrdinal("DATA_NASCIMENTO"));
                    }
                }
            }
            else if (tipo == "P")
            {
                sSql = "select ID_COLABORADOR,NOME,EMAIL, IMAGE_NAME, CPF, DATA_NASCIMENTO, ESPECIALIDADE, CREF,UF_CREF, FORMACAO ,SOBRE,STATUS,IMAGE_NAME,METODOLOGIA from TB_COLABORADOR where ID_COLABORADOR =" + ID;

                var comando = new SqlCommand(sSql, connection);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        lista.ID_COLABORADOR = reader.GetInt32(reader.GetOrdinal("ID_COLABORADOR"));
                        lista.NOME = reader.GetString(reader.GetOrdinal("NOME"));
                        lista.EMAIL = reader.GetString(reader.GetOrdinal("EMAIL"));
                        lista.IMAGE_NAME = reader.GetString(reader.GetOrdinal("IMAGE_NAME"));
                        lista.CPF = reader.GetString(reader.GetOrdinal("CPF"));
                        lista.DATA_NASCIMENTO = reader.GetDateTime(reader.GetOrdinal("DATA_NASCIMENTO"));
                        lista.CREF = reader.GetString(reader.GetOrdinal("CREF"));
                        lista.UF_CREF = reader.GetString(reader.GetOrdinal("UF_CREF"));
                        lista.ESPECIALIDADE = reader.GetString(reader.GetOrdinal("ESPECIALIDADE"));
                        lista.FORMACAO = reader.GetString(reader.GetOrdinal("FORMACAO"));
                        lista.SOBRE = reader.GetString(reader.GetOrdinal("SOBRE"));
                        lista.METODOLOGIA = reader.GetString(reader.GetOrdinal("METODOLOGIA"));
                        lista.STATUS = reader.GetString(reader.GetOrdinal("STATUS"));
                    }
                }
            }
            listaColab.Add(lista);
            return listaColab;
        }
        public static List<HorariosColaborador> Dias(String id, String idEsp)
        {
            List<HorariosColaborador> horarios = new List<HorariosColaborador>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            //String sSql = "select Data, id from TB_HORARIOS_COLABORADOR where ID_COLABORADOR =" + id + " and ID_ESPORTE=" + idEsp + " and DIA_SEMANA = '0' AND STATUS <> 'AGENDADO' AND DATA >= GETDATE() group by Data, id order by data";
            String sSql = "select Data, id from TB_HORARIOS_COLABORADOR where ID_COLABORADOR =" + id + " and ID_ESPORTE=" + idEsp + " and DIA_SEMANA = '0' AND STATUS <> 'AGENDADO' group by Data, id order by data";

            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    horarios.Add(new HorariosColaborador()
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        DATA = reader.GetDateTime(reader.GetOrdinal("DATA")),
                        DATAFORMATADA = reader.GetDateTime(reader.GetOrdinal("DATA")).ToString("dd/MM/yyyy"),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return horarios;
        }
        public static List<HorariosColaborador> Horarios(String dia, int idColab)
        {
            List<HorariosColaborador> horarios = new List<HorariosColaborador>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select distinct id,convert(Varchar(11),DATA,114) as DATA from TB_HORARIOS_COLABORADOR where convert(Varchar(11),DATA,103) like '" + dia + "' and ID_COLABORADOR = " + idColab + "and DIA_SEMANA = '0'  AND STATUS <> 'AGENDADO' ";

            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    horarios.Add(new HorariosColaborador()
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        DATAFORMATADA = reader.GetString(reader.GetOrdinal("DATA")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return horarios;
        }
        public static List<RangeHorarios> diasSemana(int dia)
        {
            List<RangeHorarios> dias = new List<RangeHorarios>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select distinct DIA_SEMANA from TB_HORARIOS_COLABORADOR where ID_COLABORADOR='" + dia + "' and DIA_SEMANA <> '0' AND STATUS <> 'AGENDADO' ";

            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    dias.Add(new RangeHorarios()
                    {
                        DiaSemana = reader.GetString(reader.GetOrdinal("DIA_SEMANA")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return dias;
        }
        public static List<RangeHorarios> Meses(String diaSemana)
        {
            List<RangeHorarios> dias = new List<RangeHorarios>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select distinct MONTH(CONVERT(VARCHAR(19),DATA, 120)) as Mes from TB_HORARIOS_COLABORADOR where DIA_SEMANA = '" + diaSemana + "' and DATA > GETDATE() AND STATUS <> 'AGENDADO' ";

            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    dias.Add(new RangeHorarios()
                    {
                        DiaSemana = reader.GetInt32(reader.GetOrdinal("Mes")).ToString(),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return dias;
        }
        public static List<RangeHorarios> retornaHorarios(String diaSemana, String mesAg)
        {
            List<RangeHorarios> dias = new List<RangeHorarios>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select distinct CONVERT(VARCHAR(11),DATA,114) as HORA from TB_HORARIOS_COLABORADOR where DIA_SEMANA = '" + diaSemana + "' and MONTH(CONVERT(VARCHAR(19),DATA, 120)) = '" + mesAg + "'  AND STATUS <> 'AGENDADO' ";

            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    dias.Add(new RangeHorarios()
                    {
                        DiaSemana = reader.GetString(reader.GetOrdinal("HORA")).ToString(),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return dias;
        }
        public static void salvaValores(String valorDiario, String valorMensal, int idColab, int idEsporte, String idUsuario, String tipoUsuario)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String query = "update TB_MODALIDADES set VALORMENSAL ='" + valorMensal + "', VALORDIARIO = '" + valorDiario + "' where ID_COLABORADOR =" + idColab + " AND ID_ESPORTE = " + idEsporte;

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('update TB_MODALIDADES set VALORMENSAL =" + valorMensal + ", VALORDIARIO = " + valorDiario + " where ID_COLABORADOR =" + idColab + " AND ID_ESPORTE = " + idEsporte+"', 'UPDATE','" + idUsuario + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
        public static void insereModalidades(int idColab, int idEsporte)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String query = "insert into TB_MODALIDADES (ID_COLABORADOR,ID_ESPORTE) values(" + idColab + "," + idEsporte + ")";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();
        }
        public static void SalvaAgendamento(String data, String horario, int idUsuario, int idColab, int idEsporte, String tipoUsuario)
        {
            DateTime teste = DateTime.Parse(data);
            String formatacao = teste.ToString("yyyy/dd/MM") + " " + horario;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "insert into TB_AGENDAMENTOS (ID_USUARIO,ID_COLABORADOR,ID_ESPORTE,DATA) values(" + idUsuario + "," + idColab + ", " + idEsporte + ", '" + formatacao + "')";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('insert into TB_AGENDAMENTOS (ID_USUARIO,ID_COLABORADOR,ID_ESPORTE,DATA) values(" + idUsuario + "," + idColab + ", " + idEsporte + ", " + formatacao + ")', 'INSERT','" + idUsuario + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
        public static void AlteraStatus(String data, String horario, int idUsuario, int idColab, int idEsporte, String tipoUsuario)
        {
            DateTime teste = DateTime.Parse(data);
            String formatacao = teste.ToString("yyyy/dd/MM") + " " + horario;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "update TB_HORARIOS_COLABORADOR SET STATUS = 'AGENDADO',ID_USUARIO_AGENDADO =" + idUsuario + " where ID_COLABORADOR=" + idColab + " AND DATA ='" + formatacao + "' AND ID_ESPORTE=" + idEsporte + "";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('update TB_HORARIOS_COLABORADOR SET STATUS = 'AGENDADO',ID_USUARIO_AGENDADO =" + idUsuario + " where ID_COLABORADOR=" + idColab + " AND DATA =" + formatacao + " AND ID_ESPORTE=" + idEsporte + "', 'UPDATE','" + idUsuario + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
        public static List<Agendamentos> listaAgendamentos(int id)
        {
            List<Agendamentos> agendamentos = new List<Agendamentos>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String sSql = "select a.ID_USUARIO, a.ID_COLABORADOR, a.ID_ESPORTE,a.DATA,c.NOME,e.DESCRICAO from TB_AGENDAMENTOS as a JOIN TB_COLABORADOR as c ON c.ID_COLABORADOR = a.ID_COLABORADOR JOIN TB_ESPORTES AS e ON e.ID_ESPORTE = a.ID_ESPORTE where a.DATA > GETDATE() and a.ID_USUARIO = " + id + " order by a.DATA DESC";
            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    agendamentos.Add(new Agendamentos()
                    {
                        DS_ESPORTE = reader.GetString(reader.GetOrdinal("DESCRICAO")),
                        ID_USUARIO = reader.GetInt32(reader.GetOrdinal("ID_USUARIO")),
                        DATA = reader.GetDateTime(reader.GetOrdinal("DATA")),
                        DS_COLABORADOR = reader.GetString(reader.GetOrdinal("NOME")).ToString(),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return agendamentos;
        }
        public static List<Agendamentos> listaAgendamentosConcluidos(int id)
        {
            List<Agendamentos> agendamentos = new List<Agendamentos>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String sSql = "select distinct a.ID_ESPORTE, a.ID_USUARIO, a.ID_COLABORADOR, a.DATA,c.NOME,e.DESCRICAO from TB_AGENDAMENTOS as a JOIN TB_COLABORADOR as c ON c.ID_COLABORADOR = a.ID_COLABORADOR JOIN TB_ESPORTES AS e ON e.ID_ESPORTE = a.ID_ESPORTE where a.DATA < GETDATE() and a.ID_USUARIO = " + id;
            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    agendamentos.Add(new Agendamentos()
                    {
                        DS_ESPORTE = reader.GetString(reader.GetOrdinal("DESCRICAO")),
                        DATA = reader.GetDateTime(reader.GetOrdinal("DATA")),
                        ID_USUARIO = reader.GetInt32(reader.GetOrdinal("ID_USUARIO")),
                        DS_COLABORADOR = reader.GetString(reader.GetOrdinal("NOME")).ToString(),
                        ID_COLABORADOR = reader.GetInt32(reader.GetOrdinal("ID_COLABORADOR")),
                        ID_ESPORTE = reader.GetInt32(reader.GetOrdinal("ID_ESPORTE")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return agendamentos;
        }
        public static List<Agendamentos> listaAgendamentosAvaliar(int id)
        {
            List<Agendamentos> agendamentos = new List<Agendamentos>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String sSql = "select distinct a.ID_ESPORTE, a.ID_USUARIO, a.ID_COLABORADOR, a.DATA,c.NOME,e.DESCRICAO from TB_AGENDAMENTOS as a JOIN TB_COLABORADOR as c ON c.ID_COLABORADOR = a.ID_COLABORADOR JOIN TB_ESPORTES AS e ON e.ID_ESPORTE = a.ID_ESPORTE where a.DATA < GETDATE() and a.ID_USUARIO = " + id;
            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    agendamentos.Add(new Agendamentos()
                    {
                        ID_COLABORADOR = reader.GetInt32(reader.GetOrdinal("ID_COLABORADOR")),
                        ID_ESPORTE = reader.GetInt32(reader.GetOrdinal("ID_ESPORTE")),
                        DS_ESPORTE = reader.GetString(reader.GetOrdinal("DESCRICAO")),
                        ID_USUARIO = reader.GetInt32(reader.GetOrdinal("ID_USUARIO")),
                        DS_COLABORADOR = reader.GetString(reader.GetOrdinal("NOME")).ToString(),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return agendamentos;
        }
        public static List<Agendamentos> ListaHorariosDaSemana(int idColab, int idEsporte)
        {
            List<Agendamentos> agendamentos = new List<Agendamentos>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String sSql = "select e.DESCRICAO, h.DATA, c.NOME from TB_HORARIOS_COLABORADOR as h join TB_ESPORTES as e on e.ID_ESPORTE = h.ID_ESPORTE join TB_USUARIO as c on c.ID_USUARIO = h.ID_USUARIO_AGENDADO where h.ID_COLABORADOR = " + idColab + " and h.ID_ESPORTE = " + idEsporte + " and h.STATUS = 'AGENDADO' and h.DATA >= GETDATE() and h.DATA <= DATEADD(week, 1, GETDATE())";

            var comando = new SqlCommand(sSql, connection);
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    agendamentos.Add(new Agendamentos()
                    {
                        DS_ESPORTE = reader.GetString(reader.GetOrdinal("DESCRICAO")).ToString(),
                        DATA = reader.GetDateTime(reader.GetOrdinal("DATA")),
                        DS_COLABORADOR = reader.GetString(reader.GetOrdinal("NOME")).ToString(),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return agendamentos;
        }
        public static List<Agendamentos> ListaHorariosFuturos(int idColab, int idEsporte)
        {
            List<Agendamentos> agendamentos = new List<Agendamentos>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String sSql = "select e.DESCRICAO, h.DATA, c.NOME from TB_HORARIOS_COLABORADOR as h join TB_ESPORTES as e on e.ID_ESPORTE = h.ID_ESPORTE join TB_USUARIO as c on c.ID_USUARIO = h.ID_USUARIO_AGENDADO where h.ID_COLABORADOR = " + idColab + " and h.ID_ESPORTE = " + idEsporte + " and h.STATUS = 'AGENDADO' and h.DATA >=  GETDATE()";

            var comando = new SqlCommand(sSql, connection);
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    agendamentos.Add(new Agendamentos()
                    {
                        DS_ESPORTE = reader.GetString(reader.GetOrdinal("DESCRICAO")).ToString(),
                        DATA = reader.GetDateTime(reader.GetOrdinal("DATA")),
                        DS_COLABORADOR = reader.GetString(reader.GetOrdinal("NOME")).ToString(),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return agendamentos;
        }
        public static void SalvaAgendamentoMensal(DateTime horario, int idColab, int idUsuario, int idEsporte, String tipoUsuario)
        {
            String DATAFORMATADA = horario.ToString("yyyy/dd/MM HH:mm:ss.000");

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "insert into TB_AGENDAMENTOS (ID_USUARIO,ID_COLABORADOR,ID_ESPORTE,DATA) values(" + idUsuario + "," + idColab + ", " + idEsporte + ", '" + DATAFORMATADA + "')";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('insert into TB_AGENDAMENTOS (ID_USUARIO,ID_COLABORADOR,ID_ESPORTE,DATA) values(" + idUsuario + "," + idColab + ", " + idEsporte + ", " + DATAFORMATADA + ")', 'INSERT','" + idUsuario + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
        public static List<Agendamentos> ListaHorariosDias(String data, String horario, String mes, int idColab, int idEsporte)
        {
            String hora = horario.Substring(0, 2);
            List<Agendamentos> agendamentos = new List<Agendamentos>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String sSql = "select * from TB_HORARIOS_COLABORADOR where ID_COLABORADOR = " + idColab + " and DIA_SEMANA ='" + data + "' AND DATEPART(MONTH, DATA) = '" + mes + "' AND CONVERT(VARCHAR(11),DATA,114) LIKE '%" + hora + "%'";
            ;

            var comando = new SqlCommand(sSql, connection);
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    agendamentos.Add(new Agendamentos()
                    {
                        DATA = reader.GetDateTime(reader.GetOrdinal("DATA")),
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return agendamentos;
        }
        public static void AlteraStatusMensal(String diaSemana, Agendamentos data, int idUsuario, int idColab, int idEsporte, String tipoUsuario)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "update TB_HORARIOS_COLABORADOR SET STATUS = 'AGENDADO',ID_USUARIO_AGENDADO =" + idUsuario + " where ID_COLABORADOR=" + idColab + " AND DATA ='" + data.DATA + "' AND ID_ESPORTE=" + idEsporte + " AND DIA_SEMANA ='" + diaSemana + "' AND ID=" + data.ID + "";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "update TB_HORARIOS_COLABORADOR SET STATUS = AGENDADO,ID_USUARIO_AGENDADO =" + idUsuario + " where ID_COLABORADOR=" + idColab + " AND DATA =" + data.DATA + " AND ID_ESPORTE=" + idEsporte + " AND DIA_SEMANA =" + diaSemana + " AND ID=" + data.ID + ")', 'UPDATE','" + idUsuario + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
        public static void SalvaAvaliacao(int idEsp, int idPersonal, String stars, int idUsuario, String dtHora, String usuario, String tipoUsuario)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "update TB_HORARIOS_COLABORADOR set RATING_STARS = " + stars + " where ID_ESPORTE = " + idEsp + " and ID_COLABORADOR = " + idPersonal + " and ID_USUARIO_AGENDADO =" + idUsuario + " and CONVERT(VARCHAR(10), DATA, 103) + ' ' + CONVERT(VARCHAR(8), DATA, 108) = '" + dtHora + "'";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('update TB_HORARIOS_COLABORADOR set RATING_STARS = " + stars + " where ID_ESPORTE = " + idEsp + " and ID_COLABORADOR = " + idPersonal + " and ID_USUARIO_AGENDADO =" + idUsuario + " and CONVERT(VARCHAR(10), DATA, 103) + ' ' + CONVERT(VARCHAR(8), DATA, 108) = '" + dtHora + "'', 'UPDATE','" + usuario + "','"+ tipoUsuario + "','"+DateTime.Now+"'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
        public static String listaEsporte(int id)
        {
            String esp = "";
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select DESCRICAO from TB_ESPORTES where ID_ESPORTE = " + id;


            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {

                    esp = reader.GetString(0);
                }
            }
            comando.Dispose();
            connection.Close();

            return esp;
        }
        public static void envioEmailAsync(String email, String nomeColaborador, String nomeUsuario, String mes, String data, String horario, String esporte)
        {
            if(mes == "1")
            {
                mes = "Janeiro";
            }else if(mes == "2")
            {
                mes = "Fevereiro";
            }
            else if(mes == "3")
            {
                mes = "Março";
            }else if (mes == "4")
            {
                mes = "Abril";
            }
            else if (mes == "5")
            {
                mes = "Maio";
            }
            else if (mes == "6")
            {
                mes = "Junho";
            }
            else if (mes == "7")
            {
                mes = "Julho";
            }
            else if (mes == "8")
            {
                mes = "Agosto";
            }
            else if (mes == "9")
            {
                mes = "Setembro";
            }
            else if (mes == "10")
            {
                mes = "Outubro";
            }
            else if (mes == "11")
            {
                mes = "Novembro";
            }
            else if (mes == "12")
            {
                mes = "Dezembro";
            }
            String remetenteEmail = "paula.phillips150896@gmail.com";

            MailMessage mail = new MailMessage();

            mail.To.Add("phillips.paula@hotmail.com");

            mail.From = new MailAddress(remetenteEmail, "Plus Sports", System.Text.Encoding.UTF8);

            mail.Subject = "Novo Agendamento! Aluno: "+ nomeUsuario;

            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            mail.Body = nomeColaborador +", </br> Foi agendado um plano mensal pelo aluno " + nomeUsuario + " para a modalidade "+ esporte + " no mês de "+ mes + " as "+horario+".";


            mail.BodyEncoding = System.Text.Encoding.UTF8;

            mail.IsBodyHtml = true;

            mail.Priority = MailPriority.High; //Prioridade do E-Mail



            SmtpClient client = new SmtpClient();  //Adicionando as credenciais do seu e-mail e senha:
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(remetenteEmail, "xfwknx08");



            client.Port = 587; // Esta porta é a utilizada pelo Gmail para envio

            client.Host = "smtp.gmail.com"; //Definindo o provedor que irá disparar o e-mail

            client.EnableSsl = true; //Gmail trabalha com Server Secured Layer

            try

            {

                client.Send(mail);

                
            }

            catch (Exception ex)

            {

                String oi = "ops";

            }
        }
        public static void envioEmail(String email, String nomeColaborador, String nomeUsuario,  String data, String horario, String esporte)
        {
            String remetenteEmail = "paula.phillips150896@gmail.com";

            MailMessage mail = new MailMessage();

            mail.To.Add("phillips.paula@hotmail.com");

            mail.From = new MailAddress(remetenteEmail, "Plus Sports", System.Text.Encoding.UTF8);

            mail.Subject = "Novo Agendamento! Aluno: " + nomeUsuario;

            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            mail.Body = nomeColaborador + ", </br> Foi agendada uma aula pelo aluno " + nomeUsuario + " para a modalidade " + esporte + " no dia " + data + " as " + horario + ".";


            mail.BodyEncoding = System.Text.Encoding.UTF8;

            mail.IsBodyHtml = true;

            mail.Priority = MailPriority.High; //Prioridade do E-Mail



            SmtpClient client = new SmtpClient();  //Adicionando as credenciais do seu e-mail e senha:
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(remetenteEmail, "xfwknx08");



            client.Port = 587; // Esta porta é a utilizada pelo Gmail para envio

            client.Host = "smtp.gmail.com"; //Definindo o provedor que irá disparar o e-mail

            client.EnableSsl = true; //Gmail trabalha com Server Secured Layer

            try

            {

                client.Send(mail);


            }

            catch (Exception ex)

            {

                String oi = "ops";

            }
        }
        public static void novoEsporte(String esporte, String usuario, String tipoUsuario)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "insert into TB_SOLICITA_MODALIDADE (MODALIDADES,STATUS) Values ('"+esporte+"', 'PENDENTE')";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('insert into TB_SOLICITA_MODALIDADE (MODALIDADES,STATUS) Values (" + esporte + ", PENDENTE)', 'INSERT','" + usuario + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
        public static List<Colaborador> listaAutorizaColaborador()
        {
            List<Colaborador> colaboradores = new List<Colaborador>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select * from tb_colaborador where status = 'EM PROCESSAMENTO'";


            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    colaboradores.Add(new Colaborador()
                    {
                        ID_COLABORADOR = reader.GetInt32(reader.GetOrdinal("ID_COLABORADOR")),
                        NOME = reader.GetString(reader.GetOrdinal("NOME")),
                        CREF = reader.GetString(reader.GetOrdinal("CREF")),
                        UF_CREF = reader.GetString(reader.GetOrdinal("UF_CREF")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();
            return colaboradores;

        }
        public static List<Esporte> listaAutorizaEsporte()
        {
            List<Esporte> colaboradores = new List<Esporte>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select * from TB_SOLICITA_MODALIDADE where status = 'PENDENTE'";


            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    colaboradores.Add(new Esporte()
                    {
                        ID_NOVOESPORTE = reader.GetInt32(reader.GetOrdinal("ID")),
                        DESCRICAO = reader.GetString(reader.GetOrdinal("MODALIDADES")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();
            return colaboradores;

        }
        public static void salvaAutorizacao(int id)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "update TB_COLABORADOR set STATUS = 'AUTORIZADO' where ID_COLABORADOR =" + id;

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('update TB_COLABORADOR set STATUS = AUTORIZADO where ID_COLABORADOR =" + id+ "', 'UPDATE','A','A','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
        public static void bloqueiaAutorizacao(int id)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "update TB_COLABORADOR set STATUS = 'NAO AUTORIZADO' where ID_COLABORADOR =" + id;

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('update TB_COLABORADOR set STATUS = NAO AUTORIZADO where ID_COLABORADOR =" + id + "', 'UPDATE','A','A','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
        }
    }
}