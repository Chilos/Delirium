namespace Delirium.Domain;

public sealed record Measurement(string Name, string Unit)
{
    public long Id { get; set; }
    public List<ExerciseTemplate> ExerciseTemplates { get; set; }
    public List<MeasurementValue> Values { get; set; }
}