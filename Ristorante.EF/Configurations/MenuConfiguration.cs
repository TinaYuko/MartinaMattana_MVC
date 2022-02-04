using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ristorante.Core.Entities;

namespace Ristorante.EF
{
    internal class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menù");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Nome).IsRequired();

            //Relazione 1 -> n
            builder.HasMany(m => m.Piatti).WithOne(p => p.Menu).HasForeignKey(p => p.MenuId);
        }
    }
}