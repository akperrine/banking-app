using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankingApp.Models;

public class Account {
    [Key]
    public long Id {get; set;}
    public bool IsCheckingAccount {get; set;}
    public int Balance {get; set;}
    
    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("User")]
    public long UserId {get; set;}

    public Account() {

    }

    public Account(long userId, bool isCheckingAccount) {
        this.UserId = userId;
        this.IsCheckingAccount = isCheckingAccount;
    }
}