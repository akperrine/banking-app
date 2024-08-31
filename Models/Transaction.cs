using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models;

public class Transaction {
    [Key]
    private long id;
    private string toAccount;
    private string fromAccount;
    private int transferAmount;
    private DateTime timestamp;
    
    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("Account")]
    public long AccountId {get; set;}

    public Transaction() {

    }
}