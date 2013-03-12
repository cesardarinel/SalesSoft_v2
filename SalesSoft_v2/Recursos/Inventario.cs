using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace SalesSoft_v2.Recursos
{
    class Producto
    {
        #region Campos
        int _id;
        string _fabricante;
        string _modelo;
        string _nombre;
        string _numeroSerial;
        Decimal _precio;
        int _tipo;
        int _existencia;
        #endregion

        #region Propiedades
        public int ID_Producto
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string Fabricante
        {
            get
            {
                return _fabricante;
            }
            set
            {
                _fabricante = value;
            }
        }
        public string Modelo
        {
            get
            {
                return _modelo;
            }
            set
            {
                _modelo = value;
            }
        }
        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
            }
        }
        public string NumeroSerial
        {
            get
            {
                return _numeroSerial;
            }
            set
            {
                _numeroSerial = value;
            }
        }
        public Decimal Precio
        {
            get
            {
                return _precio;
            }
            set
            {
                _precio = value;
            }
        }
        public int Tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                _tipo = value;
            }
        }
        public int ExistenciaMinima
        {
            get
            {
                return _existencia;
            }
            set
            {
                _existencia = value;
            }
        }
        #endregion

        #region Constructores
        public Producto()
        {

        }
        
        #endregion

        #region Metodos

        #endregion
    }
    class Hardware : Producto
    {
        #region Campos
        int _cantidad;
        int _estante;
        #endregion

        #region Propiedades
        public int Estante {
            get 
            {
                return _estante;
            }
            set
            {
                _estante = value;
            }
        }
        public int Cantidad
        {
            get
            {
                return _cantidad;
            }
            set
            {
                _cantidad = value;
            }
        }
        #endregion

        #region Constructores
        public Hardware()
            : base()
        {
            Tipo = 1;
        }
        public Hardware(string nombre)
        {
            Conexion.AbrirConexion();

            MySqlCommand tabla = new MySqlCommand("SELECT *FROM  productos  WHERE nombreproducto='" + nombre + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {

                ID_Producto = data.GetInt32(0);
                Fabricante = data.GetString(1);
                Modelo = data.GetString(2);
                Nombre = nombre;
                NumeroSerial = data.GetString(4);
                Precio = data.GetInt32(5);
                Cantidad = data.GetInt32(6);
                Estante = data.GetInt32(7);
                Tipo = data.GetInt32(8);
                ExistenciaMinima = data.GetInt32(9);
            }
            Conexion.CerrarConexion();

        }
        #endregion

    }
    class Periferico : Producto
    {
        #region Campos
        int _cantidad;
        int _estante;
        #endregion

        #region Propiedades
        public int Estante
        {
            get
            {
                return _estante;
            }
            set
            {
                _estante = value;
            }
        }
        public int Cantidad
        {
            get
            {
                return _cantidad;
            }
            set
            {
                _cantidad = value;
            }
        }
        #endregion

        #region Constructores
        public Periferico()
            : base()
        {
            Tipo = 2;
        }
        public Periferico(string nombre)
        {
            Conexion.AbrirConexion();

            MySqlCommand tabla = new MySqlCommand("SELECT *FROM  productos  WHERE nombreproducto='" + nombre + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {

                ID_Producto = data.GetInt32(0);
                Fabricante = data.GetString(1);
                Modelo = data.GetString(2);
                Nombre = nombre;
                NumeroSerial = data.GetString(4);
                Precio = data.GetInt32(5);
                Cantidad = data.GetInt32(6);
                Estante = data.GetInt32(7);
                Tipo = data.GetInt32(8);
                ExistenciaMinima = data.GetInt32(9);
            }
            Conexion.CerrarConexion();

        }
        #endregion

    }
    class Software : Producto
    {
        #region Campos
        int _cantidad;
        int _estante;
        #endregion

        #region Propiedades
        public int Estante
        {
            get
            {
                return _estante;
            }
            set
            {
                _estante = value;
            }
        }
        public int Cantidad
        {
            get
            {
                return _cantidad;
            }
            set
            {
                _cantidad = value;
            }
        }
        #endregion

        #region Constructores
        public Software()
            : base()
        {
            Tipo = 3;

        }
        public Software(string nombre)
        {
            Conexion.AbrirConexion();

            MySqlCommand tabla = new MySqlCommand("SELECT *FROM  productos  WHERE nombreproducto='" + nombre + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {

                ID_Producto = data.GetInt32(0);
                Fabricante = data.GetString(1);
                Modelo = data.GetString(2);
                Nombre = nombre;
                NumeroSerial = data.GetString(4);
                Precio = data.GetInt32(5);
                Cantidad = data.GetInt32(6);
                Estante = data.GetInt32(7);
                Tipo = data.GetInt32(8);
                ExistenciaMinima = data.GetInt32(9);
            }
            Conexion.CerrarConexion();

        }
        #endregion

    }

}
