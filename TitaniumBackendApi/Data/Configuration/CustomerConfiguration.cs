using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Models;

namespace TitaniumBackendApi.Data.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomersModel>
    {
        public void Configure(EntityTypeBuilder<CustomersModel> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(c => c.NationalId)
                   .HasMaxLength(12)
                   .IsFixedLength()
                   .IsRequired();

            builder.Property(c => c.PhoneNumber)
                   .HasMaxLength(15)
                   .IsRequired();

            builder.Property(c => c.Email)
                   .HasMaxLength(100);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("SYSUTCDATETIME()");

            // The rule Hassan needed: enforced by the database, not by hope.
            builder.HasIndex(c => c.NationalId)
                   .IsUnique()
                   .HasDatabaseName("UX_Customers_NationalId");


        }
    }



}
