using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Models;

namespace TitaniumBackendApi.Data.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AccountNumber).IsRequired().HasMaxLength(20);
            builder.Property(a => a.Currency).IsRequired().HasMaxLength(3);
            builder.Property(a => a.OpenedAt).IsRequired();
            builder.Property(a => a.Status).IsRequired();
            builder.HasOne(a => a.Customer)
                .WithMany(c => c.CustomerAccounts)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    
}
