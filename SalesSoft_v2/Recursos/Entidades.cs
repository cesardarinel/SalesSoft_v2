using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SalesSoft_v2.Recursos
{
    class Entidad
    {
        #region Campo
        int _id;
        string _nombre;
        string _telefono;
        string _estado;
        #endregion
        #region Propiedades
       
        public int ID
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
        public string Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                _telefono = value;
            }
        }
        public string Estado
        {
            get
            {
                return _estado;
            }
            set
            {
                _estado = value;
            }
        }
        #endregion
        #region Constructor
        public Entidad()
        {

        }
        #endregion
        #region Metodo
        void Inactivar()
        {
            //programar
        }
        #endregion
    }

    class Persona : Entidad
    {
        #region Campo
        string _cedula;
        string _nombreCompleto;
        #endregion
        #region Propiedades
        public string Cedula
        {
            get
            {
                return _cedula;
            }
            set
            {
                _cedula = value;
            }
        }
        public string NombreCompleto
        {
            get
            {
                return _nombreCompleto;
            }
            set
            {
                _nombreCompleto = value;
            }
        }

        #endregion
        #region Constructor
        public Persona()
        {

        }
        #endregion
        #region Metodo
        void Inactivar()
        {
            //programar
        }
        #endregion
    }

    class Suplidor : Entidad
    {
        #region Campo
        string _rnc;
        #endregion
        #region Propiedades
        public string RNC
        {
            get
            {
                return _rnc;
            }
            set
            {
                _rnc = value;
            }
        }
        #endregion
        #region Constructor
        public Suplidor()
        {

        }
        #endregion
        #region Metodo
        void Inactivar()
        {
            //programar
        }
        #endregion
    }

    class Empleado : Persona
    {
        
        #region Campo
        string _idEmpleado;
        Decimal _sueldo;
        string _clave;
        int _tusuario;
        bool _activo;
        #endregion
        #region Propiedades
        public bool Activo
        {
            get
            {
                return _activo;
            }
            set
            {
                _activo = value;
            }
        }
        public int TUsuario
        {
            get
            {
                return _tusuario;
            }
            set
            {
                _tusuario = value;
            }
        }
        public string IdEmpleado
        {
            get
            {
                return _idEmpleado;
            }
            set
            {
                _idEmpleado = value;
            }
        }
        //public etipoEmpleado TipoEmpleado { get; set; }
        public Decimal Sueldo
        {
            get
            {
                return _sueldo;
            }
            set
            {
                _sueldo = value;
            }
        }
        public string Clave
        {
            get
            {
                return _clave;
            }
            set
            {
                _clave = value;
            }
        }
        #endregion
        #region Constructor
        public Empleado()
        {

        }
        public Empleado(int id)
        {
            Conexion.AbrirConexion();

            MySqlCommand tabla = new MySqlCommand("SELECT nombrecompleto,tipousuario  FROM  empleados  WHERE id_empleado='" + id + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {

                NombreCompleto = data.GetString(0);
                TUsuario = data.GetInt32(1);


            }
            Conexion.CerrarConexion();
        }
        #endregion
        #region Metodo
        string MostrarInfo()
        {
            //programar
            return "algo";
        }
        string VentaMensuales()
        {
            //programar
            return "noc que tiene que ver las venta con el empleado";
        }
       
        #endregion
    }
    //clase administrativo deveria heredar de empleado tiene todo lo que necesita ...
    //class Administrativo:Persona
    //{
    //    #region Campo
    //    string _idEmpledo;
    //    Decimal
    //    #endregion
    //    #region Propiedades

    //    #endregion
    //    #region Constructor

    //    #endregion
    //    #region Metodo

    //    #endregion
    //}

    class Cliente : Persona
    {
        
        #region Campo
        int _id;
        bool _tipocliente;
        string _telefono;
        string _direccion;
        #endregion
        #region Propiedades
        public int ID
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
        public bool TipoCliente
        {
            get
            {
                return _tipocliente;
            }
            set
            {
                _tipocliente = value;
            }
        }
        public string Telefono
        {
            get
            {
                return _telefono;
            }
            set
            {
                _telefono = value;
            }
        }
        public string Direccion
        {
            get
            {
                return _direccion;
            }
            set
            {
                _direccion = value;
            }
        }
        #endregion 
         
        #region Constructor
        //
        public Cliente()
        {

        }
        public Cliente(int id)
        {

            Conexion.AbrirConexion();

            MySqlCommand tabla = new MySqlCommand("SELECT nombrecompleto,tipocliente,direccion,telefono  FROM  clientes  WHERE id_cliente='" + id + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {

                NombreCompleto = data.GetString(0);
                if (Convert.ToInt32(data.GetString(1)) == 1)
                {
                    TipoCliente = true;
                }
                else
                {
                    TipoCliente = false;
                }
                Direccion = data.GetString(2);
                Telefono = data.GetString(3);


            }
            Conexion.CerrarConexion();

        }
        #endregion
        #region Metodo
        string Factura()
        {
            return "factura";
        }
        void ClienteID(int id)
        {
            Conexion.AbrirConexion();
            MySqlCommand tabla = new MySqlCommand("SELECT nombrecompleto,tipocliente,direccion,telefono  FROM  clientes WHERE id_cliente='" + id + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {

                NombreCompleto = data.GetString(0);
                TipoCliente =Convert.ToBoolean( data.GetString(1));
                Direccion = data.GetString(2);
                Telefono = data.GetString(3);

            }
            Conexion.CerrarConexion();
        }
        #endregion
    }

}
