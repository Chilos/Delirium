using MediatR;

namespace Delirium.Application.Features.ExerciseTemplate.Commands.CreateExerciseTemplate;

public record CreateExerciseTemplateRequest(
    string Title,
    string Description,
    List<Guid> TagIds,
    List<string> ImageUrls,
    int DefaultSetsCount,
    List<long> MeasurementIds) : IRequest;