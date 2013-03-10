using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SalesSoft_v2.Recursos;
using MySql.Data.MySqlClient;
using System.Data;

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for ucCuentasPorCobrar.xaml
    /// </summary>
    public partial class ucCuentasPorCobrar : UserControl, iTabbedMDI
    {
        public ucCuentasPorCobrar()
        {
            InitializeComponent();
        }

        #region iTabbedMDI Members

        /// <summary>
        /// This event will be fired when user will click close button
        /// </summary>
        public event delClosed CloseInitiated;

        /// <summary>
        /// This is unique name of the tab
        /// </summary>
        public string UniqueTabName
        {
            get
            {
                return "CuentasPorCobrar";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Cuentas Por Cobrar"; }
        }
        #endregion

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }

        private void Limpiar(object sender, RoutedEventArgs e)
        {
            tbNombred.Text = null;
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {
            int usuario;
            
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            ///agregamos la base de datos a la pantalla 
            ///
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT id_ciente FROM clientes WHERE   nombrecompleto= '"+ tbNombred.Text+"' ", Conexion.varConexion);
            MySqlDataReader data = da.ExecuteReader();

            while (data.Read())
            {
                usuario = data.GetInt32(0);

            }
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            ///agregamos la base de datos a la pantalla 
            ///
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT* FROM facturas WHERE ", Conexion.varConexion);

            DataSet dt = new DataSet();
            da.Fill(dt);
            dgFacturasPendientes.DataSource = dt.Tables[0];
            Conexion.CerrarConexion();
        
        }
    }
}
