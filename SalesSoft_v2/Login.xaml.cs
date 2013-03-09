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
            //Configure the ProgressBar
            Proces.Minimum = 0;
            Proces.Maximum = short.MaxValue;
            Proces.Value = 0;

            //Stores the value of the ProgressBar
            double value = 0;

            //Create a new instance of our ProgressBar Delegate that points
            // to the ProgressBar's SetValue method.
            UpdateProgressBarDelegate updatePbDelegate =
                new UpdateProgressBarDelegate(Proces.SetValue);

            //Tight Loop: Loop until the ProgressBar.Value reaches the max
            do
            {
                value += 2;

                /*Update the Value of the ProgressBar:
                    1) Pass the "updatePbDelegate" delegate
                       that points to the Proces.SetValue method
                    2) Set the DispatcherPriority to "Background"
                    3) Pass an Object() Array containing the property
                       to update (ProgressBar.ValueProperty) and the new value */
                Dispatcher.Invoke(updatePbDelegate,
                    System.Windows.Threading.DispatcherPriority.Background,
                    new object[] { ProgressBar.ValueProperty, value });
            }
            while (Proces.Value != Proces.Maximum);
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Proces.Visibility = System.Windows.Visibility.Visible;
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            Connection = false;

            MySqlCommand preguntar = new MySqlCommand("SELECT *FROM empleados WHERE nombreusuario='" + textUsuario.Text + "' and clave='" + contrasena.Password + "'", Conexion.varConexion);
            MySqlDataReader data = preguntar.ExecuteReader();
            Process();
            if (data.Read())
            {
                Connection = true;
                Conexion.CerrarConexion();
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña Incorecto");
                data.Close();
            }

            Conexion.CerrarConexion();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Connection = false;
            DialogResult = true;
        }
    }
}
