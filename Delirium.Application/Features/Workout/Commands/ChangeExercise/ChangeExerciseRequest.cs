using MediatR;

namespace Delirium.Application.Features.Workout.Commands.ChangeExercise;

public record ChangeExerciseRequest(Guid WorkoutId, Guid FromId, Guid ToId) : IRequest;