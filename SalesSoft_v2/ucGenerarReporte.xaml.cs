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

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for ucGenerarReporte.xaml
    /// </summary>
    public partial class ucGenerarReporte : UserControl, iTabbedMDI
    {
        public ucGenerarReporte()
        {
            InitializeComponent();
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
                return "GenerarReporte";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Generar Reporte"; }
        }
        #endregion

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }
    }
}