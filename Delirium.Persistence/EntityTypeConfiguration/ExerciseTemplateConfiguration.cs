using Delirium.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delirium.Persistence.EntityTypeConfiguration
{
    public sealed class ExerciseTemplateConfiguration: IEntityTypeConfiguration<ExerciseTemplate>
    {
        public void Configure(EntityTypeBuilder<ExerciseTemplate> builder)
        {
            builder.HasKey(exercise => exercise.Id);
            builder.HasMany(e => e.Parameters).WithMany(p => p.ExerciseTemplates);
            builder.HasMany(e => e.Tags).WithMany(p => p.ExerciseTemplates);
            builder.HasIndex(exercise => exercise.Id).IsUnique();
        }
    }
}