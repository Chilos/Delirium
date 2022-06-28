using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.Workout.Commands.AddExercise;

public sealed class AddExerciseRequestHandler : IRequestHandler<AddExerciseRequest>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public AddExerciseRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<Unit> Handle(AddExerciseRequest request, CancellationToken cancellationToken)
    {
        var addedExercise = await _deliriumDbContext.ExerciseTemplates
                .FirstOrDefaultAsync(e => e.Id == request.ExerciseId, cancellationToken);
        var workout = await _deliriumDbContext.Workouts
            .FirstOrDefaultAsync(w => w.Id == request.WorkoutId, cancellationToken);

        if (workout is null || addedExercise is null)
        {
            return Unit.Value;
        }
        
        workout.Exercises.Add(addedExercise);
        await _deliriumDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}