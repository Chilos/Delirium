using Delirium.Domain;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Persistence
{
    public interface IDeliriumDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ExerciseTemplate> ExerciseTemplates { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<MeasurementValue> MeasurementValues { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Set> Sets { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}