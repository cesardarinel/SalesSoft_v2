using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using Microsoft.Windows.Controls;
using MySql.Data.MySqlClient;
using SalesSoft_v2.Recursos;
using System.Data;

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for ucAlmacenHardware.xaml
    /// </summary>
    public partial class ucAlmacenHardware : UserControl, iTabbedMDI
    {
        int vol;
        bool nuevo = false;
        Decimal Maximo;

        StringCollection coleccionNombres = new StringCollection();

        public ucAlmacenHardware()
        {
            InitializeComponent();
            MostrarTablas();
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

        public void MostrarTablas()
        {
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            ///agregamos la base de datos a la pantalla 
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM productos WHERE tipo='1' ", Conexion.varConexion);

            DataSet dt = new DataSet();
            da.Fill(dt);
            dgProductos.DataSource = dt.Tables[0];
            Conexion.CerrarConexion();
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
            tbFAbricante.Text = null;
            tbModelo.Text = null;
            tbNombreP.Text = null;
            tbNSerial.Text = null;
            tbPrecio.Text = null;
            tbCantidad.Text = null;
            tbEstante.Text = null;
            tbExistenciaMin.Text = null;
            nuevo = false;

            Inicialp(true);
        }

        private void AgregarProducto(object sender, RoutedEventArgs e)
        {
            Hardware Nuevo_hadware = new Hardware();

            if (tbFAbricante.Text == string.Empty || tbFAbricante.Text.Length > 20)
            {
                MessageBox.Show("El campo Fabricante esta vacío, porfavor llene con un Fabricante ó tienes mas de 20 caracteres");
                return;
            }
            Nuevo_hadware.Fabricante= tbFAbricante.Text;
            if (tbModelo.Text == string.Empty || tbModelo.Text.Length > 10)
            {
                MessageBox.Show("El campo Modelo esta vacío, porfavor llene con un Modelo ó tienes mas de 10 caracteres");
                return;
            }
            Nuevo_hadware.Modelo = tbModelo.Text;
            if (tbNombreP.Text == string.Empty || tbNombreP.Text.Length > 10)
            {
                MessageBox.Show("El campo Nombre esta vacío, porfavor llene con un Nombre ó tienes mas de 10 caracteres");
                return;
            }
            Nuevo_hadware.Nombre = tbNombreP.Text;
            if (tbNSerial.Text == string.Empty || tbNSerial.Text.Length > 10)
            {
                MessageBox.Show("El campo N Serial esta vacío, porfavor llene con un N Serial ó tienes mas de 10 caracteres");
                return;
            }
            Nuevo_hadware.NumeroSerial = tbNSerial.Text;
            if (tbPrecio.Text == string.Empty || tbPrecio.Text.Length > 10)
            {
                MessageBox.Show("El campo Precio esta vacío, porfavor llene con un Precio ó tienes mas de 10 caracteres");
                return;
            }
            Nuevo_hadware.Precio = Convert.ToDecimal(tbPrecio.Text);
            if (tbEstante.Text == string.Empty || tbEstante.Text.Length > 10)
            {
                MessageBox.Show("El campo Estante esta vacío, porfavor llene con un Estante ó tienes mas de 10 caracteres");
                return;
            }
            Nuevo_hadware.Estante =Convert.ToInt32( tbEstante.Text);
            if (tbCantidad.Text == string.Empty || tbCantidad.Text.Length > 10)
            {
                MessageBox.Show("El campo Cantidad esta vacío, porfavor llene con un Cantidad ó tienes mas de 10 caracteres");
                return;
            }
            Nuevo_hadware.Cantidad =Convert.ToInt32( tbCantidad.Text);
            if (tbExistenciaMin.Text == string.Empty || tbExistenciaMin.Text.Length > 10)
            {
                MessageBox.Show("El campo Existencia Minima esta vacío, porfavor llene con un Existencia Minima ó tienes mas de 10 caracteres");
                return;
            }
            Nuevo_hadware.ExistenciaMinima = Convert.ToInt32(tbCantidad.Text);
            if(nuevo)
            {

                Conexion.CerrarConexion();
                Conexion.AbrirConexion();
                MySqlCommand preguntar = new MySqlCommand("SELECT * FROM productos WHERE id_producto = '" + (dgProductos.CurrentRow.Index + 1) + "'", Conexion.varConexion);

                MySqlDataReader leopregunta = preguntar.ExecuteReader();

                if (leopregunta.Read())
                {
                    if(Nuevo_hadware.Cantidad < Maximo)
                    {
                        MessageBox.Show("La Cantidad digitada es menor a la ya existente ");
                        return;
                    }
                    Conexion.CerrarConexion();
                    Conexion.AbrirConexion();
                    MySqlCommand Actualizar = new MySqlCommand("UPDATE productos SET Cantidad='" + Nuevo_hadware.Cantidad + "' WHERE id_producto='" + vol + "'", Conexion.varConexion);
                    try
                    {
                        Actualizar.ExecuteNonQuery();
                        MessageBox.Show("Información: Producto Hadware Actualizado.");

                        tbFAbricante.Text = null;
                        tbModelo.Text = null;
                        tbNombreP.Text = null;
                        tbNSerial.Text = null;
                        tbPrecio.Text = null;
                        tbCantidad.Text = null;
                        tbEstante.Text = null;
                        tbExistenciaMin.Text = null;
                        Inicialp(false);
                        MostrarTablas();
                    }
                    finally
                    {
                        Conexion.CerrarConexion();

                    }

                    return;
                }
            }
            Conexion.AbrirConexion();

            MySqlCommand agregarNuevo_hadware = new MySqlCommand
                ("INSERT INTO productos (fabricante ,modelo ,nombreproducto,numeroserie,precio,cantidad,estante,tipo,existenciaminima ) VALUES('"
                + Nuevo_hadware.Fabricante + "','" + Nuevo_hadware.Modelo + "','" + Nuevo_hadware.Nombre
                + "','" + Nuevo_hadware.NumeroSerial + "','" + Nuevo_hadware.Precio + "','" +Nuevo_hadware.Cantidad
                + "','" +Nuevo_hadware.Estante+ "','" +Nuevo_hadware.Tipo+ "','" +Nuevo_hadware.ExistenciaMinima+"')", Conexion.varConexion);

            try
            {
                agregarNuevo_hadware.ExecuteNonQuery();
                MessageBox.Show("Información: nuevo Agregado.");
                tbFAbricante.Text = null;
                tbModelo.Text = null;
                tbNombreP.Text = null;
                tbNSerial.Text = null;
                tbPrecio.Text = null;
                tbCantidad.Text = null;
                tbEstante.Text = null;
                tbExistenciaMin.Text = null;
                Inicialp(false);
                MostrarTablas();
            }


            finally
            {
                Conexion.CerrarConexion();
            }
        }

        private void Click(object sender, EventArgs e)
        {

            Inicialp(true);

            Conexion.AbrirConexion();
            MySqlCommand tabla = new MySqlCommand("SELECT fabricante ,modelo ,nombreproducto,numeroserie,precio,cantidad,estante,tipo,existenciaminima   FROM  productos WHERE id_producto='" + (dgProductos.CurrentRow.Cells[0].Value) + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {

                Maximo = data.GetDecimal(5);
                tbFAbricante.Text = data.GetString(0);
                tbModelo.Text = data.GetString(1);
                tbNombreP.Text = data.GetString(2);
                tbNSerial.Text = data.GetString(3);
                tbPrecio.Text = data.GetString(4);
                tbCantidad.Text = data.GetString(5);
                tbEstante.Text = data.GetString(6);
                tbExistenciaMin.Text = data.GetString(7);
                vol = Convert.ToInt32(dgProductos.CurrentRow.Cells[0].Value);
                Inicialp(false);
                tbCantidad.IsEnabled = true;
                nuevo = true;

            }
            Conexion.CerrarConexion();
        }

        private void tbAssembly_Populating(object sender, System.Windows.Controls.PopulatingEventArgs e)
        {
            Conexion.AbrirConexion();
            MySqlCommand cmd = new MySqlCommand("SELECT nombrefabricante from fabrcantes", Conexion.varConexion);
            MySqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
               
                coleccionNombres.Add(data.GetString(1));
            }

            tbAssembly.ItemsSource = coleccionNombres;
            tbAssembly.PopulateComplete();
        }
    }
}
