using MediatR;

namespace Delirium.Application.Features.Workout.Commands.RemoveExercise;

public record RemoveExerciseRequest(Guid WorkoutId, Guid ExerciseId): IRequest;