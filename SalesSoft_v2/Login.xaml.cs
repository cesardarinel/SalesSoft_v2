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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using SalesSoft_v2.Recursos;
using System.Threading;
using System.Windows.Threading;

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        
        //variable conexion;
        public bool Connection { get; set; }

        public Login()
        {
            
           
            InitializeComponent();
            Proces.Visibility = System.Windows.Visibility.Hidden;   
        }

        private delegate void UpdateProgressBarDelegate(
        System.Windows.DependencyProperty dp, Object value);

        private void Process()
        {
           

            Proces.Minimum = 0;
            Proces.Maximum = short.MaxValue;
            Proces.Value = 0;

            
            double value = 0;

            
            UpdateProgressBarDelegate updatePbDelegate =
                new UpdateProgressBarDelegate(Proces.SetValue);

           
            do
            {
                value += 2;

                
                Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, value });
            }
            while (Proces.Value != Proces.Maximum);
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Proces.Visibility = System.Windows.Visibility.Visible;
            Cancelar.IsEnabled = false;
            Aceptar.IsEnabled = false;
            Conexion.AbrirConexion();
            Connection = false;

            MySqlCommand preguntar = new MySqlCommand("SELECT id_empleado,nombreusuario,clave FROM empleados WHERE nombreusuario='" + textUsuario.Text + "' AND clave='" + contrasena.Password + "'", Conexion.varConexion);
            MySqlDataReader data = preguntar.ExecuteReader();
            Process();
            if (data.Read())
            {
                Connection = true;
                Conexion.IdEntradaSistema=Convert.ToInt32( data.GetString(0));
                Conexion.CerrarConexion();
                
               DialogResult = true;
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña Incorecto");
                data.Close();
                Cancelar.IsEnabled = true;
                Aceptar.IsEnabled = true;
            }

            Conexion.CerrarConexion();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Connection = false;
            this.DialogResult = true;
        }
    }
}
