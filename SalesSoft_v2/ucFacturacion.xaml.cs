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

//Clases y Conexión
using MySql.Data.MySqlClient;
using SalesSoft_v2.Recursos;
using System.Data;

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for ucFacturacion.xaml
    /// </summary>
    public partial class ucFacturacion : UserControl, iTabbedMDI
    {
        public ucFacturacion()
        {
            InitializeComponent();
        }
        #region AgregarCliente
        private void Procesar(object sender, RoutedEventArgs e)
        {
            if (tbNMienbro.Text == string.Empty )
            {
                MessageBox.Show("El campo N Miembro  esta vacío, porfavor llene el Campo");
                return;
            }
            Cliente ClienteActivo = new Cliente(Convert.ToInt32(tbNMienbro.Text));
            tbNombre.Text= ClienteActivo.NombreCompleto;
            tbDireccion.Text = ClienteActivo.Direccion;
            tbTelefono.Text = ClienteActivo.Telefono;
            cbActivo.IsChecked = ClienteActivo.TipoCliente;
        }

        private void AgregarCliente(object sender, RoutedEventArgs e)
        {
            if (tbNMienbro.Text != string.Empty)
            {
                MessageBox.Show("El campo N Miembro  esta Lleno, Puede Procesar el Cliente");
                return;
            }
            Cliente Cliente_nuevo = new Cliente();
            if (tbNombre.Text == string.Empty || tbNombre.Text.Length > 60)
            {
                MessageBox.Show("El campo nombre esta vacío, porfavor llene con un Nombre ó tienes mas de 10 caracteres");
                return;
            }
            Cliente_nuevo.NombreCompleto= tbNombre.Text;
            if (tbDireccion.Text == string.Empty || tbDireccion.Text.Length > 50)
            {
                MessageBox.Show("El campo Direccion esta vacío, porfavor llene con una Direccion ó tienes mas de 50 caracteres");
                return;
            }
            Cliente_nuevo.Direccion = tbDireccion.Text;
            if (tbTelefono.Text == string.Empty || tbTelefono.Text.Length > 10)
            {
                MessageBox.Show("El campo Telefono esta vacío, porfavor llene con un Telefono ó tienes mas de 10 caracteres");
                return;
            }
            Cliente_nuevo.Telefono = tbTelefono.Text;
            if(cbActivo.IsChecked==true)
                Cliente_nuevo.TipoCliente = true;
            else
                Cliente_nuevo.TipoCliente = false;

            Conexion.AbrirConexion();

            MySqlCommand agregarCliente_nuevo = new MySqlCommand
                ("INSERT INTO clientes (nombrecompleto,tipocliente, direccion,telefono) VALUES('"
                + Cliente_nuevo.NombreCompleto + "','" + Convert.ToInt32(Cliente_nuevo.TipoCliente) + "','" + Cliente_nuevo.Direccion
                + "','" + Cliente_nuevo.Telefono + "')", Conexion.varConexion);

            try
            {
                agregarCliente_nuevo.ExecuteNonQuery();
                MessageBox.Show("Información: Cliente Agregado.");
                cbActivo.IsChecked = false;
                tbDireccion.Text = string.Empty;
                tbNMienbro.Text = string.Empty;
                tbNombre.Text = string.Empty;
                tbTelefono.Text = string.Empty;

            }


            finally
            {
                Conexion.CerrarConexion();
            }

        }
        
        #endregion

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
                return "Facturación";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Facturación"; }
        }
        #endregion

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }

        
    }
}
