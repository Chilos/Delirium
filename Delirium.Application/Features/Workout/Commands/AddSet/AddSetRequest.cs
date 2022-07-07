using MediatR;

namespace Delirium.Application.Features.Workout.Commands.AddSet;

public sealed record AddSetRequest(Guid WorkoutId,
    Guid ExerciseId,
    IReadOnlyList<(long MeasurementId, double Value)> Values) : IRequest;