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
using SalesSoft_v2;

namespace ejemplo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       

        public MainWindow()
        {
            if (!LlamarLogin())
            {
                Application.Current.Shutdown(-1);
            }
            InitializeComponent();
        }
       
        private bool LlamarLogin()
        {
            //instancio un objeto de la ventana login

            Login1 login = new Login1();
            //llamo esa ventana mientra ella no me responda el no puede hacer mas nada
            login.ShowDialog();

            //retorno verdadero o falso dependiendo la conexion
            return login.Conexio;

        }

    }
}
