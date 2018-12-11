using personal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace personal.Repositório
{
    public class Filtro
    {
        public static List<Colaborador> precoListaPersonal(int modalidade, string filtro)
        {
            String sSql = "";
            List<Colaborador> colaboradores = new List<Colaborador>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            if (filtro == "todos")
            {
                sSql = "select c.ID_COLABORADOR,c.NOME,c.FORMACAO,c.CREF,c.UF_CREF, c.IMAGE_NAME from TB_MODALIDADES as M join TB_COLABORADOR as C on C.ID_COLABORADOR = m.ID_COLABORADOR where ID_ESPORTE =" + modalidade;
            }
            else
            {
                sSql = "select c.ID_COLABORADOR,c.NOME,c.FORMACAO,c.CREF,c.UF_CREF, c.IMAGE_NAME from TB_MODALIDADES as M join TB_COLABORADOR as C on C.ID_COLABORADOR = m.ID_COLABORADOR where ID_ESPORTE =" + modalidade + " ORDER BY M.[PRECO/HORA]";
            }


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
                        FORMACAO = reader.GetString(reader.GetOrdinal("FORMACAO")),
                        IMAGE_NAME = reader.GetString(reader.GetOrdinal("IMAGE_NAME")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return colaboradores;
        }
        public static List<Colaborador> formacaoListaPersonal(int modalidade, int id)
        {
            String sSql = "";
            List<Colaborador> colaboradores = new List<Colaborador>();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            if(id == 0){
                sSql = "select c.ID_COLABORADOR,c.NOME,c.FORMACAO,c.CREF,c.UF_CREF, c.IMAGE_NAME, c.SOBRE from TB_MODALIDADES as M join TB_COLABORADOR as C on C.ID_COLABORADOR = m.ID_COLABORADOR where ID_ESPORTE =" + modalidade;
            }
            else {
                sSql = "select c.ID_COLABORADOR,c.NOME,c.FORMACAO,c.CREF,c.UF_CREF, c.IMAGE_NAME, c.SOBRE from TB_MODALIDADES as M join TB_COLABORADOR as C on C.ID_COLABORADOR = m.ID_COLABORADOR join TB_FORMACAO as F on F.DESC_FORMACAO = c.FORMACAO where ID_ESPORTE = " + modalidade +" and f.ID = " + id;
            }
            
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
                        FORMACAO = reader.GetString(reader.GetOrdinal("FORMACAO")),
                        IMAGE_NAME = reader.GetString(reader.GetOrdinal("IMAGE_NAME")),
                        SOBRE = reader.GetString(reader.GetOrdinal("SOBRE")),
                    });
                }
            }
            comando.Dispose();
            connection.Close();

            return colaboradores;
        }
    }
}