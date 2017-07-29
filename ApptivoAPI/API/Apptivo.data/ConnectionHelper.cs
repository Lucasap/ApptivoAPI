
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Azure;

namespace Apptivo.data
{
    public class ConnectionHelper
    {
        public bool connection_open;
        public MySqlConnection connection;
        public static string ConnectionString
        {
            get
            {
                return CloudConfigurationManager.GetSetting("MySqlConnectionString");
            }
        }
        /*   public void Get_Connection()
           {
               MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
               conn_string.Server = "localhost";
               conn_string.UserID = "root";
               conn_string.Password = "apptivo";
               conn_string.Database = "apptivo"; 


        connection_open = false;

            //connection = new MySqlConnection();
            string strConnectionString = conn_string.ToString();
            connection = new MySqlConnection(strConnectionString);
            //connection = DB_Connect.Make_Connnection(ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString);
            //connection.ConnectionString = "Server=localhost;Database=apptivo;Uid=root;Pwd=apptivo;"; // ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            if (Open_Local_Connection())
            {
                connection_open = true;
            }
            else
            {
                //					MessageBox::Show("No database connection connection made...\n Exiting now", "Database Connection Error");
                //					 Application::Exit();
            }

        }*/
        public bool Open_Local_Connection()
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
