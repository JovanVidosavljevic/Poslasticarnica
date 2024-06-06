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
using System.Windows.Shapes;

namespace Poslasticarnica.Windows
{
    /// <summary>
    /// Interaction logic for Login2.xaml
    /// </summary>
    public partial class Login2 : Window
    {
        public Login2()
        {
            InitializeComponent();
        }

        private void LoginListView(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewLogin.SelectedIndex;
            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    GridLoginRegister.Children.Clear();
                    GridLoginRegister.Children.Add(new Windows.RegisterUserControll());
                    break;
                case 1:
                    GridLoginRegister.Children.Clear();
                    GridLoginRegister.Children.Add(new Windows.LoginUserControl()); 
                    break;
                
            }
        }

        private void MoveCursorMenu(int index)
        {
            TransakcijaLogin.OnApplyTemplate();
            GridCursor.Margin = new Thickness(( + (220 * index)), 0, 10, 0);
        }

        private void pocetniProzorPower(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
          
        }

        private void PomerajLoginProzor(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
