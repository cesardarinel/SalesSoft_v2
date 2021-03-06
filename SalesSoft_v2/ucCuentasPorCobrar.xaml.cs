﻿using System;
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
    /// Interaction logic for ucCuentasPorCobrar.xaml
    /// </summary>
    public partial class ucCuentasPorCobrar : UserControl, iTabbedMDI
    {
        public ucCuentasPorCobrar()
        {
            InitializeComponent();
            Inicializar(false);
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
                return "CuentasPorCobrar";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Cuentas Por Cobrar"; }
        }
        #endregion

        void Inicializar(bool a)
        {
            tbMiembroN.IsEnabled = a;
            tbNombre.IsEnabled = a;
            tbTelefono.IsEnabled = a;
            tbDireccion.IsEnabled = a;
            label1.Content = "";
            lbPrecio.Content = "";
           
        }
        decimal Label_Deuda(int id)
        {
            Decimal total = 0;
            Conexion.AbrirConexion();

            MySqlCommand cmd = new MySqlCommand("SELECT total FROM totalfactura WHERE   factura= '" + id + "' ", Conexion.varConexion);
            
            MySqlDataReader data = cmd.ExecuteReader();
            while (data.Read())
            {
                total += Convert.ToDecimal(data.GetString(0));

            }
            

           // lbPrecio.Content = "RD $"+total;
               Conexion.CerrarConexion();
               return total;

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }

        private void Limpiar(object sender, RoutedEventArgs e)
        {
            tbNombred.Text = null;
            tbID.Text = null;
        }

        private void Buscar(object sender, RoutedEventArgs e)
        {   
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            
            if (tbNombred.Text == string.Empty || tbID.Text != string.Empty)
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT nombrecompleto,id_cliente FROM clientes WHERE  id_cliente= '" + tbID.Text + "' ", Conexion.varConexion);
                DataSet dt = new DataSet();
                da.Fill(dt);
                dgFacturasPendientes.DataSource = dt.Tables[0];
                Conexion.CerrarConexion();
                return;

            }
            
            else 
            { 
            /////agregamos la base de datos a la pantalla 
            /////
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT nombrecompleto,id_cliente FROM clientes WHERE   nombrecompleto= '" + tbNombred.Text + "' ", Conexion.varConexion);
             DataSet dt = new DataSet();
            da.Fill(dt);
            dgFacturasPendientes.DataSource = dt.Tables[0];
            Conexion.CerrarConexion();
            return;
            }
            MessageBox.Show("El campo nombre Y ID Cliente vacío, porfavor llenar uno de ellos");
            return;

        }

        private void Click_cliente(object sender, EventArgs e)
        {
             Decimal Total=0;
            Cliente verFactiura = new Cliente(Convert.ToInt32(dgFacturasPendientes.CurrentRow.Cells[1].Value));
            
            
            tbMiembroN.Text = Convert.ToString(verFactiura.ID);
            tbNombre.Text = verFactiura.NombreCompleto;
            tbTelefono.Text =verFactiura.Telefono ;
            tbDireccion.Text =verFactiura.Direccion;


            MySqlDataAdapter da = new MySqlDataAdapter("SELECT *FROM facturas WHERE   pagada= '" + 1 + "' AND cliente= '" + verFactiura.ID + "'", Conexion.varConexion);
            DataSet dt = new DataSet();
            
            da.Fill(dt);
           
            dgFacturasIndividuales.DataSource = dt.Tables[0];
            
            label1.Content = "";
            lbPrecio.Content = "";
            int i = 0;
            while(dt.Tables[0].Rows.Count > i)
            {

                
                Total += Convert.ToInt32(dt.Tables[0].Rows[i][9]);
               i++;
            }

            lbPrecio.Content = "RD $" + Total;
            Conexion.CerrarConexion();
        }

        private void dgFacturasIndividuales_Click(object sender, EventArgs e)
        {
            Conexion.CerrarConexion();

            label1.Content = "RD $" + dgFacturasIndividuales.Rows[dgFacturasIndividuales.CurrentRow.Index].Cells[ 9 ].Value;
           Conexion.CerrarConexion();
        }
    }
}
