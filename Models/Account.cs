using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models;

public class Account {
    [Key]
    private long Id;
    private bool IsCheckingAccount;
    private int Balance;
    
    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("User")]
    public long UserId {get; set;}

}