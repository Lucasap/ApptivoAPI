using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models;
using System.Data;
using System.Data.OleDb;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Apptivo.data;

namespace API.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Sexo { get; set; }

        //private string NombreArchivo = "Apptivo.mdb";
        //private OleDbConnection nCon;
        private bool connection_open;
        private MySqlConnection connection;
        /*private void Conectar()
        {
            string proovedor = @"Provider=Microsoft.Jet.OLEDB.4.0;  Data Source = |DataDirectory|" + NombreArchivo;
            nCon = new OleDbConnection();
            nCon.ConnectionString = proovedor;
            nCon.Open();
        }*/
        public void AgregarUsuario(string MailUsuario)
        {
            ConnectionHelper miHelper = new ConnectionHelper();
            Get_Connection();
            Email = MailUsuario;
            Sexo = "Masculino";

            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = miHelper.connection;                
                cmd.CommandText = string.Format("INSERT INTO `usuario` (`Nombre`, `Apellido`, `Sexo`, `Mail`, `Contrasena`) VALUES ('" + Nombre + "', '" + Apellido + "', '" + Sexo + "', '" + MailUsuario.Replace("@", "O") + "', '" + Contraseña + "')");
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
            }
        }
        public static List<Usuario> ObtenerPorMail(string Email, string Password)
        {
            ConnectionHelper miHelper = new ConnectionHelper();
            string select = "SELECT * FROM `usuario` WHERE `Mail` = '" + Email + "' AND `Contrasena` = '" + Password + "'";
            DataTable dt = DBHelper.EjecutarSelect(select);
            List<Usuario> lista = new List<Usuario>();
            Usuario usr;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    p = ObtenerPorRow(row);
                    lista.Add(p);
                }
                p = ObtenerPorRow(dt.Rows[0]);
            }
            return lista;
        }
        public Usuario ValidateLogin(String Email, String Password)
        {
            bool SioNo = false;
            //Get_Connection();
            Usuario nusuario = new Usuario();
            MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = "SELECT * FROM `usuario` WHERE `Mail` = '" + Email + "' AND `Contrasena` = '" + Password + "'";
                MySqlDataReader Reader = cmd.ExecuteReader();
                Reader.Read();
                
            if (Reader.Read()==true)
                {
                nusuario.Nombre = Reader.GetString("Nombre");
                nusuario.Apellido = Reader.GetString("Apellido");
                nusuario.Email = Reader.GetString("Mail");
                nusuario.Contraseña = Reader.GetString("Contraseña");
                nusuario.Sexo = Reader.GetString("Sexo");
            }
            else
            {
                nusuario = null; 
            }

            return nusuario;

        }

        private void Get_Connection()

        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Database = "localdb";
            conn_string.Port = 3306;
            conn_string.Server = "localhost";
            conn_string.UserID = "root";
            conn_string.Password = "root";
            
            

            connection_open = false;

            //connection = new MySqlConnection();
            string strConnectionString = conn_string.ToString();
            connection = new MySqlConnection(strConnectionString);
            if (Open_Local_Connection())
            {
                connection_open = true;
            }

        }
        private bool Open_Local_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}