using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zadanie10.Entities.Configs;

public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");

        builder.HasKey(e => e.IdDoctor).HasName("Doctor_pk");
        builder.Property(e => e.IdDoctor).UseIdentityColumn();

        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
    }
}
