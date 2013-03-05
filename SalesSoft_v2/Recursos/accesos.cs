using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSoft_v2.Recursos
{
    class acceso
    {
        #region Propiedades
        public enum eTipo{
            Administrador,
            Cajero
        };
        public eTipo TipoEmpleado { get; set; }
        #endregion
        #region Constructores
        public acceso()
        {

        }
        #endregion

        #region Metodos
        // se supone pregunto el tipo para saver que mostrar.... pero no tiene logica la clase solo preguntando en la base de datso al atrar y listo :/
        #endregion
    }
}
