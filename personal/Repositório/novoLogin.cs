using personal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace personal.Repositório
{
    public class novoLogin
    {
        public static SqlDataReader consultaLogin(String email, String cripPassword, Char usuario)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String queryString = "SELECT LOGIN,USUARIO FROM TB_LOGIN where LOGIN = '" + email + "' AND PASSWORD = '" + cripPassword + "' AND USUARIO = '" + usuario + "'";

            var comando = new SqlCommand(queryString, connection);
            return comando.ExecuteReader();
        }

        public static String insereLoginUsuario(String nome, String email, String cpf, String dtNascimento, String cripPassword)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String query = "insert into TB_LOGIN (LOGIN,PASSWORD,USUARIO) values ('" + email + "','" + cripPassword + "','U')";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();
                      

            SqlConnection connectionInsert = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connectionInsert.Open();
            String queryString = "insert into TB_USUARIO(NOME,EMAIL,CPF,DATA_NASCIMENTO) Values('" + nome + "','" + email + "','" + cpf + "','" + dtNascimento + "')";
            var comandoInsert = new SqlCommand(queryString, connectionInsert);
            comandoInsert.ExecuteReader();
            comandoInsert.Dispose();
            connectionInsert.Close();

            query = "insert into TB_LOGIN(LOGIN, PASSWORD, USUARIO) values(" + email + ", " + cripPassword + ", U)";
            queryString = "insert into TB_USUARIO(NOME,EMAIL,CPF,DATA_NASCIMENTO) Values(" + nome + "," + email + "," + cpf + "," + dtNascimento + ")";
            return query+"/"+queryString;
        }
        public static String insereLoginPersonal(String nome, String email, String cpf, String cref, String crefUf, String dtNascimento, String especialidade, String formacao, String sobre, String cripPassword, String fileName, String filePath,String metodologia)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String query = "insert into TB_LOGIN (LOGIN,PASSWORD,USUARIO) values ('" + email + "','" + cripPassword + "','P')";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();

            SqlConnection connectionInsert = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connectionInsert.Open();
            String queryString = "insert into TB_COLABORADOR(NOME,EMAIL,CPF,CREF,UF_CREF,DATA_NASCIMENTO,ESPECIALIDADE,FORMACAO,SOBRE,DATA,STATUS, IMAGE_NAME, IMAGE_PATH,METODOLOGIA) " +
                                 "Values('" + nome + "','" + email + "','" + cpf + "','" + cref + "','" + crefUf + "','" + dtNascimento + "','" + especialidade + "','" + formacao + "','" + sobre + "','" + DateTime.Today + "', 'EM PROCESSAMENTO', '" + fileName + "','"+ filePath + "','"+metodologia+"')";
            var comandoInsert = new SqlCommand(queryString, connectionInsert);
            comandoInsert.ExecuteReader();
            comandoInsert.Dispose();
            connectionInsert.Close();

            query = "insert into TB_LOGIN (LOGIN,PASSWORD,USUARIO) values (" + email + "," + cripPassword + ",P)";
            queryString = "insert into TB_COLABORADOR(NOME,EMAIL,CPF,CREF,UF_CREF,DATA_NASCIMENTO,ESPECIALIDADE,FORMACAO,SOBRE,DATA,STATUS, IMAGE_NAME, IMAGE_PATH,METODOLOGIA) " +
                                 "Values(" + nome + "," + email + "," + cpf + "," + cref + "," + crefUf + "," + dtNascimento + "," + especialidade + "," + formacao + "," + sobre + "," + DateTime.Today + ", EM PROCESSAMENTO, " + fileName + "," + filePath + "," + metodologia + ")";
            return query + "/" + queryString;
        }

        public static String consultaControle(String email, String tipo)
        {
            String NOME = "";
            String queryString="";
            Colaborador u = new Colaborador();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            if(tipo == "U") { 
                queryString = "SELECT ID_USUARIO, NOME FROM TB_USUARIO where EMAIL = '" + email + "'";

                var comando = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NOME = reader.GetString(reader.GetOrdinal("NOME"));
                        NOME += "/" + reader.GetInt32(reader.GetOrdinal("ID_USUARIO"));
                    }
                }
            } else if(tipo == "P")
            {
                queryString = "SELECT ID_COLABORADOR, NOME FROM TB_COLABORADOR where EMAIL = '" + email + "'";

                var comando = new SqlCommand(queryString, connection);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        NOME = reader.GetString(reader.GetOrdinal("NOME"));
                        NOME += "/" + reader.GetInt32(reader.GetOrdinal("ID_COLABORADOR"));
                    }
                }
            }
             
            connection.Close();
            return NOME;

        }

        public static String consultaId(String id)
        {
            String ID_COLABORADOR="";
            Colaborador u = new Colaborador();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

                String query = "SELECT ID_COLABORADOR FROM TB_COLABORADOR where CPF = '" + id + "'";

                var comando = new SqlCommand(query, connection);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                    ID_COLABORADOR = reader.GetInt32(reader.GetOrdinal("ID_COLABORADOR")).ToString();
                        
                    }
                }
            connection.Close();
            return ID_COLABORADOR;
        }

        public static String consultaIdUsuario(String id)
        {
            String ID_COLABORADOR = "";
            Colaborador u = new Colaborador();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String query = "SELECT ID_USUARIO FROM TB_USUARIO where CPF = '" + id + "'";

            var comando = new SqlCommand(query, connection);

            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    ID_COLABORADOR = reader.GetInt32(reader.GetOrdinal("ID_USUARIO")).ToString();

                }
            }
            connection.Close();
            return ID_COLABORADOR;
        }

        public static void insereRegistroUsuario(String query1, String query2, String usuario, String tipoUsuario)
        {
            SqlConnection connections = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connections.Open();
            String querys = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('"+query1+"', 'INSERT','" + usuario + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comandos = new SqlCommand(querys, connections);
            comandos.ExecuteReader();
            comandos.Dispose();
            connections.Close();

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();
            String query = "insert into TB_REGISTRO(ACAO,TIPO_ATUALIZACAO,USUARIO,TIPO_USUARIO,DATA) Values ('" + query2 + "', 'INSERT','" + usuario + "','" + tipoUsuario + "','" + DateTime.Now + "'  )";

            var comando = new SqlCommand(query, connection);
            comando.ExecuteReader();
            comando.Dispose();
            connection.Close();
        }
    }
}
