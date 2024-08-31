
namespace BankingApp.Models;
public class PaymentDto {
    public long UserId {get; set;}
    public long AddFundAccountId {get; set;}
    public long RemoveFundAccountId {get; set;}
    public int Amount {get; set;}
}