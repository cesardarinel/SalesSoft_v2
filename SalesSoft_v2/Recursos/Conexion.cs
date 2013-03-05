using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SalesSoft_v2.Recursos
{
   
    abstract class Conexion
    {
        /// <summary>
        /// VAriable para coenctar con la base de datos...
        /// </summary>
        public static MySqlConnection varConexion = new MySqlConnection("Server=db4free.net;Database=portafolionumero;Uid=portafolio123;Pwd=123456;");

        public static void AbrirConexion()
        {
            varConexion.Open();
        }
        public static void CerrarConexion()
        {
            varConexion.Close();
        }
    }

}
