namespace BankingApp.Models;

public class Account {
    private long Id;
    private bool IsCheckingAccount;
    private int Balance;
    
    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("User")]
    public long UserId; {get; set;}
    public User? User { get; set;}

}