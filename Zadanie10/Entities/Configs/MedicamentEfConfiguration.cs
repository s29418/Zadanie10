using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.ToTable("Medicament");

        builder.HasKey(e => e.IdMedicament).HasName("Medicament_pk");
        builder.Property(e => e.IdMedicament).UseIdentityColumn();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Type).IsRequired().HasMaxLength(100);
    }
}
