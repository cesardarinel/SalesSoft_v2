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
using System.Data;
using System.Diagnostics;

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for ucListadoProductos.xaml
    /// </summary>
    public partial class ucListadoProductos : UserControl, iTabbedMDI
    {
        public ucListadoProductos()
        {
            InitializeComponent();
            iniciar(false);
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
                return "ListadoProductos";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Listado de Productos"; }
        }
        #endregion
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }

        private void cbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            ///agregamos la base de datos a la pantalla 
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM productos WHERE tipo='"+cbTipo.SelectedIndex+1+"' ", Conexion.varConexion);

            DataSet dt = new DataSet();
            da.Fill(dt);
            dgProductos.DataSource = dt.Tables[0];
            Conexion.CerrarConexion(); ;
        }
        void iniciar(bool a)
        {
            tbFAbricante.IsEnabled = a;
            tbModelo.IsEnabled = a;
            tbNombreP.IsEnabled = a;
            tbNSerial.IsEnabled = a;
            tbPrecio.IsEnabled = a;
            tbCantidad.IsEnabled = a;
            tbEstante.IsEnabled = a;
        }
        private void dgProductos_Click(object sender, EventArgs e)
        {
            Conexion.AbrirConexion();
            MySqlCommand tabla = new MySqlCommand("SELECT fabricante ,modelo ,nombreproducto,numeroserie,precio,cantidad,estante   FROM  productos WHERE id_producto='" + (dgProductos.CurrentRow.Cells[0].Value) + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {

                tbFAbricante.Text = data.GetString(0);
                tbModelo.Text = data.GetString(1);
                tbNombreP.Text = data.GetString(2);
                tbNSerial.Text = data.GetString(3);
                tbPrecio.Text = data.GetString(4);
                tbCantidad.Text = data.GetString(5);
                tbEstante.Text = data.GetString(6);
                llamo_la_web(tbNombreP.Text);
            }
        
           
            Conexion.CerrarConexion();
        } 
        void llamo_la_web(string nombre)
        {
            wbWIKIPEDIA.Navigate("http://www.google.com/search?btnI&q=" + "wikipedia " +nombre);
        }
    }
}
