namespace Delirium.Domain;

public sealed record Tag(Guid Id, string Name)
{
    public List<ExerciseTemplate> ExerciseTemplates { get; set; }
}