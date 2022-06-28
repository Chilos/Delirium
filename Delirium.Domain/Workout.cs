namespace Delirium.Domain;

public sealed class Workout
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public DateOnly Date { get; set; }
    public List<ExerciseTemplate> Exercises { get; set; } = new();
    public List<Set> Sets { get; set; } = new();
    public WorkoutState State { get; set; }
}