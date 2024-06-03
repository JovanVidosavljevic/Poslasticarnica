using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            RacunStavke = new ObservableCollection<RacunStavka>();
            ListaRacun.ItemsSource = RacunStavke;

            var piceControl = new Pice();
            piceControl.PiceDodato += PiceControl_PiceDodato;

            // dodavanje Pice kontrole u menuGrid
            MenuGrid.Children.Add(piceControl);
        }

        private void PiceControl_PiceDodato(string nazivPica, string cenaPica)
        {
            DodajPiceNaRacun(nazivPica, cenaPica);
        }

        public void DodajPiceNaRacun(string nazivPica, string cenaPica)
        {
            // provera formata unosa cene
            if (decimal.TryParse(cenaPica.Replace(" RSD", ""), out decimal cena))
            {
                // pronalazenje stavke sa istim nazivom
                var stavka = RacunStavke.FirstOrDefault(s => s.Proizvod == nazivPica);

                if (stavka != null)
                {
                    stavka.Kolicina++;
                }
                else
                {
                    // ako stavka ne postoji dodavanje nove stavke na racun
                    RacunStavke.Add(new RacunStavka() { Proizvod = nazivPica, Cena = cena, Kolicina = 1 });
                }

                // osvezavanje zataGrid-a
                ListaRacun.Items.Refresh();

                // azuriranje ukupnog iznosa
                AzurirajUkupno();
            }
            else
            {
                MessageBox.Show("Unesena cena nije u ispravnom formatu.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AzurirajUkupno()
        {
            // ukupan iznos na osnovu stavki
            Ukupno = RacunStavke.Sum(stavka => stavka.Cena * stavka.Kolicina);

            // postavljanje ukupnog iznosa u TextBlock
            UkupnoText.Text = $"Ukupno: {Ukupno:C} RSD";

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
                UserControl userControl = null;

                switch (index)
                {
                    case 0:
                        MenuGrid.Children.Clear();
                        MenuGrid.Children.Add(new Windows.Kolaci());
                        break;
                    case 1:
                        var piceControl = new Windows.Pice();
                        piceControl.PiceDodato += PiceControl_PiceDodato;
                        MenuGrid.Children.Clear();
                        MenuGrid.Children.Add(piceControl);
                        break;
                }
            }
        }

        private void MoveCursorMenu(int index)
        {
        }
    }
}