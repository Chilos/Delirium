using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.Workout.Commands.ChangeExercise;

public sealed class ChangeExerciseRequestHandler : IRequestHandler<ChangeExerciseRequest>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public ChangeExerciseRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<Unit> Handle(ChangeExerciseRequest request, CancellationToken cancellationToken)
    {
        var workout = await _deliriumDbContext.Workouts
            .FirstOrDefaultAsync(w => w.Id == request.WorkoutId, cancellationToken);
        var toExercise =
            await _deliriumDbContext.ExerciseTemplates
                .FirstOrDefaultAsync(e => e.Id == request.ToId, cancellationToken);
        if (workout is null || toExercise is null)
        {
            return Unit.Value;
        }
        var fromExercise = workout.Exercises.FirstOrDefault(e => e.Id == request.FromId);
        if (fromExercise is null)
        {
            return Unit.Value;
        }
        workout.Exercises.Remove(fromExercise);
        workout.Exercises.Add(toExercise);
        await _deliriumDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}