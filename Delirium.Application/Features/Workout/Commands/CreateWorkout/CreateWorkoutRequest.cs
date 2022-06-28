using MediatR;

namespace Delirium.Application.Features.Workout.Commands.CreateWorkout;

public sealed record CreateWorkoutRequest(long UserId, DateOnly Date) : IRequest;
