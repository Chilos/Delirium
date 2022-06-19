using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.ExerciseTemplate.Commands.CreateExerciseTemplate;

public sealed class CreateExerciseTemplateRequestHandler : IRequestHandler<CreateExerciseTemplateRequest>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public CreateExerciseTemplateRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<Unit> Handle(CreateExerciseTemplateRequest request, CancellationToken cancellationToken)
    {
        var tags = await _deliriumDbContext.Tags
            .Where(t => request.TagIds.Contains(t.Id))
            .ToListAsync(cancellationToken);
        var measurements = await _deliriumDbContext.Measurements
            .Where(m => request.MeasurementIds.Contains(m.Id))
            .ToListAsync(cancellationToken);

        await _deliriumDbContext.ExerciseTemplates.AddAsync(new Domain.ExerciseTemplate
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            DefaultSetsCount = request.DefaultSetsCount,
            ImageUrls = request.ImageUrls,
            Tags = tags,
            Parameters = measurements
        }, cancellationToken);
        await _deliriumDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}