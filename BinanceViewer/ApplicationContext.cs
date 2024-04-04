using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceAcountViewer
{
    public class ApplicationContext : DbContext
    {
        
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<OrderH> Orders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ordersDb3;Username=postgres;Password=postgres");
        }


    }
}
