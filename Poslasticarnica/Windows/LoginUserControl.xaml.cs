using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using System.Data.SQLite;
using System.Data;

namespace Poslasticarnica.Windows
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void btnLogin(object sender, RoutedEventArgs e)
        {

            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=Poslasticarnica_KorisniciDB.db;Version=3"); //konekcioni string
            //proveravamo da li je konekcija otvorena
            if(sQLiteConnection.State == ConnectionState.Closed)
                sQLiteConnection.Open();

            try
            {

                String query = "select count(1) from users where username =@unesiteIme and password =@unesiteLozinku"; //pravimo upit(query) 
                SQLiteCommand cmd = new SQLiteCommand(query,sQLiteConnection);  //koji ce se izvrsiti na ovoj konekciji

                cmd.CommandTimeout = (int)CommandType.Text;
                cmd.Parameters.AddWithValue("@unesiteIme", unesiteIme.Text);
                cmd.Parameters.AddWithValue("@unesiteLozinku", unesiteLozinku.Password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if(count == 1)
                {
                    User loggedInUser = new User();
                    {
                        Username = reader["username"].ToString(),
                        Prezime = reader["prezime"].ToString(),
                        Email = reader["email"].ToString(),
                        Password = reader["password"].ToString(),
                    };

                    TrenutniKorisnik.CurrentUser = loggedInUser;

                    PocetniProzor pocetniProzor = new PocetniProzor();
                    pocetniProzor.Show();

                    var myWindow = Window.GetWindow(this);
                    myWindow.Close();
                } else
                {
                    MessageBox.Show("Podaci su pogrešni!");
                }

            } 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
            finally
            {
                sQLiteConnection.Close();
            }
                                                                                                                              

        }

        private bool isUserExist(string unesiteIme, string unesiteLozinku)
        {
            foreach(User user in Global.Users) 
            {

                if((user.Username == unesiteIme) && (user.Password == unesiteLozinku))
                        return true;
                return false;              
            } return false;
        }
    }
}
