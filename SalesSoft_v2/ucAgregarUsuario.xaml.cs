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
        int Activo;
        int vol;
        public ucAgregarUsuario()
        {
            InitializeComponent();
            CargarEmpleado();
            MostrarTablas();
            Inicio(false);
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
        
        #region Cargar/Mostrar
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
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            ///agregamos la base de datos a la pantalla 
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT* FROM empleados", Conexion.varConexion);

            DataSet dt = new DataSet();
            da.Fill(dt);
            dgUsuario.DataSource = dt.Tables[0];
            Conexion.CerrarConexion();
        }
        public void Inicio(bool a)
        {
            tbNombre.IsEnabled = a;
            tbNombreCompleto.IsEnabled = a;
            tbCedula.IsEnabled = a;
            tbTelefono.IsEnabled = a; ;
            tbClave.IsEnabled = a;
            cbActivo.IsEnabled = a;
            btAgregar.IsEnabled = a;
            cbTUsuario.IsEnabled = a;
            tbSueldo.IsEnabled = a;

        }
        #endregion



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }

        private void Agregar_Empleado(object sender, RoutedEventArgs e)
        {

            

            Empleado Empleado = new Empleado();//creamos un objeto empleado

            if (tbNombre.Text == string.Empty||tbNombre.Text.Length>10)
            {
                MessageBox.Show("El campo nombre esta vacío, porfavor llene con un Nombre ó tienes mas de 10 caracteres");
                return;
            }
            Empleado.Nombre = tbNombre.Text;
            if (tbNombreCompleto.Text == string.Empty || tbNombreCompleto.Text.Length > 60)
            {
                MessageBox.Show("El campo Nombre Completo esta vacío, porfavor llene con un Nombre Completo  ó tienes mas de 60 caracteres");
                return;
            }

            Empleado.NombreCompleto = tbNombreCompleto.Text;

            if (tbCedula.Text == string.Empty || tbCedula.Text.Length > 11)
            {
                MessageBox.Show("El campo Cedula esta vacío, porfavor llene con un Cedula ó tienes mas de 11 caracteres");
                return;
            }
            Empleado.Cedula = tbCedula.Text;
            if (tbClave.Text == string.Empty || tbClave.Text.Length > 30)
            {
                MessageBox.Show("El campo Clave esta vacío, porfavor llene con un Clave  ó tienes mas de 30 caracteres");
                return;
            }
            Empleado.Clave = tbClave.Text;
            if (tbTelefono.Text == string.Empty || tbTelefono.Text.Length > 10)
            {
                MessageBox.Show("El campo Tipo Telefono esta vacío, porfavor llene con un Tipo Usuario  ó tienes mas de 10 caracteres");
                return;
            }
            Empleado.Telefono = tbTelefono.Text;
            if (tbSueldo.Text == string.Empty || tbSueldo.Text.Length > 6)
            {
                MessageBox.Show("El campo Tipo Sueldo esta vacío, porfavor llene con un Tipo Usuario  ó tienes mas de 6 caracteres");
                return;
            }
            Empleado.Sueldo = Convert.ToDecimal(tbSueldo.Text);

            if (cbTUsuario.Text == string.Empty )
            {
                MessageBox.Show("El campo Tipo Usuario esta vacío, porfavor llene con un Tipo Usuario");
                return;
            }
            Empleado.TUsuario = Convert.ToInt32(cbTUsuario.SelectedIndex + 1);

            if (cbActivo.IsChecked == true)
            {
                Empleado.Activo = true;
                Activo = 1;
            }
            else
            {
                Empleado.Activo = false;
                Activo = 0;
            }

            
            
            if(nuevo)
            {

                Conexion.CerrarConexion();
                Conexion.AbrirConexion();
                MySqlCommand preguntar = new MySqlCommand("SELECT * FROM empleados WHERE id_empleado = '" + (dgUsuario.CurrentRow.Index + 1) + "'", Conexion.varConexion);

                MySqlDataReader leopregunta = preguntar.ExecuteReader();
                
                if (leopregunta.Read())
                {
                    Conexion.CerrarConexion();
                    Conexion.AbrirConexion();
                    MySqlCommand Actualizar = new MySqlCommand("UPDATE empleados SET nombreusuario='" + Empleado.Nombre + "', nombrecompleto='" + Empleado.NombreCompleto + "',clave='" + Empleado.Clave
                        + "',cedula='" + Empleado.Cedula + "',telefono='" + Empleado.Telefono + "',tipousuario='" + Empleado.TUsuario + "',sueldo='" + Empleado.Sueldo + "',estado='" + Activo + "' WHERE id_empleado='" + vol + "'", Conexion.varConexion);
                    try
                    {
                        Actualizar.ExecuteNonQuery();
                        MessageBox.Show("Información: Empleao Actualizado.");
                        
                        tbNombre.Text = null;
                        tbNombreCompleto.Text = null;
                        tbTelefono.Text = null;
                        tbClave.Text = null;
                        tbCedula.Text = null;
                        tbSueldo.Text = null;
                        nuevo = false;
                        Inicio(false);
                        MostrarTablas();
                    }
                    finally
                    {
                        Conexion.CerrarConexion();
                        
                    }

                    return;
                }
            }
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            MySqlCommand preguntar1 = new MySqlCommand("SELECT * FROM empleados WHERE nombreusuario = '" + Empleado.Nombre + "'", Conexion.varConexion);

            MySqlDataReader leopregunta1 = preguntar1.ExecuteReader();

            if (leopregunta1.Read())
            {
                MessageBox.Show("El campo Nombre ya esta en la base de datos ");
                return;
            }

            Conexion.CerrarConexion();
            Conexion.AbrirConexion();

            MySqlCommand agregarEmpleado = new MySqlCommand
                ("INSERT INTO empleados (tipousuario,nombreusuario,clave ,nombrecompleto ,cedula,telefono,sueldo,estado) VALUES('"
                + Empleado.TUsuario + "','" + Empleado.Nombre + "','" + Empleado.Clave + "','" + Empleado.NombreCompleto + "','" + Empleado.Cedula
                + "','" + Empleado.Telefono + "','" + Empleado.Sueldo + "','" + Activo + "')", Conexion.varConexion);

            try
            {
                agregarEmpleado.ExecuteNonQuery();
                MessageBox.Show("Información: Empleado Agregado.");
                tbNombre.Text = null;
                tbNombreCompleto.Text = null;
                tbTelefono.Text = null;
                tbClave.Text = null;
                tbCedula.Text = null;
                tbSueldo.Text = null;
                
                Inicio(false);
                MostrarTablas();

            }


            finally
            {
                Conexion.CerrarConexion();
            }
        }

        void dgUsuario_Click(object sender, EventArgs e)
        {

            Inicio(true);
            
            Conexion.AbrirConexion();
            MySqlCommand tabla = new MySqlCommand("SELECT tipousuario,nombreusuario,clave,nombrecompleto,cedula,telefono,sueldo,estado  FROM  empleados WHERE id_empleado='" + (dgUsuario.CurrentRow.Cells[0].Value) + "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {
                
                cbTUsuario.Text = data.GetString(0);
                tbNombre.Text = data.GetString(1);
                tbClave.Text = data.GetString(2);
                tbNombreCompleto.Text = data.GetString(3);
                
                tbCedula.Text = data.GetString(4);
                tbTelefono.Text = data.GetString(5);
                tbSueldo.Text = data.GetString(6);
               
                vol = Convert.ToInt32(dgUsuario.CurrentRow.Cells[0].Value);
                nuevo = true;

            } 
            Conexion.CerrarConexion();

        }

        private void Registrar(object sender, RoutedEventArgs e)
        {
            tbNombre.Text = null;
            tbNombreCompleto.Text = null;
            tbTelefono.Text = null;
            tbClave.Text = null;
            tbCedula.Text = null;
            tbSueldo.Text = null;
            nuevo = false;
            Inicio(true);
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            nuevo = false;
            tbNombre.Text = null;
            tbNombreCompleto.Text = null;
            tbTelefono.Text = null;
            tbClave.Text = null;
            tbCedula.Text = null;
            tbSueldo.Text = null;
            nuevo = false;
            Inicio(false);

        }



    }
}