using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class RacunStavka : INotifyPropertyChanged
{
    private string proizvod;
    public string Proizvod
    {
        get => proizvod;
        set
        {
            if (proizvod != value)
            {
                proizvod = value;
                OnPropertyChanged(nameof(Proizvod));
            }
        }
    }

    private decimal cena;
    public decimal Cena
    {
        get => cena;
        set
        {
            if (cena != value)
            {
                cena = value;
                OnPropertyChanged(nameof(Cena));
                OnPropertyChanged(nameof(Ukupno));
            }
        }
    }

    private int kolicina;
    public int Kolicina
    {
        get => kolicina;
        set
        {
            if (kolicina != value)
            {
                kolicina = value;
                OnPropertyChanged(nameof(Kolicina));
                OnPropertyChanged(nameof(Ukupno));
            }
        }
    }

    public decimal Ukupno => Cena * Kolicina;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

//definisemo izracunavanje ukupne cene stavke u okviru klase da se ukupna cena automatski azurira svaki put kad se promeni cena ili kolicina