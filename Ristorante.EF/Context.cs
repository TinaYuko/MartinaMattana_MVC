using Microsoft.EntityFrameworkCore;
using Ristorante.Core.Entities;

namespace Ristorante.EF
{
    public class Context: DbContext
    {
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Piatto> Piatti { get; set; }
        public DbSet<Utente> Utenti { get; set; }
        public Context()
        {

        }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Ristorante;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Menu>(new MenuConfiguration());
            modelBuilder.ApplyConfiguration<Piatto>(new PiattoConfiguration());
            modelBuilder.ApplyConfiguration<Utente>(new UtenteConfiguration());
        }
    }
}