using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.Workout.Queries.GetWorkoutsByDate;

public sealed class GetWorkoutsByDateRequestHandler : IRequestHandler<GetWorkoutsByDateRequest, IReadOnlyList<Domain.Workout>>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public GetWorkoutsByDateRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<IReadOnlyList<Domain.Workout>> Handle(GetWorkoutsByDateRequest request, CancellationToken cancellationToken)
    {
        return await _deliriumDbContext.Workouts
            .Include(w => w.Exercises)
            .ThenInclude(e => e.Parameters)
            .Include(w => w.Exercises)
            .ThenInclude(e => e.Tags)
            .Include(w => w.Sets)
            .ThenInclude(s => s.Values)
            .Where(workout => workout.Date == request.Date && workout.UserId == request.UserId)
            .ToListAsync(cancellationToken);
    }
}