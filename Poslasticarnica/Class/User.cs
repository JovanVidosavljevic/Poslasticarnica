using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

class User
    {

    public User() { }

    public User(string username, string password, string email, string prezime)
    {
        Username = username;
        Password = password;
        Email = email;
        Prezime = prezime;
        
        
    }

    public string Username { get; set; }

    public string Prezime { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    
}

