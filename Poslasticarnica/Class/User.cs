using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

class User
    {

    public User() { }

    public User(string username, string prezime, string password, string email)
    {
        Username = username;
        Prezime = prezime;
        Password = password;
        Email = email;
    }

    public string Username { get; set; }

    public string Prezime { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    
}

