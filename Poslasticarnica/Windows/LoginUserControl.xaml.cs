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
            // Kreiramo vezu sa SQLite bazom podataka
            using (SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=Poslasticarnica_KorisniciDB.db;Version=3"))
            {
                sQLiteConnection.Open();

                string query = "SELECT * FROM users WHERE username = @unesiteIme AND password = @unesiteLozinku";
                using (SQLiteCommand cmd = new SQLiteCommand(query, sQLiteConnection))
                {
                    cmd.Parameters.AddWithValue("@unesiteIme", unesiteIme.Text);
                    cmd.Parameters.AddWithValue("@unesiteLozinku", unesiteLozinku.Password);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            User loggedInUser = new User
                            {
                                Username = reader["username"].ToString(),
                                Prezime = reader["prezime"].ToString(),
                                Email = reader["email"].ToString(),
                                Password = reader["password"].ToString()
                            };

                            // Postavljamo trenutno ulogovanog korisnika
                            SessionManager.CurrentUser = loggedInUser;


                            // Otvaramo glavni prozor aplikacije
                            PocetniProzor pocetniProzor = new PocetniProzor();
                            pocetniProzor.Show();

                            // Zatvaramo trenutni prozor (login prozor)
                            Window.GetWindow(this).Close();
                        }
                        else
                        {
                            MessageBox.Show("Podaci su pogrešni!");
                        }
                    }
                }
            }
        }
    }
}

