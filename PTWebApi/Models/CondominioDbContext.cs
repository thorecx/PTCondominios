using Microsoft.EntityFrameworkCore;

namespace PTWebApi.Models
{
    public class CondominioDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = localhost\MSSQLSERVER01; Initial Catalog = Condominios; Integrated Security = True");
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Residencial> Residenciales { get; set; }
        public DbSet<Cuota> Cuotas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Estado> Estados { get; set; }
    }
}
