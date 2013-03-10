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
            Inicializar(false);
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

        void Inicializar(bool a)
        {
            tbMiembroN.IsEnabled = a;
            tbNombre.IsEnabled = a;
            tbTelefono.IsEnabled = a;
            tbDireccion.IsEnabled = a;
            label1.Content = "";
            lbPrecio.Content = "";
           
        }
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
            tbID.Text = null;
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {   Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            if (tbNombred.Text == string.Empty || tbID.Text != string.Empty)
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT nombrecompleto,id_cliente FROM clientes WHERE  id_cliente= '" + tbID.Text + "' ", Conexion.varConexion);
                DataSet dt = new DataSet();
                da.Fill(dt);
                dgFacturasPendientes.DataSource = dt.Tables[0];
                Conexion.CerrarConexion();
                return;

            }
            else { 
            /////agregamos la base de datos a la pantalla 
            /////
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT nombrecompleto,id_cliente FROM clientes WHERE   nombrecompleto= '" + tbNombred.Text + "' ", Conexion.varConexion);
             DataSet dt = new DataSet();
            da.Fill(dt);
            dgFacturasPendientes.DataSource = dt.Tables[0];
            Conexion.CerrarConexion();
            return;
            
            }
            MessageBox.Show("El campo nombre Y ID Cliente vacío, porfavor llenar uno de ellos");
            return;

        }

        private void Click_cliente(object sender, EventArgs e)
        {
            Cliente verFactiura = new Cliente((int )(dgFacturasPendientes.CurrentRow.Cells[1].Value));

            tbMiembroN.Text = Convert.ToString(verFactiura.ID);
            tbNombre.Text = verFactiura.NombreCompleto;
            tbTelefono.Text =verFactiura.Telefono ;
            tbDireccion.Text =verFactiura.Direccion;
            label1.Content = "";
            lbPrecio.Content = "";
        }
    }
}
