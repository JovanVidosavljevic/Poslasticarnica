using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
    public partial class RegisterUserControll : UserControl
    {
        public RegisterUserControll()
        {
            InitializeComponent();
        }

        private void BtnRegister(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(unesiteIme.Text) && String.IsNullOrEmpty(unesiteLozinku.Password) && String.IsNullOrEmpty(unesitePrezime.Text) && String.IsNullOrEmpty(unesiteEmail.Text))
            {
                MessageBox.Show("Unesite podatke!");
            } 
            else
            {
                
                User user = new User(unesiteIme.Text,unesiteLozinku.Password,unesiteEmail.Text,unesitePrezime.Text); //Pravimo objekat USER-korisnik
                SqlDataAccess sqlDataAccess = new SqlDataAccess();
                

                if (sqlDataAccess.SaveUser(user))
                    MessageBox.Show("Korisnik uspešno unet!");
                else MessageBox.Show("Greška u kreiranju!");

            }
        }
    }
}
