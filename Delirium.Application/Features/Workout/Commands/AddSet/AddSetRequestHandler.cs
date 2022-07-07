using Delirium.Domain;
using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.Workout.Commands.AddSet;

public sealed class AddSetRequestHandler : IRequestHandler<AddSetRequest>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public AddSetRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<Unit> Handle(AddSetRequest request, CancellationToken cancellationToken)
    {
        var workout = await _deliriumDbContext.Workouts
            .FirstOrDefaultAsync(w => w.Id == request.WorkoutId, cancellationToken);
        var exercise = await _deliriumDbContext.ExerciseTemplates
            .FirstOrDefaultAsync(e => e.Id == request.ExerciseId, cancellationToken);
        var measurements = await _deliriumDbContext.Measurements
            .ToListAsync(cancellationToken);

        var measurementDict = measurements.ToDictionary(k => k.Id);
        
        if (workout is null || exercise is null)
        {
            return Unit.Value;
        }

        workout.State = WorkoutState.InProgress;
        workout.Sets.Add(new Set
        {
            Exercise = exercise,
            Values = request.Values
                .Select(v => new MeasurementValue
                {
                    Measurement = measurementDict[v.MeasurementId],
                    Value = v.Value
                })
                .ToList()
        });
        await _deliriumDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}