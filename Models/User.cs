namespace BankingApp.Models;

public class User 
{
    public long Id {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}


    public User() {
        
    }

    public User(long id, string username, string password) 
    {
        this.Id = id;
        this.Username = username;
        this.Password = password;
    }

     public User(string username, string password) 
    {
        this.Username = username;
        this.Password = password;
    }
}