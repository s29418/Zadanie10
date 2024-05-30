using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("Patient");

        builder.HasKey(e => e.IdPatient).HasName("Patient_pk");
        builder.Property(e => e.IdPatient).UseIdentityColumn();

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Birthdate).IsRequired();
    }
}