using Delirium.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delirium.Persistence.EntityTypeConfiguration
{
    public sealed class MeasurementConfiguration: IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.HasKey(measurement => measurement.Id);
            builder.HasIndex(measurement => measurement.Id).IsUnique();
            builder.Property(measurement => measurement.Id).ValueGeneratedOnAdd();
        }
    }
}