using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreService.Api.Domain.Entities;

namespace StoreService.Api.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        // ========================
        // CreationToken (ValueObject)
        // ========================
        builder.OwnsOne(x => x.CreationToken, token =>
        {
            token.Property(t => t.Value)
                .HasColumnName("CreationToken")
                .IsRequired();
            
            token.HasIndex(t => t.Value)
                .HasDatabaseName("UIX_Invoices_CreationToken")
                .IsUnique();
        });

        // ========================
        // CustomerInfo Snapshot
        // ========================
        builder.OwnsOne(x => x.CustomerSnapshot, customer =>
        {
            customer.Property(x => x.FirstName)
                .HasColumnName("CustomerFirstName")
                .HasMaxLength(100)
                .IsRequired();

            customer.Property(x => x.LastName)
                .HasColumnName("CustomerLastName")
                .HasMaxLength(100)
                .IsRequired();

            customer.Property(x => x.NationalCode)
                .HasColumnName("CustomerNationalCode")
                .HasMaxLength(20)
                .IsRequired();

            customer.HasIndex(x => x.NationalCode);
        });
        
        builder.Ignore(x => x.TotalAmount);
        
        // ========================
        // Items Navigation
        // ========================
        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey("InvoiceId")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(x => x.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}