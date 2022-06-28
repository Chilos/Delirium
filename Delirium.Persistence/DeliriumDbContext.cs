using Delirium.Domain;
using Delirium.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Persistence
{
    public sealed class DeliriumDbContext: DbContext, IDeliriumDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ExerciseTemplate> ExerciseTemplates { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<MeasurementValue> MeasurementValues { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Set> Sets { get; set; }

        public DeliriumDbContext()
        {
        }
        
        public DeliriumDbContext(DbContextOptions<DeliriumDbContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        { 
            if (!optionsBuilder.IsConfigured) 
            { 
                optionsBuilder.UseNpgsql("Host=localhost; Port=5432; User Id=postgres; Password=postgres; Database=delirium-db; Trust Server Certificate=true"); 
            } 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExerciseTemplateConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MeasurementConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new WorkoutConfiguration());
            modelBuilder.ApplyConfiguration(new MeasurementValueConfiguration());
            modelBuilder.ApplyConfiguration(new SetConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}