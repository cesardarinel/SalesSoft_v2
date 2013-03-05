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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using SalesSoft_v2.Recursos;
namespace SalesSoft_v2
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login1 : Window
    {
        //variable conexion;
        public bool Conexio { get; set; }
        public Login1()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Conexion.AbrirConexion();
            Conexio = false;
            MySqlCommand preguntar = new MySqlCommand("SELECT *FROM usuarios WHERE nombre='" + textUsuario.Text + "' and contrasena='" + contrasena.Password + "'", Conexion.varConexion);
            MySqlDataReader data = preguntar.ExecuteReader();
            if (data.Read())
            {
                Conexio = true;
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
            Conexio = false;
            DialogResult = true;
        }
    }
}