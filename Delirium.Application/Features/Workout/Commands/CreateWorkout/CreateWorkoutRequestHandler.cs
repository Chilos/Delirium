using Delirium.Domain;
using Delirium.Persistence;
using MediatR;

namespace Delirium.Application.Features.Workout.Commands.CreateWorkout;

public sealed class CreateWorkoutRequestHandler : IRequestHandler<CreateWorkoutRequest>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public CreateWorkoutRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<Unit> Handle(CreateWorkoutRequest request, CancellationToken cancellationToken)
    {
        _deliriumDbContext.Workouts.Add(new Domain.Workout
        {
            Date = request.Date,
            State = WorkoutState.Planned,
            UserId = request.UserId
        });
        await _deliriumDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}