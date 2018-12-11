using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using personal.Models;
using System.Text.RegularExpressions;


namespace personal.Repositório
{
    public class ModalidadesAulas
    {
        public static void NovaModalidade(int esporte, int id, DateTime dataFormatada, String frequencia, int meses, String diaSemana, String idUsuario, String tipUsuario)
        {
            if (frequencia == "diaria")
            {
                String query = "";
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
                connection.Open();


                query = "insert into TB_HORARIOS_COLABORADOR (ID_ESPORTE,ID_COLABORADOR,DATA,FREQUENCIA,DIA_SEMANA,STATUS,RATING_STARS) values (" + esporte + "," + id + ",'" + dataFormatada + "', 0,'0', 'DISPONIVEL', 0)";

                var comando = new SqlCommand(query, connection);
                comando.ExecuteReader();
                comando.Dispose();
                connection.Close();


                SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
                connections.Open();
                String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('insert into TB_HORARIOS_COLABORADOR (ID_ESPORTE,ID_COLABORADOR,DATA,FREQUENCIA,DIA_SEMANA,STATUS,RATING_STARS) values (" + esporte + "," + id + "," + dataFormatada + ", 0,0, DISPONIVEL, 0)', 'INSERT','" + idUsuario + "','" + tipUsuario + "','" + DateTime.Now + "'  )";

                var comandos = new SqlCommand(querys, connections);
                comandos.ExecuteReader();
                comandos.Dispose();
                connections.Close(); 
            }
            else
            {
                
                String query = "";
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
                connection.Open();

                query = "insert into TB_HORARIOS_COLABORADOR (ID_ESPORTE,ID_COLABORADOR,DATA,FREQUENCIA,DIA_SEMANA,STATUS,RATING_STARS) values (" + esporte + "," + id + ",'" + dataFormatada + "'," + meses + ",'" + diaSemana + "', 'DISPONIVEL',0)";

                var comando = new SqlCommand(query, connection);
                comando.ExecuteReader();
                comando.Dispose();
                connection.Close();

                SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
                connections.Open();
                String querys = "insert into TB_HORARIOS_COLABORADOR (ID_ESPORTE,ID_COLABORADOR,DATA,FREQUENCIA,DIA_SEMANA,STATUS,RATING_STARS) values ('" + esporte + "," + id + "," + dataFormatada + "," + meses + "," + diaSemana + ", DISPONIVEL,0)', 'UPDATE','" + idUsuario + "','" + tipUsuario + "','" + DateTime.Now + "'  )";

                var comandos = new SqlCommand(querys, connections);
                comandos.ExecuteReader();
                comandos.Dispose();
                connections.Close();
            }
        }
        public static List<HorariosColaborador> ListaHorariosColaborador(String id, String tipoUsuario)
        {
            List<HorariosColaborador> horarios = new List<HorariosColaborador>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select ID_ESPORTE from TB_HORARIOS_COLABORADOR where ID_COLABORADOR =" + id + " group by ID_ESPORTE";
            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    horarios.Add(new HorariosColaborador()
                    {
                        ID_ESPORTE = reader.GetInt32(reader.GetOrdinal("ID_ESPORTE")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return horarios;
        }
        public static List<HorariosColaborador> ListaHorarioPorModalidade(int id, String idUsuario)
        {
            List<HorariosColaborador> horarios = new List<HorariosColaborador>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select * from TB_HORARIOS_COLABORADOR where ID_COLABORADOR = " + idUsuario + " and ID_ESPORTE =" + id + " AND STATUS <> 'AGENDADO' order by DATA";
            var comando = new SqlCommand(sSql, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    horarios.Add(new HorariosColaborador()
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        ID_ESPORTE = reader.GetInt32(reader.GetOrdinal("ID_ESPORTE")),
                        DATAFORMATADA = reader.GetDateTime(reader.GetOrdinal("DATA")).ToString(),

                    });

                }
            }
            comando.Dispose();
            connection.Close();

            return horarios;
        }
        public static void excluirHorario(String ids, String id, String tipoUsuario)
        {
            String[] horarios = Regex.Split(ids, ",");
            for (int i = 0; i < horarios.Length -1;i++) { 
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String query = "delete from TB_HORARIOS_COLABORADOR where id ="+ horarios[i]+" and ID_COLABORADOR ="+id+ " and STATUS <> 'AGENDADO' ";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('delete from TB_HORARIOS_COLABORADOR where id =" + horarios[i] + " and ID_COLABORADOR =" + id + " and STATUS <> AGENDADO', 'DELETE','" + id + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();
            }


        }
        public static void excluirModalidade(int esporte, String id, String tipoUsuario)
        {
           SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
           connection.Open();

            String query = "delete from TB_HORARIOS_COLABORADOR where ID_ESPORTE =" + esporte + " and ID_COLABORADOR=" + id + " and STATUS <> 'AGENDADO' ";

            var comando = new SqlCommand(query, connection);
           comando.ExecuteReader();
           comando.Dispose();
           connection.Close();

            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('delete from TB_HORARIOS_COLABORADOR where ID_ESPORTE =" + esporte + " and ID_COLABORADOR=" + id + " and STATUS <> AGENDADO', 'DELETE','" + id + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();


            SqlConnection connec = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connec.Open();

            String bdQuery = "delete from TB_MODALIDADES where ID_ESPORTE =" + esporte + " and ID_COLABORADOR=" + id;

            var comand = new SqlCommand(bdQuery, connec);
            comand.ExecuteReader();
            comand.Dispose();
            connec.Close();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            conn.Open();
            String q = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('delete from TB_MODALIDADES where ID_ESPORTE =" + esporte + " and ID_COLABORADOR=" + id+"', 'DELETE','" + id + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var com = new SqlCommand(q, conn);
            com.ExecuteReader();
            com.Dispose();
            conn.Close();
        }
    }
   
}

