using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Media.TextFormatting;

namespace Poslasticarnica.Windows
{
    public partial class PocetniProzor : Window
    {
        internal ObservableCollection<RacunStavka> RacunStavke { get; set; }
        private decimal Ukupno { get; set; }

        public PocetniProzor()
        {
            InitializeComponent();
            LoadUserData();

            //za invertar
            MenuGrid.Children.Clear();
            //MenuGrid.Children.Add(new invertarControl());

            // postavljanje inicijalnog prikaza na Kolaci
            RacunStavke = new ObservableCollection<RacunStavka>();
            ListaRacun.ItemsSource = RacunStavke;

            var kolaciControl = new Kolaci();
            kolaciControl.KolaciDodato += KolaciControl_KolaciDodato;
            MenuGrid.Children.Add(kolaciControl);

            PocetnaMenu.SelectionChanged += MenuListView;
        }

        private void PiceControl_PiceDodato(string nazivPica, string cenaPica)
        {
            DodajProizvodNaRacun(nazivPica, cenaPica);
        }

        private void KolaciControl_KolaciDodato(string nazivKolaca, string cenaKolaca)
        {
            DodajProizvodNaRacun(nazivKolaca, cenaKolaca);
        }


        //dodajem proizvod na racun tako sto se prvo parsira cena
        public void DodajProizvodNaRacun(string nazivProizvoda, string cenaProizvoda)
        {
            /*parsiramo cenu kako bi proverili da je uneti broj tacnog formata,
            ako parsiranje ne uspe nece biti bacen izuzetak vec ce funkcija fratiti false
            sto nam omogucava da rukujemo porukom*/
            if (!decimal.TryParse(cenaProizvoda.Replace(" RSD", ""), out decimal cena))
            {
                MessageBox.Show("Unesena cena nije u ispravnom formatu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //izvrsava se pretraga liste,ako pronadje stavku uvecava kolicinu,u suprotnom dodaje je na racun
            var stavka = RacunStavke.FirstOrDefault(s => s.Proizvod == nazivProizvoda);
            if (stavka != null)
                stavka.Kolicina++;
            else
                RacunStavke.Add(new RacunStavka { Proizvod = nazivProizvoda, Cena = cena, Kolicina = 1 });

            ListaRacun.Items.Refresh();
            AzurirajUkupno();
        }

        //azuriramo ukupno
        private void AzurirajUkupno()
        {
            Ukupno = RacunStavke.Sum(stavka => stavka.Cena * stavka.Kolicina);
            UkupnoText.Text = $"Ukupno: {Ukupno.ToString("N2")} RSD";
        }

        private void Pomeraj(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //prikazivanje trenutno ulogovanog korisnika
        private void LoadUserData()
        {
            MessageBox.Show($"Ime korisnika: {SessionManager.CurrentUser.Username}");
            if (SessionManager.CurrentUser != null)
            {
                txtUsername.Text = SessionManager.CurrentUser.Username;
                
            }
        }


        //switch user kontrole
        private void MenuListView(object sender, SelectionChangedEventArgs e)
        {
            int index = PocetnaMenu.SelectedIndex;
            MoveCursorMenu(index);

            if (MenuGrid != null)
            {
                MenuGrid.Children.Clear();
                switch (index)
                {
                    case 0:
                        var kolaciControl = new Kolaci();
                        kolaciControl.KolaciDodato += KolaciControl_KolaciDodato;
                        MenuGrid.Children.Add(kolaciControl);
                        break;
                    case 1:
                        var piceControl = new Pice();
                        piceControl.PiceDodato += PiceControl_PiceDodato;
                        MenuGrid.Children.Add(piceControl);
                        break;
                    case 2:
                        var invertarControl = new Invertar();
                        MenuGrid.Children.Add(invertarControl);
                        break;
                }
            }
        }



        private void MoveCursorMenu(int index)
        {
          //
        }

        private void PocetniProzorClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }




        //naplati button
        private void Naplati_Click(object sender, RoutedEventArgs e)
        {
            // kreiranje prozora za stapanje
            PrintDialog printDialog = new PrintDialog();

            // podesavanje prozora za stampanje
            if (printDialog.ShowDialog() == true)
            {
                // postavljanje sadrzaja
                printDialog.PrintVisual(ListaRacun, "Štamparnje računa");
            }
        }
    
        //ukloni button
        private void ukloni(object sender, RoutedEventArgs e)
        {
            // provera da li postoji jedna stvar u datagridu
            if (ListaRacun.Items.Count > 0)
            {
                //dobijanje poslednje stavke
                RacunStavka poslednjaStavka = RacunStavke.LastOrDefault();

                // uklanjanje poslednje stavke iz kolekcije
                if (poslednjaStavka != null)
                {
                    RacunStavke.Remove(poslednjaStavka);

                    
                    ListaRacun.Items.Refresh();

                   
                    AzurirajUkupno();
                }
                else
                {
                    MessageBox.Show("Došlo je do greške prilikom pronalaženja poslednje stavke.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Nema stavki za uklanjanje.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //obrisi button

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            
            if (ListaRacun.Items.Count > 0)
            {
                
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite obrisati sve stavke iz liste?", "Potvrda brisanja", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                 
                    RacunStavke.Clear();

                    ListaRacun.Items.Refresh();

                    
                    AzurirajUkupno();
                }
            }
            else
            {
                MessageBox.Show("Nema stavki za brisanje.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        

        private void BtnLogOut(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

            Windows.Login2 login = new Windows.Login2();
            login.Visibility = Visibility.Visible;
            
        }
    }
}