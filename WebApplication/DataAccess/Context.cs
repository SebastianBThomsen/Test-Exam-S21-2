using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Name of database
            optionsBuilder.UseSqlite($@"Data Source = Football.db");
        }
    }
}