using MediatR;

namespace Delirium.Application.Features.Workout.Commands.AddExercise;

public sealed record AddExerciseRequest(Guid WorkoutId, Guid ExerciseId) : IRequest;