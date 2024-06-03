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
    /// Interaction logic for PocetniProzor.xaml
    /// </summary>
    public partial class PocetniProzor : Window
    {
        public PocetniProzor()
        {
            InitializeComponent();
            LoadUserData();
        }

       

        private void Pomeraj(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LoadUserData()
        {
            MessageBox.Show($"Ime korisnika: {SessionManager.CurrentUser.Username}");
            if (SessionManager.CurrentUser != null)
            {
                txtUsername.Text = SessionManager.CurrentUser.Username;
                txtPrezime.Text = SessionManager.CurrentUser.Prezime;
            }
        }


        private void MenuListView(object sender, SelectionChangedEventArgs e)
        {
            int index = PocetnaMenu.SelectedIndex;
            MoveCursorMenu(index);

           if(MenuGrid != null)
            {
                MenuGrid.Children.Clear();
                UserControl userControl = null;

                switch (index)
                {
                    case 0:
                        MenuGrid.Children.Clear();
                        MenuGrid.Children.Add(new Windows.Kolaci());
                        break;
                    case 1:
                        MenuGrid.Children.Clear();
                        MenuGrid.Children.Add(new Windows.Pice());
                        break;
                }
            }


            
        }

        private void MoveCursorMenu(int index)
        {          
        }
    }
}
