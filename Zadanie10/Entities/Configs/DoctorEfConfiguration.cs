using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(e => e.IdDoctor).HasName("Doctor_pk");
        builder.Property(e => e.IdDoctor).UseIdentityColumn();

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        
        builder.ToTable("Doctor");
        
        builder.HasData(
            new Doctor { IdDoctor = 1, FirstName = "Filip", LastName = "Kowalski", Email = "fk@gmail.com" },
            new Doctor { IdDoctor = 2, FirstName = "Joanna", LastName = "Pietruszka", Email = "jp@gmail.com" }
        );
    }
}
