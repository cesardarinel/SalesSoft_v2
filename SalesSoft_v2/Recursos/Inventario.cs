using System;
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
        string _nombre;
        string _numeroSerial;
        Decimal _precio;
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

        }
        #endregion

    }

}
