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
using System.Windows.Controls.Ribbon;
using SalesSoft_v2.Recursos;
using MySql.Data.MySqlClient;
using SalesSoft_v2;

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            if (!LlamarLogin())
            {
                Application.Current.Shutdown(-1);
            }

           InitializeComponent();
        }

        //private void InitializeComponent()
        //{
        //    throw new NotImplementedException();
        //}

       /// <summary>
       /// Llama el login y retorna verdadero o falso dependiendo de la conexion 
       /// </summary>
       /// <returns></returns>
        private bool LlamarLogin()
        {
            //Instancio un objeto de la ventana login

            Login login = new Login();
            //Llamo esa ventana mientra ella no me responda el no puede hacer mas nada
            login.ShowDialog();

            //Retorno verdadero o falso dependiendo la conexion
            return login.Connection;

        }
    }
}
