using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(e => e.IdPatient).HasName("Patient_pk");
        builder.Property(e => e.IdPatient).UseIdentityColumn();

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Birthdate).IsRequired();
        
        builder.ToTable("Patient");
        
        builder.HasData(
            new Patient { IdPatient = 1, FirstName = "Jan", LastName = "Kowalski", Birthdate = new DateTime(1987, 1, 1) },
            new Patient { IdPatient = 2, FirstName = "Anna", LastName = "Nowak", Birthdate = new DateTime(1996, 2, 2) }
        );
        
    }
}