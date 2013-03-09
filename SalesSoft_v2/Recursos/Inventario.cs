﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        int _cantidad;
        int _estante;
        int _tipo;
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
        public Decimal  Precio      
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
       
        #endregion

        #region Propiedades
      
        #endregion

        #region Constructores
        public Hardware()
        {
            Tipo = 1;
        }
        #endregion

    }
    class Periferico : Producto
    {
        #region Campos
       
        #endregion

        #region Propiedades
      
        #endregion

        #region Constructores
        public Periferico()
        {
            Tipo = 2;
        }
        #endregion

    }
    class Software : Producto
    {
        #region Campos
       
        #endregion

        #region Propiedades
      
        #endregion

        #region Constructores
        public Software()
        {
            Tipo = 3;
        }
        #endregion

    }

}
