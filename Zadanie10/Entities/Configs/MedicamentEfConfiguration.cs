using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.HasKey(e => e.IdMedicament).HasName("Medicament_pk");
        builder.Property(e => e.IdMedicament).UseIdentityColumn();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Type).IsRequired().HasMaxLength(100);
        
        builder.ToTable("Medicament");
        
        builder.HasData(
            new Medicament { IdMedicament = 1, Name = "AAA", Description = "OPIS_A", Type = "TYP_A" },
            new Medicament { IdMedicament = 2, Name = "BBB", Description = "OPIS_B", Type = "TYP_B" }
        );
    }
}
