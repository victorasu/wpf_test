using Akkodis.Model;
using Microsoft.EntityFrameworkCore;

namespace Akkodis.Data
{
    public class AkkodisDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\R1kK\\source\\repos\\victorasu\\wpf_test\\Akkodis\\akkodis.db"); //change
        }
    }
}