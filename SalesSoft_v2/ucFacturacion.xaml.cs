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


//Clases y Conexión
using MySql.Data.MySqlClient;
using SalesSoft_v2.Recursos;
using System.Data;

namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for ucFacturacion.xaml
    /// </summary>
    public partial class ucFacturacion : UserControl, iTabbedMDI
    {
        DataTable dt = new DataTable(); //creas una tabla
        decimal suma = 0;
        public ucFacturacion()
        {
            InitializeComponent();
            lbDevuelta.Content = null;
            lbpagar.Content = null;
            dt.Columns.Add("Producto"); //le creas las columnas
            dt.Columns.Add("Precio");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("ID"); 
        }
        #region AgregarCliente
        private void Procesar(object sender, RoutedEventArgs e)
        {
            if (tbNMienbro.Text == string.Empty )
            {
                MessageBox.Show("El campo N Miembro  esta vacío, porfavor llene el Campo");
                return;
            }
            Cliente ClienteActivo = new Cliente(Convert.ToInt32(tbNMienbro.Text));
            tbNombre.Text= ClienteActivo.NombreCompleto;
            tbDireccion.Text = ClienteActivo.Direccion;
            tbTelefono.Text = ClienteActivo.Telefono;
            cbActivo.IsChecked = ClienteActivo.TipoCliente;
        }

        private void AgregarCliente(object sender, RoutedEventArgs e)
        {
            if (tbNMienbro.Text != string.Empty)
            {
                MessageBox.Show("El campo N Miembro  esta Lleno, No  Puede Cargar el Cliente");
                return;
            }
            Cliente Cliente_nuevo = new Cliente();
            if (tbNombre.Text == string.Empty || tbNombre.Text.Length > 60)
            {
                MessageBox.Show("El campo nombre esta vacío, porfavor llene con un Nombre ó tienes mas de 10 caracteres");
                return;
            }
            Cliente_nuevo.NombreCompleto= tbNombre.Text;
            if (tbDireccion.Text == string.Empty || tbDireccion.Text.Length > 50)
            {
                MessageBox.Show("El campo Direccion esta vacío, porfavor llene con una Direccion ó tienes mas de 50 caracteres");
                return;
            }
            Cliente_nuevo.Direccion = tbDireccion.Text;
            if (tbTelefono.Text == string.Empty || tbTelefono.Text.Length > 10)
            {
                MessageBox.Show("El campo Telefono esta vacío, porfavor llene con un Telefono ó tienes mas de 10 caracteres");
                return;
            }
            Cliente_nuevo.Telefono = tbTelefono.Text;
            if(cbActivo.IsChecked==true)
                Cliente_nuevo.TipoCliente = true;
            else
                Cliente_nuevo.TipoCliente = false;

            Conexion.AbrirConexion();

            MySqlCommand agregarCliente_nuevo = new MySqlCommand
                ("INSERT INTO clientes (nombrecompleto,tipocliente, direccion,telefono) VALUES('"
                + Cliente_nuevo.NombreCompleto + "','" + Convert.ToInt32(Cliente_nuevo.TipoCliente) + "','" + Cliente_nuevo.Direccion
                + "','" + Cliente_nuevo.Telefono + "')", Conexion.varConexion);

            try
            {
                agregarCliente_nuevo.ExecuteNonQuery();
                MessageBox.Show("Información: Cliente Agregado.");
                cbActivo.IsChecked = false;
                tbDireccion.Text = string.Empty;
                tbNMienbro.Text = string.Empty;
                tbNombre.Text = string.Empty;
                tbTelefono.Text = string.Empty;

            }


            finally
            {
                Conexion.CerrarConexion();
            }

        }
        
        #endregion

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
                return "Facturación";
            }
        }

        /// <summary>
        /// This is the title that will be shown in the tab.
        /// </summary>
        public string Title
        {
            get { return "Facturación"; }
        }
        #endregion

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (CloseInitiated != null)
            {
                CloseInitiated(this, new EventArgs());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int tipo = 0;
            if (tbNombre.Text == string.Empty || tbDireccion.Text == string.Empty || tbNMienbro.Text == string.Empty)
            {
                MessageBox.Show("El un campo del Cliente esta vacío, porfavor llenar ");
                return;
            }
            if (tbCantidad.Text == string.Empty )
            {
                MessageBox.Show("El campo Cantidad esta vacío, porfavor llenar ");
                return;
            }
            if (cbFormaPago.Text == string.Empty)
            {
                MessageBox.Show("El campo Forma de pago esta vacío, porfavor llenar ");
                return;
            }
            
            
            Conexion.AbrirConexion();
            MySqlCommand tabla = new MySqlCommand("SELECT tipo  FROM  productos WHERE nombreproducto='" + tbNombreP.Text+ "'", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {
                tipo=data.GetInt32(0);
            }
            Conexion.CerrarConexion();
           
            switch (tipo)
            {
                case 1:
                    Hardware comprarH = new Hardware(tbNombreP.Text);
                    tbPrecio.Text=Convert.ToString( comprarH.Precio);

                    suma+=comprarH.Precio*Convert.ToInt32(tbCantidad.Text);
                    lbpagar.Content = suma +"-10% ";
                    DataRow row = dt.NewRow(); //creas un registro
                     row["Producto"] = comprarH.Nombre; //Le añadres un valor
                     row["Precio"] = comprarH.Precio;
                     row["Cantidad"] = tbCantidad.Text;
                     row["ID"] = comprarH.ID_Producto;
                    dt.Rows.Add(row); //añades el registro a la tabla
                    dgProductos.DataSource = dt; //añades la tabla al datagrid
                    dgProductos.Update(); //actualizas
                    break;
                case 2:
                    Software comprars = new Software(tbNombreP.Text);
                    tbPrecio.Text=Convert.ToString( comprars.Precio);

                    suma += comprars.Precio * Convert.ToInt32(tbCantidad.Text);
                    lbpagar.Content = suma;
                    DataRow rows = dt.NewRow(); //creas un registro
                    rows["Producto"] = comprars.Nombre; //Le añadres un valor
                    rows["Precio"] = comprars.Precio;
                    rows["Cantidad"] = tbCantidad.Text;
                    rows["ID"] = comprars.ID_Producto;
                    dt.Rows.Add(rows); //añades el registro a la tabla
                    dgProductos.DataSource = dt; //añades la tabla al datagrid
                    dgProductos.Update(); //actualizas
                    break;
                case 3:
                    Periferico comprarp = new Periferico(tbNombreP.Text);
                    tbPrecio.Text=Convert.ToString( comprarp.Precio);

                    suma += comprarp.Precio * Convert.ToInt32(tbCantidad.Text);
                    lbpagar.Content = suma;
                    DataRow rowp = dt.NewRow(); //creas un registro
                    rowp["Producto"] = comprarp.Nombre; //Le añadres un valor
                    rowp["Precio"] = comprarp.Precio;
                    rowp["Cantidad"] = tbCantidad.Text;
                    rowp["ID"] = comprarp.ID_Producto;
                    dt.Rows.Add(rowp); //añades el registro a la tabla
                    dgProductos.DataSource = dt; //añades la tabla al datagrid
                    dgProductos.Update(); //actualizas
                    break;
               
                default:
                    break;
            }
                    
        }


        private void btnGuardar_Click_1(object sender, RoutedEventArgs e)
        {

            string Producto, Precio, Cantidad,id;
            int id_factura=0;
            int activo = 0;
            decimal total = 0;
            if (tbPago.Text == string.Empty)
            {
                MessageBox.Show("El campo Pago esta vacío, porfavor llenar ");
                return;
            }
            if (cbcredito.IsChecked == true)
                activo = 1;
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            MySqlCommand tabla = new MySqlCommand("SELECT id_factura FROM  facturas ", Conexion.varConexion);
            MySqlDataReader data = tabla.ExecuteReader();
            while (data.Read())
            {
                id_factura = data.GetInt32(0);
            }
            id_factura++;
            foreach (System.Windows.Forms.DataGridViewRow row in dgProductos.Rows)
                {
                    if (Convert.ToString(row.Cells[0].Value) == string.Empty)
                    {
                        Conexion.CerrarConexion();
                        Conexion.AbrirConexion();
                        MySqlCommand tablass = new MySqlCommand("SELECT * FROM clientes WHERE tipocliente='1' AND nombrecompleto='"+tbNombre.Text+"'", Conexion.varConexion);
                        MySqlDataReader datass = tabla.ExecuteReader();
                        if (datass.Read())
                        {
                            Cargar_final(id_factura, (total-(total*(decimal) 0.10 )));
                        }
                        else
                        {
                            Cargar_final(id_factura, total);
                        }
                        return;
                    }

                    Producto = Convert.ToString(row.Cells[0].Value);
                    Precio = Convert.ToString(row.Cells[1 ].Value);
                    Cantidad = Convert.ToString(row.Cells[2 ].Value);
                    id= Convert.ToString(row.Cells[3].Value);
                    total += Convert.ToDecimal(Convert.ToInt32(Precio) * Convert.ToInt32(Cantidad));
                    Conexion.CerrarConexion();
                    Conexion.AbrirConexion();

                    MySqlCommand agregarfactura = new MySqlCommand
                        ("INSERT INTO facturas (id_factura,cliente,producto,cantidad,facturador,pagada,formapago,fechafactura,precio) VALUES('"
                        + id_factura + "','" +tbNMienbro.Text+ "','" +id+ "','" + Cantidad + "','" +Conexion.IdEntradaSistema
                        + "','" + activo + "','" + cbFormaPago.Text + "','" + Convert.ToString(DateTime.Today) + "','" +
                        Convert.ToInt32(Precio) * Convert.ToInt32(Cantidad) + "')", Conexion.varConexion);
                    try
                    {
                        agregarfactura.ExecuteNonQuery();
                       
                    }


                    finally
                    {
                        Conexion.CerrarConexion();
                    }
                
            }
         
        }
        void Cargar_final(int id_factura, decimal total)
        {
            Conexion.CerrarConexion();
            Conexion.AbrirConexion();
            MySqlCommand agregar = new MySqlCommand("INSERT INTO totalfactura (factura,total) VALUES('"
                       + id_factura + "','" + total + "')", Conexion.varConexion);
            try
            {
                agregar.ExecuteNonQuery();
                tbNombreP.Text = null;
                tbPrecio.Text = null;
                tbCantidad.Text = null;
                tbNombre.Text = null;
                tbTelefono.Text = null;
                tbDireccion.Text = null;
                tbNMienbro.Text = null;
                lbDevuelta.Content ="RD $"+ (total - Convert.ToInt32(tbPago.Text));
                MessageBox.Show("Gracias Por Su Compra !!!");
            }
            finally
            {
                Conexion.CerrarConexion();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tbNombreP.Text = null;
            tbPrecio.Text = null;
            tbCantidad.Text = null;
        }
    }
}
