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

namespace ejemplo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MySqlConnection conexion = new MySqlConnection("Server=db4free.net;Database=portafolionumero;Uid=portafolio123;Pwd=123456;");

        public MainWindow()
        {

            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            conexion.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT *FROM usuarios WHERE nombre='" + nombre.Text + "' AND contrasena='" + cb.Text + "' ", conexion);
            MySqlDataReader leer = cmd.ExecuteReader();
            if (leer.Read())
                MessageBox.Show("conectado");
            else
                MessageBox.Show("Error");
            conexion.Close();
            // kjlkjlkjlkjlkjasdasd 
        }

    }
}
