namespace Delirium.Domain;

public sealed class Set
{
    public Guid Id { get; set; }
    public ExerciseTemplate Exercise { get; set; }
    public Workout Workout { get; set; }
    public List<MeasurementValue> Values { get; set; }

}