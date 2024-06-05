using System;
using System.Windows;
using System.Windows.Controls;

namespace Poslasticarnica.Windows
{
    public partial class Kolaci : UserControl
    {
        public event Action<string, string> KolaciDodato;

        public Kolaci()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 1", "300 RSD");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 2", "554 RSD");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 3", "321 RSD");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 4", "665 RSD");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 5", "333 RSD");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 6", "353 RSD");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 7", "161 RSD");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 8", "574 RSD");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 9", "235 RSD");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 10", "567 RSD");
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 11", "235 RSD");
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 12", "444 RSD");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 13", "647 RSD");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 14", "374 RSD");
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 15", "865 RSD");
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 16", "745 RSD");
        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 17", "978 RSD");
        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            KolaciDodato?.Invoke("Cake 18", "235 RSD");
        }
    }
}
