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
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}