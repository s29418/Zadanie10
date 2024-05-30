using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescription");

        builder.HasKey(e => e.IdPrescription).HasName("Prescription_pk");
        builder.Property(e => e.IdPrescription).UseIdentityColumn();

        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.DueDate).IsRequired();

        builder.HasOne(e => e.Patient)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(e => e.IdPatient)
            .HasConstraintName("Prescription_Patient")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Doctor)
            .WithMany(d => d.Prescriptions)
            .HasForeignKey(e => e.IdDoctor)
            .HasConstraintName("Prescription_Doctor")
            .OnDelete(DeleteBehavior.Restrict);
    }
}