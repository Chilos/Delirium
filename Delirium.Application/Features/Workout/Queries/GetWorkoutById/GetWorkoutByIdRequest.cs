using MediatR;

namespace Delirium.Application.Features.Workout.Queries.GetWorkoutById;

public sealed record GetWorkoutByIdRequest(Guid Id) : IRequest<Domain.Workout>;