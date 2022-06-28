using MediatR;

namespace Delirium.Application.Features.Workout.Queries.GetWorkoutsByDate;

public sealed record GetWorkoutsByDateRequest(long UserId, DateOnly Date) 
    : IRequest<IReadOnlyList<Domain.Workout>>;