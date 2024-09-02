using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp
{
    public class BankingAppContext : DbContext {
        public BankingAppContext(DbContextOptions<BankingAppContext> options) 
            : base(options) {
        }

        public DbSet<User> Users { get; set;} = null!;
        public DbSet<Account> Accounts { get; set;} = null!;
    }
}