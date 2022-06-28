using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.Workout.Queries.GetWorkoutById;

public sealed class GetWorkoutByIdRequestHandler : IRequestHandler<GetWorkoutByIdRequest, Domain.Workout?>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public GetWorkoutByIdRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public Task<Domain.Workout?> Handle(GetWorkoutByIdRequest request, CancellationToken cancellationToken)
    {
        return _deliriumDbContext.Workouts
            .Include(w => w.Exercises)
            .ThenInclude(e => e.Parameters)
            .Include(w => w.Exercises)
            .ThenInclude(e => e.Tags)
            .Include(w => w.Sets)
            .ThenInclude(s => s.Values)
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);
    }
}