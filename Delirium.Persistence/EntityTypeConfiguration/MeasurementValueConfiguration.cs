using Delirium.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delirium.Persistence.EntityTypeConfiguration
{
    public sealed class MeasurementValueConfiguration: IEntityTypeConfiguration<MeasurementValue>
    {
        public void Configure(EntityTypeBuilder<MeasurementValue> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Measurement).WithMany(e => e.Values);
            builder.HasIndex(m => m.Id).IsUnique();
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
        }
    }
}