using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        builder.ToTable("PrescriptionMedicament");

        builder.HasKey(e => new { e.IdMedicament, e.IdPrescription }).HasName("PrescriptionMedicament_pk");

        builder.Property(e => e.Dose).IsRequired();

        builder.HasOne(e => e.Medicament)
            .WithMany()
            .HasForeignKey(e => e.IdMedicament)
            .HasConstraintName("PrescriptionMedicament_Medicament")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Prescription)
            .WithMany(p => p.PrescriptionMedicaments)
            .HasForeignKey(e => e.IdPrescription)
            .HasConstraintName("PrescriptionMedicament_Prescription")
            .OnDelete(DeleteBehavior.Restrict);
    }
}