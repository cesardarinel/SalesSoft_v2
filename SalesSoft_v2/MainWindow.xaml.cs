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
        private Dictionary<string, string> _mdiChildren = new Dictionary<string, string>();

        public MainWindow()
        {

           // Conexion.IdEntradaSistema = 1;
            if(!LlamarLogin())
            {
                Application.Current.Shutdown(-1);
            }
          
            InitializeComponent();
             Entrada_al_Sistema();
        }

        private void Entrada_al_Sistema()
        {
           Empleado Entrada = new Empleado();
             Entrada.Entrada_Sistema(Conexion.IdEntradaSistema);
             this.Title = "SalesSoft | Tienda Informátia||   Hola :" + Entrada.NombreCompleto + "";
            #region Tipos Usuarios
             switch (Entrada.TUsuario)
             {
                 case 1:
                     admin.IsEnabled = false;
                     Inventarios.IsEnabled = false;
                     listadoProductos.IsEnabled = false;
                     break;
                 case 2:
                     admin.IsEnabled = false;
                     Facturaciones.IsEnabled = false;
                     break;
                 case 3:
                     admin.IsEnabled = false;
                     Inventarios.IsEnabled = false;
                     break;
                 default:
                     break;
             }

            #endregion
        }
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

        /// <summary>
        /// Add tab item to the tab
        /// </summary>
        /// <param name="mdiChild">This is the user control</param>
        private void AddTab(iTabbedMDI mdiChild)
        {
            //Check if the user control is already opened
            if (_mdiChildren.ContainsKey(mdiChild.UniqueTabName))
            {
                //user control is already opened in tab. 
                //So set focus to the tab item where the control hosted
                foreach (object item in tcMdi.Items)
                {
                    TabItem ti = (TabItem)item;
                    if (ti.Name == mdiChild.UniqueTabName)
                    {
                        ti.Focus();
                        break;
                    }
                }
            }
            else
            {
                //the control is not open in the tab item
                tcMdi.Visibility = Visibility.Visible;
                tcMdi.Width = this.ActualWidth;
                tcMdi.Height = this.ActualHeight;

                ((iTabbedMDI)mdiChild).CloseInitiated += new delClosed(CloseTab);

                //create a new tab item
                TabItem ti = new TabItem();
                //set the tab item's name to mdi child's unique name
                ti.Name = ((iTabbedMDI)mdiChild).UniqueTabName;
                //set the tab item's title to mdi child's title
                ti.Header = ((iTabbedMDI)mdiChild).Title;
                //set the content property of the tab item to mdi child
                ti.Content = mdiChild;
                ti.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                ti.VerticalContentAlignment = VerticalAlignment.Top;
                //add the tab item to tab control
                tcMdi.Items.Add(ti);
                //set this tab as selected
                tcMdi.SelectedItem = ti;
                //add the mdi child's unique name in the open children's name list
                _mdiChildren.Add(((iTabbedMDI)mdiChild).UniqueTabName, ((iTabbedMDI)mdiChild).Title);

            }
        }

        /// <summary>
        /// Close a tab item
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="e"></param>
        private void CloseTab(iTabbedMDI tab, EventArgs e)
        {
            TabItem ti = null;
            foreach (TabItem item in tcMdi.Items)
            {
                if (tab.UniqueTabName == ((iTabbedMDI)item.Content).UniqueTabName)
                {
                    ti = item;
                    break;
                }
            }
            if (ti != null)
            {
                _mdiChildren.Remove(((iTabbedMDI)ti.Content).UniqueTabName);
                tcMdi.Items.Remove(ti);
            }
        }

        private void facturar_Click(object sender, RoutedEventArgs e)
        {
            ucFacturacion mdiChild = new ucFacturacion();
            AddTab(mdiChild);
        }

        private void cuentasPorCobrar_Click(object sender, RoutedEventArgs e)
        {
            ucCuentasPorCobrar mdiChild = new ucCuentasPorCobrar();
            AddTab(mdiChild);
        }

        private void listadoProductos_Click(object sender, RoutedEventArgs e)
        {
            ucListadoProductos mdiChild = new ucListadoProductos();
            AddTab(mdiChild);
        }

        private void almacenHardware_Click(object sender, RoutedEventArgs e)
        {
            ucAlmacenHardware mdiChild = new ucAlmacenHardware();
            AddTab(mdiChild);
        }

        private void almacenSoftware_Click(object sender, RoutedEventArgs e)
        {
            ucAlmacenSoftware mdiChild = new ucAlmacenSoftware();
            AddTab(mdiChild);
        }

        private void almacenPerifericos_Click(object sender, RoutedEventArgs e)
        {
            ucAlmacenPerifericos mdiChild = new ucAlmacenPerifericos();
            AddTab(mdiChild);
        }

        private void agregarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ucAgregarUsuario mdiChild = new ucAgregarUsuario();
            AddTab(mdiChild);
        }

        private void generarReporte_Click(object sender, RoutedEventArgs e)
        {
            ucGenerarReporte mdiChild = new ucGenerarReporte();
            AddTab(mdiChild);
        }

        private void modificarFactura_Click(object sender, RoutedEventArgs e)
        {
            ucModificarFactura mdiChild = new ucModificarFactura();
            AddTab(mdiChild);
        }
    }
}
