using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrueba2
{
    class Conexion
    {
        public static SqlConnection conexion()
        {
            string servidor = "DESKTOP-V93PM4L";
            string bd = "Promotor";
            string usuario = "sa";
            string password = "Shaco98";
            //string puerto = "3306";

            String cadenaConexion = "server=" + servidor + " ; database=" + bd + " ; integrated security = true; user id = " + usuario + "; password = " + password + "";
            //String cadenaConexion = "Database=" + bd + ";Data Source=" + servidor + "; Port=" + puerto + ";User Id =" + usuario + "; Password=" + password;
            try
            {
                SqlConnection conexionBD = new SqlConnection(cadenaConexion);
                //MessageBox.Show("Conexion abierta");
                return conexionBD;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
                Console.WriteLine("Error" + ex.Message);
                return null;
            }
        }
    }
}
