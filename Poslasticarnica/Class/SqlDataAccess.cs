using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows;
using Poslasticarnica.Windows;


    internal class SqlDataAccess
    {

        public SqlDataAccess() { }

        public bool SaveUser(User user)
        {
            SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=Poslasticarnica_KorisniciDB.db;Version=3"); //konekcioni string
            //proveravamo da li je konekcija otvorena
            if (sQLiteConnection.State == ConnectionState.Closed)
                sQLiteConnection.Open();

            try
            {

                String query = "insert into users (username,password,email,prezime)values('" + user.Username + "','" + user.Password + "','" + user.Email + "','" + user.Prezime +"')"; 
                SQLiteCommand cmd = new SQLiteCommand(query, sQLiteConnection);  //koji ce se izvrsiti na ovoj konekciji
                cmd.ExecuteScalar();//izvrsava se query

                return true;
             
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            finally
            {
                sQLiteConnection.Close();
            }
        }

    }

