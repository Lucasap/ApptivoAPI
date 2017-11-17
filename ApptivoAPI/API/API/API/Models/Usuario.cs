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
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Sexo { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public int Linea { get; set; }

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
        public void InsertarCoordenadas(string Mail, int Linea, float Lat, float Lng)
        {
            ConnectionHelper miHelper = new ConnectionHelper();
            string sUpdate = ("update usuario Set Linea = '"+ Linea +"', Lat = '"+Lat+"', Lng='"+Lng+"' where Mail = '"+Mail+"'");
            miHelper.EjecutarIUD(sUpdate);
        }
        public void ActualizarSeBajo(string Mail)
        {
            int Linea = 999;
            ConnectionHelper miHelper = new ConnectionHelper();
            string sUpdate = ("update usuario Set Linea = '" + Linea + "' where Mail = '" + Mail + "'");
            miHelper.EjecutarIUD(sUpdate);
        }

        public void Insert(Usuario Usr)
        {
            ConnectionHelper miHelper = new ConnectionHelper();
            string sInsert = "Insert into usuario (Nombre, Apellido, Sexo, Mail, Contrasena) values ('" + Usr.Nombre + "','" + Usr.Apellido + "','" + Usr.Sexo + "','" + Usr.Email + "','" + Usr.Contraseña + "')";
            miHelper.EjecutarIUD(sInsert);
        }
        public List<LatLng> ObtenerPorLinea(int Linea)
        {
            ConnectionHelper miHelper = new ConnectionHelper();
            string select = "SELECT * FROM usuario WHERE Linea = '" + Linea + "'";
            DataTable dt = miHelper.EjecutarSelect(select);
            List<LatLng> lista = new List<LatLng>();
            LatLng p;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    p = ObtenerLatLngPorRow(row);
                    lista.Add(p);
                }
                p = ObtenerLatLngPorRow(dt.Rows[0]);
            }
            return lista;
        }
        public Usuario ObtenerPorMail(string Email, string Password)
        {
            ConnectionHelper miHelper = new ConnectionHelper();
            string select = "SELECT * FROM usuario WHERE Mail = '" + Email + "' AND Contrasena = '" + Password + "'";
            //string select = "select * from usuario where Mail=" + Email + " and Contrasena =" + Password;
            DataTable dt = miHelper.EjecutarSelect(select);
            //List<Usuario> lista = new List<Usuario>();
            Usuario usr = new Usuario();
            if (dt.Rows.Count > 0)
            {
                usr = ObtenerPorRow(dt.Rows[0]);
                return usr;
            }
            else
            {
                return null;
            }
        }
        private static LatLng ObtenerLatLngPorRow(DataRow row)
        {
            LatLng p = new LatLng();
            p.Lat = row.Field<float>("Lat");
            p.Lng = row.Field<float>("Lng");

            return p;
        }
        private static Usuario ObtenerPorRow(DataRow row)
        {
            Usuario p = new Usuario();
            p.Id = row.Field<int>("idUsuario");
            p.Nombre = row.Field<string>("Nombre");
            p.Apellido = row.Field<string>("Apellido");
            p.Sexo = row.Field<string>("Sexo");
            p.Email = row.Field<string>("Mail");
            p.Contraseña = row.Field<string>("Contrasena");
            p.Lat = 0;
            p.Lng = 0;
            p.Linea = 0;
         
            return p;
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
