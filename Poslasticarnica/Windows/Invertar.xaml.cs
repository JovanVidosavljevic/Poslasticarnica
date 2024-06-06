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
using System.Data;
using System.Data.SQLite;

namespace Poslasticarnica.Windows
{
    /// <summary>
    /// Interaction logic for Invertar.xaml
    /// </summary>
    public partial class Invertar : UserControl
    {
        public Invertar()
        {
            InitializeComponent();
            LoadInverter();
        }

        private void LoadInverter()
        {
            try
            {
                //konekcioni string
                SQLiteConnection connection = new SQLiteConnection("Data Source=Invertar.DB;Version=3;");
                connection.Open();
                //sqlite komandni objekat
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Proizvodi";
                //cuva u sebi rezultat ovog upita,rezultati tabele
                SQLiteDataAdapter DB = new SQLiteDataAdapter(command.CommandText, connection);
                connection.Close();

                /*adapter u sebi samo cuva podatke
                DS = dataset*/
                DataSet DS = new DataSet();
                DB.Fill(DS);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    InverterDataGrid.ItemsSource = DS.Tables[0].DefaultView;
                }
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void BtnUkloni(object sender, RoutedEventArgs e)
        {
            if (InverterDataGrid.SelectedItem != null)
            {
                DataRowView rowView = InverterDataGrid.SelectedItem as DataRowView;
                if (rowView != null)
                {
                    int id = Convert.ToInt32(rowView["ID"]);

                    try
                    {
                        using (SQLiteConnection connection = new SQLiteConnection("Data Source=Invertar.DB;Version=3;"))
                        {
                            connection.Open();
                            SQLiteCommand command = new SQLiteCommand(connection);
                            command.CommandText = "DELETE FROM Proizvodi WHERE ID = @ID";
                            command.Parameters.AddWithValue("@ID", id);
                            command.ExecuteNonQuery();
                        }

                        
                        LoadInverter();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Molimo izaberite stavku za uklanjanje.");
            }
        }

        private void BtnDodaj(object sender, RoutedEventArgs e)
        {
            try
            {
               
                using (SQLiteConnection connection = new SQLiteConnection("Data Source=Invertar.DB;Version=3;"))
                {
                    connection.Open();

                    
                    string sqlQuery = "INSERT INTO Proizvodi (imeProizvoda, cenaProizvoda, kolicinaNaStanju) VALUES (@Naziv, @Kolicina, @Cena)";

                    
                    using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                    {
                        // Postavljanje parametara
                        command.Parameters.AddWithValue("@Naziv", "Novi proizvod");
                        command.Parameters.AddWithValue("@Kolicina", 10);
                        command.Parameters.AddWithValue("@Cena", 100);

                        command.ExecuteNonQuery();
                    }
                }

                LoadInverter();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
