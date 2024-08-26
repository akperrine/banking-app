namespace BankingApp.Models;

public class UserDto 
{
    public long Id {get; set;}
    public string Username {get; set;}

    public UserDto(long id, string username) 
    {
        this.Id = id;
        this.Username = username;
    }
}