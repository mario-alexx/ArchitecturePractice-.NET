using Cf.Dotnet.Architecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cf.Dotnet.Database.ModelConfigurations;

public class BuyerModelConfiguration : IEntityTypeConfiguration<Buyer>
{
    public void Configure(EntityTypeBuilder<Buyer> builder)
    {
        var buyer = new Buyer(
            1,
            "Test Buyer 1");

        builder.HasData(buyer);

        builder.Property(b => b.Balance).HasColumnType("decimal(18,2)").HasPrecision(18, 2);

        builder.Property<byte[]>("Version")
            .IsRowVersion();
    }
}