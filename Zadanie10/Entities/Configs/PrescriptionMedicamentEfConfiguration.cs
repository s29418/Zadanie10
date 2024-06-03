using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        builder.HasKey(e => new { e.IdMedicament, e.IdPrescription });

        builder.Property(e => e.Dose).IsRequired();
        builder.Property(e => e.Details).HasMaxLength(100);

        builder.HasOne(e => e.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdPrescription)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Medicament)
            .WithMany(m => m.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdMedicament)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("PrescriptionMedicament");

        builder.HasData(
            new PrescriptionMedicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "Codziennie po pierwszym posiłku" },
            new PrescriptionMedicament { IdMedicament = 2, IdPrescription = 2, Dose = 1, Details = "Codziennie przed spaniem" }
        );
    }
}