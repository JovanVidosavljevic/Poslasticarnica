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

namespace Poslasticarnica.Windows
{
    /// <summary>
    /// Interaction logic for Pice.xaml
    /// </summary>
    public partial class Pice : UserControl
    {
       
        public event Action<string,string> PiceDodato;

        public Pice()
        {
            InitializeComponent();
        }

       
        private void Button_Coke(object sender, RoutedEventArgs e)
        {
            
            PiceDodato?.Invoke("Coca-Cola", "160 RSD");
            
        }

        private void Button_Fanta(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Fanta", "160 RSD");
        }

        private void Button_PepsiLime(object sender, RoutedEventArgs e)
        {

            PiceDodato?.Invoke("Pepsi Lime", "160 RSD");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Red Bull", "240 RSD");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Sprite", "160 RSD");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Pepsi Zero", "160 RSD");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Dr Pepper", "330 RSD");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Canada Dry", "330 RSD");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("High Brew", "400 RSD");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Mountain Dew", "400 RSD");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Starbucks", "400 RSD");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Starbucks", "650 RSD");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Starbucks", "650 RSD");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("7Up Cherry", "160 RSD");
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Monster", "360 RSD");
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Red Bull", "440 RSD");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("SKOL", "130 RSD");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            PiceDodato?.Invoke("Voda", "65 RSD");
        }
    }
}
