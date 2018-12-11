using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace personal.Models
{
    public class Security
    {
        public String Criptografa(String password)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageButyes = UE.GetBytes(password);
            SHA1Managed SHhash = new SHA1Managed();
            String  cripPassword = "";
            HashValue = SHhash.ComputeHash(MessageButyes);

            foreach (byte b in HashValue)
            {
                cripPassword += String.Format("{0:x2}", b);
            }
            return cripPassword;
         }
       
        public bool validaCPF(String cpf)
        {
            string valor = cpf.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;

            }
            else
                if (numeros[10] != 11 - resultado)
                return false;
            return true;

        }

        public bool validaEmail(String email)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select email from tb_usuario where email ='"+ email + "'";
             var comando = new SqlCommand(sSql, connection);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }
        
            public bool validaEmailColaborador(String email)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
            connection.Open();

            String sSql = "select email from tb_colaborador where email ='" + email + "'";
            var comando = new SqlCommand(sSql, connection);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            return false;
        }
    }
}