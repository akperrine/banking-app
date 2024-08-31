namespace BankingApp.Models;

public class User 
{
    public long Id {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
    public List<Account> Accounts {get; set;}

    public User() {
        Accounts = new List<Account>();
    }

     public User(string username, string password) {
        this.Username = username;
        this.Password = password;
    }
}