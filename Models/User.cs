namespace BankingApp.Models;

public class User 
{
    public long Id {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
    public Account SavingsAccount {get; set;}
    public Account CheckingAccount {get; set;}


    public User() {
        
    }

     public User(string username, string password) {
        this.Username = username;
        this.Password = password;
    }
}