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
       /// <summary>
       /// llama el login y retorna verdadero o falso dependiendo de la conexion 
       /// </summary>
       /// <returns></returns>
        private bool LlamarLogin()
        {
            //instancio un objeto de la ventana login

            Login1 login = new Login1();
            //llamo esa ventana mientra ella no me responda el no puede hacer mas nada
            login.ShowDialog();

            //retorno verdadero o falso dependiendo la conexion
            return login.Conexio;

        }


        private void FormLoad(object sender, EventArgs e)
        {
            /////////////////////////////////////
            // Encode The Data
            /////////////////////////////////////
            Barcodes bb = new Barcodes();
            bb.BarcodeType = Barcodes.BarcodeEnum.Code39;
            bb.Data = "C123a";
            bb.CheckDigit = Barcodes.YesNoEnum.Yes;
            bb.encode();

            int thinWidth;
            int thickWidth;

            thinWidth = 3;
            thickWidth = 3 * thinWidth;

            string outputString = bb.EncodedData;
            string humanText = bb.HumanText;


            /////////////////////////////////////
            // Draw The Barcode
            /////////////////////////////////////
            int len = outputString.Length;
            int currentPos = 10;
            int currentTop = 10;
            int currentColor = 0;
            for (int i = 0; i < len; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Height = 200;
                if (currentColor == 0)
                {
                    currentColor = 1;
                    rect.Fill = new SolidColorBrush(Colors.Black);

                }
                else
                {
                    currentColor = 0;
                    rect.Fill = new SolidColorBrush(Colors.White);

                }
                Canvas.SetLeft(rect, currentPos);
                Canvas.SetTop(rect, currentTop);

                if (outputString[i] == 't')
                {
                    rect.Width = thinWidth;
                    currentPos += thinWidth;

                }
                else if (outputString[i] == 'w')
                {
                    rect.Width = thickWidth;
                    currentPos += thickWidth;

                }
              //  mainCanvas.Children.Add(rect);

            }


            /////////////////////////////////////
            // Add the Human Readable Text
            /////////////////////////////////////
            TextBlock tb = new TextBlock();
            tb.Text = humanText;
            tb.FontSize = 32;
            tb.FontFamily = new FontFamily("Courier New");
            Rect rx = new Rect(0, 0, 0, 0);
            tb.Arrange(rx);
            Canvas.SetLeft(tb, (currentPos - tb.ActualWidth) / 2);
            Canvas.SetTop(tb, currentTop + 205);
            //mainCanvas.Children.Add(tb);

        }

    }
}
