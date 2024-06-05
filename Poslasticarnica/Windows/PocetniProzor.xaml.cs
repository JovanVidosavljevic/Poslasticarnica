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

        public void DodajProizvodNaRacun(string nazivProizvoda, string cenaProizvoda)
        {
            if (decimal.TryParse(cenaProizvoda.Replace(" RSD", ""), out decimal cena))
            {
                var stavka = RacunStavke.FirstOrDefault(s => s.Proizvod == nazivProizvoda);

                if (stavka != null)
                {
                    stavka.Kolicina++;
                }
                else
                {
                    RacunStavke.Add(new RacunStavka() { Proizvod = nazivProizvoda, Cena = cena, Kolicina = 1 });
                }

                ListaRacun.Items.Refresh();
                AzurirajUkupno();
            }
            else
            {
                MessageBox.Show("Unesena cena nije u ispravnom formatu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AzurirajUkupno()
        {
            Ukupno = RacunStavke.Sum(stavka => stavka.Cena * stavka.Kolicina);
            UkupnoText.Text = $"Ukupno: {Ukupno.ToString("N2")} RSD";
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
                }
            }
        }

        private void MoveCursorMenu(int index)
        {
            
        }

        private void PocetniProzorClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

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

      

        private void UkloniSaListe_Click(object sender, RoutedEventArgs e)
        {

        }

       

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
    }
}