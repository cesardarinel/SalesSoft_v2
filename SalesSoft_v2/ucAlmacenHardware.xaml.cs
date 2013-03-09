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
using MySql.Data.MySqlClient;
using SalesSoft_v2.Recursos;

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for ucAlmacenHardware.xaml
    /// </summary>
    public partial class ucAlmacenHardware : UserControl, iTabbedMDI
    {
        public ucAlmacenHardware()
        {
            InitializeComponent();
            Iniciar(false);
            Inicialp(false);
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
                return "AlmacenHardware";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Almacen de Hardware"; }
        }
        #endregion
        void Iniciar(bool a)
        {
            tbNombre.IsEnabled = a;
            tbRnc.IsEnabled = a;
            tbTelefono.IsEnabled = a;
            cbActivo.IsEnabled = a;
 
        }
        void Inicialp(bool a)
        {
            tbCantidad.IsEnabled = a;
            tbEstante.IsEnabled = a;
            tbExistenciaMin.IsEnabled = a;
            tbFAbricante.IsEnabled = a;
            tbModelo.IsEnabled = a;
            tbNombreP.IsEnabled = a;
            tbNSerial.IsEnabled = a;
            tbNSerial.IsEnabled = a;
            tbPrecio.IsEnabled = a;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }

        private void NuevoFabricante(object sender, RoutedEventArgs e)
        {
            Iniciar(true);
        }

        private void Agregar(object sender, RoutedEventArgs e)
        {
            Suplidor Fabricante = new Suplidor();
            if (tbNombre.Text == string.Empty || tbNombre.Text.Length > 10)
            {
                MessageBox.Show("El campo nombre esta vacío, porfavor llene con un Nombre ó tienes mas de 10 caracteres");
                return;
            }
            Fabricante.Nombre = tbNombre.Text;
            if (tbTelefono.Text == string.Empty || tbTelefono.Text.Length > 10)
            {
                MessageBox.Show("El campo TElefono esta vacío, porfavor llene con un Telefono ó tienes mas de 10 caracteres");
                return;
            }
            Fabricante.Telefono = tbTelefono.Text;
            if (tbRnc.Text == string.Empty || tbRnc.Text.Length > 10)
            {
                MessageBox.Show("El campo RNC esta vacío, porfavor llene con un RNC ó tienes mas de 10 caracteres");
                return;
            }
            Fabricante.RNC = tbRnc.Text;

            if (cbActivo.IsChecked == true)
            {
                Fabricante.Estado = true;
            }
            else
            {
                Fabricante.Estado = false;
            }
            Conexion.AbrirConexion();

            MySqlCommand agregarFabricante = new MySqlCommand
                ("INSERT INTO fabricantes (nombrefabricante,telefono ,rnc ,estado ) VALUES('"
                + Fabricante.Nombre + "','" + Fabricante.Telefono + "','" + Fabricante.RNC 
                + "','" + Convert.ToInt32(Fabricante.Estado) + "')", Conexion.varConexion);

            try
            {
                agregarFabricante.ExecuteNonQuery();
                MessageBox.Show("Información: Fabricante Agregado.");
                tbNombre.Text = null;
                tbTelefono.Text = null;
                tbRnc.Text = null;
                cbActivo.IsChecked = false;
                Iniciar(false);
            }


            finally
            {
                Conexion.CerrarConexion();
            }
            
            
        }

        private void RegistrarProducto(object sender, RoutedEventArgs e)
        {
            Inicialp(true);
        }

        private void AgregarProducto(object sender, RoutedEventArgs e)
        {
            Producto nuevo = new Producto();
            if (tbFAbricamte.Text == string.Empty || tbFAbricante.Text.Length > 20)
            {
                MessageBox.Show("El campo Fabricante esta vacío, porfavor llene con un Fabricante ó tienes mas de 20 caracteres");
                return;
            }
            nuevo. = tbNombre.Text;
            if (tbTelefono.Text == string.Empty || tbTelefono.Text.Length > 10)
            {
                MessageBox.Show("El campo TElefono esta vacío, porfavor llene con un Telefono ó tienes mas de 10 caracteres");
                return;
            }
            nuevo.Telefono = tbTelefono.Text;
            if (tbRnc.Text == string.Empty || tbRnc.Text.Length > 10)
            {
                MessageBox.Show("El campo RNC esta vacío, porfavor llene con un RNC ó tienes mas de 10 caracteres");
                return;
            }
            nuevo.RNC = tbRnc.Text;

            if (cbActivo.IsChecked == true)
            {
                nuevo.Estado = true;
            }
            else
            {
                nuevo.Estado = false;
            }
            Conexion.AbrirConexion();

            MySqlCommand agregarnuevo = new MySqlCommand
                ("INSERT INTO nuevos (nombrenuevo,telefono ,rnc ,estado ) VALUES('"
                + nuevo.Nombre + "','" + nuevo.Telefono + "','" + nuevo.RNC
                + "','" + Convert.ToInt32(nuevo.Estado) + "')", Conexion.varConexion);

            try
            {
                agregarnuevo.ExecuteNonQuery();
                MessageBox.Show("Información: nuevo Agregado.");
                tbNombre.Text = null;
                tbTelefono.Text = null;
                tbRnc.Text = null;
                cbActivo.IsChecked = false;
                Iniciar(false);
            }


            finally
            {
                Conexion.CerrarConexion();
            }
        }

       
    }
}
