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
    }
}
