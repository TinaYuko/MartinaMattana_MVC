using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ristorante.Core.Entities;

namespace Ristorante.EF
{
    internal class PiattoConfiguration : IEntityTypeConfiguration<Piatto>
    {
        public void Configure(EntityTypeBuilder<Piatto> builder)
        {
            builder.ToTable("Piatti");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).IsRequired();
            builder.Property(p => p.Descrizione).IsRequired();
            builder.Property(p => p.Tipologia).IsRequired();
            builder.Property(p => p.Prezzo).HasColumnType("decimal(8,2)").IsRequired(); 


            //Relazione 1 -> n
            builder.HasOne(p => p.Menu).WithMany(m => m.Piatti).HasForeignKey(p => p.MenuId);
        }
    }
}