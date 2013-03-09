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
    /// Interaction logic for ucAgregarUsuario.xaml
    /// </summary>
    public partial class ucAgregarUsuario : UserControl, iTabbedMDI
    {
        bool nuevo = false;
        public ucAgregarUsuario()
        {
            InitializeComponent();
            CargarEmpleado();
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
                return "AgregarUsuario";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Agregar Usuario"; }
        }
        #endregion

        public void CargarEmpleado()
        {
            cbTUsuario.Items.Clear();


            Conexion.AbrirConexion();
            MySqlCommand tabla = new MySqlCommand("SELECT *  FROM tipousuario", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();

            while (data.Read())
            {
                cbTUsuario.Items.Add(data.GetString(1));

            }
            Conexion.CerrarConexion();

        }
        public void MostrarTablas()
        {

            Conexion.AbrirConexion();
            ///agregamos la base de datos a la pantalla 
           MySqlDataAdapter da = new MySqlDataAdapter("SELECT* FROM empleados", Conexion.varConexion);
           
            DataSet dt = new DataSet();
            da.Fill(dt);
            dgUsuario.DataSource = dt.Tables[0];
            Conexion.CerrarConexion();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }

        private void Agregar_Empleado(object sender, RoutedEventArgs e)
        {
            
            Conexion.AbrirConexion();

            Empleado Empleado = new Empleado();//creamos un objeto empleado

            if (tbNombre.Text == string.Empty)
            {
                MessageBox.Show("El campo nombre esta vacío, porfavor llene con un Nombre");
                return;
            }
            Empleado.Nombre = tbNombre.Text;
            if (tbNombreCompleto.Text == string.Empty)
            {
                MessageBox.Show("El campo Nombre Completo esta vacío, porfavor llene con un Nombre Completo ");
                return;
            }
            
            Empleado.NombreCompleto = tbNombreCompleto.Text;

            if (tbCedula.Text == string.Empty)
            {
                MessageBox.Show("El campo Cedula esta vacío, porfavor llene con un Cedula");
                return;
            }
            Empleado.Cedula = tbCedula.Text;
            if (tbClave.Text == string.Empty)
            {
                MessageBox.Show("El campo Clave esta vacío, porfavor llene con un Clave");
                return;
            }
            Empleado.Clave = tbClave.Text;
            Empleado.Telefono = tbTelefono.Text;
            Empleado.Sueldo = Convert.ToDecimal( tbSueldo.Text);
    
            if (cbTUsuario.Text == string.Empty)
            {
                MessageBox.Show("El campo Tipo Usuario esta vacío, porfavor llene con un Tipo Usuario");
                return;
            }
            Empleado.TUsuario = Convert.ToInt32(cbTUsuario.SelectedIndex + 1);

            if (cbActivo.IsChecked == true)
                Empleado.Activo = true;
            else
                Empleado.Activo = false;
            
            Conexion.CerrarConexion();

            if (nuevo)
            {
                MySqlCommand preguntar = new MySqlCommand("SELECT referencia FROM empleados WHERE nombreusuario='" + Empleado.Nombre + "'", Conexion.varConexion);
                MySqlDataReader leopregunta = preguntar.ExecuteReader();
                if (leopregunta.Read())
                {
                    
                    Conexion.AbrirConexion();
                    MySqlCommand Actualizar = new MySqlCommand("UPDATE empleados SET nombresusuario='" + Empleado.Nombre + "', nombrecompleto='" + Empleado.NombreCompleto + "',clave='" + Empleado.Clave
                        + "',cedula='" + Empleado.Cedula + "',telefono='" + Empleado.Telefono + "',sueldo='" + Empleado.Sueldo + "',estado='" + Empleado.Activo + "'", Conexion.varConexion);
                    try
                    {
                        Actualizar.ExecuteNonQuery();
                        MessageBox.Show("Información: Empleao Agregado.");
                        MostrarTablas();
                        tbNombre.Text = null;
                        tbNombreCompleto.Text = null;
                        tbTelefono.Text = null;
                        tbClave.Text = null;
                        tbCedula.Text = null;
                        tbSueldo.Text = null;
                        nuevo = false;
                    }

                    finally
                    {
                        Conexion.CerrarConexion();

                    }

                    return;
                }
            }
            Conexion.AbrirConexion();
            MySqlCommand agregarEmpleado = new MySqlCommand
                ("INSERT INTO empleados (tipoempleado,nombreusuario,clave ,nombrecompleto ,cedula,telefono,sueldo,estado) VALUES('"
                + Empleado.TUsuario + "','" + Empleado.Nombre + "','" + Empleado.Clave + "','" + Empleado.NombreCompleto + "','" + Empleado.Cedula
                + "','" + Empleado.Telefono + "','" + Empleado.Sueldo  +"','"+1+"')", Conexion.varConexion);

            try
            {
                agregarEmpleado.ExecuteNonQuery();
                MessageBox.Show("Información: Empleado Agregado.");
               

            }

            finally
            {
                Conexion.CerrarConexion();
            }
        }
    }
}