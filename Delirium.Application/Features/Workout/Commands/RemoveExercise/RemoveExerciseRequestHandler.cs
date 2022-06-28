using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.Workout.Commands.RemoveExercise;

public sealed class RemoveExerciseRequestHandler: IRequestHandler<RemoveExerciseRequest>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public RemoveExerciseRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<Unit> Handle(RemoveExerciseRequest request, CancellationToken cancellationToken)
    {
        var workout = await _deliriumDbContext.Workouts
            .FirstOrDefaultAsync(w => w.Id == request.WorkoutId, cancellationToken);
        if (workout is null)
        {
            return Unit.Value;
        }

        var removedExercise = workout.Exercises.FirstOrDefault(e => e.Id == request.ExerciseId);
        if (removedExercise is null)
        {
            return Unit.Value;
        }

        workout.Exercises.Remove(removedExercise);
        await _deliriumDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}