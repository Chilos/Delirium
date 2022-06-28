namespace Delirium.Domain;

public sealed class ExerciseTemplate
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public List<Tag> Tags { get; init; }
    public List<string> ImageUrls { get; init; }
    public int DefaultSetsCount { get; init; }
    public List<Measurement> Parameters { get; init; }
    public List<Workout> Workouts { get; set; }
    public List<Set> Sets { get; set; }
}