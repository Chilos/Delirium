using Delirium.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delirium.Persistence.EntityTypeConfiguration
{
    public sealed class SetConfiguration: IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> builder)
        {
            builder.HasKey(w => w.Id);
            builder.HasOne(s => s.Exercise).WithMany(e => e.Sets);
            builder.HasOne(s => s.Workout).WithMany(w => w.Sets);
            builder.HasMany(s => s.Values).WithOne(w => w.Set);
            builder.HasIndex(s => s.Id).IsUnique();
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
        }
    }
}