using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSoft_v2.Recursos
{
    class Entidad
    {
        #region Campo
        static int _ultimoID;
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
        #region Enum
        enum etipoEmpleado
        {
            facturador,
            supervisor,
            gerente,
            administrador
        };
        #endregion
        #region Campo
        string _idEmpleado;
        Decimal _sueldo;
        string _clave;
        #endregion
        #region Propiedades
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
        public etipoEmpleado TipoEmpleado { get; set; }
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
        #region Enum
        enum etipoCliente
        {
            registrado,
            noregistrado
        }
        #endregion
        #region Campo
        //        ring _numeroCliente;

        #endregion
        #region Propiedades
        public string NumeroCliente { get; set; }
        public etipoCliente TipoCliente { get; set; }
        #endregion
        #region Constructor
        //
        public Cliente()
        {

        }
        #endregion
        #region Metodo
        string Factura()
        {
            return "factura";
        }
        #endregion
    }

}
