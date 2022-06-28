using Delirium.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delirium.Persistence.EntityTypeConfiguration
{
    public sealed class WorkoutConfiguration: IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.HasKey(w => w.Id);
            builder.HasMany(w => w.Exercises).WithMany(e => e.Workouts);
            builder.HasMany(w => w.Sets).WithOne(s => s.Workout);
            builder.HasIndex(w => w.Id).IsUnique();
            builder.Property(w => w.Id).ValueGeneratedOnAdd();
        }
    }
}