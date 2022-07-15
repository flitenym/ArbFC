using Microsoft.EntityFrameworkCore;
using Storage.Module.Entities;

namespace Storage.Module
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Chain> Chains { get; set; }
        public DbSet<ExchangeChain> ExchangeChains { get; set; }
        public DbSet<Ticker> Tickers { get; set; }
    }
}