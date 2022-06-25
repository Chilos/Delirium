using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.ExerciseTemplate.Queries.GetExerciseTemplate;

public class GetExerciseTemplateRequestHandler : IRequestHandler<GetExerciseTemplateRequest, IReadOnlyList<Domain.ExerciseTemplate>>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public GetExerciseTemplateRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<IReadOnlyList<Domain.ExerciseTemplate>> Handle(GetExerciseTemplateRequest request, CancellationToken cancellationToken)
    {
        if (request.Ids.Count != 0)
        {
            return await _deliriumDbContext.ExerciseTemplates
                .Where(e => request.Ids.Contains(e.Id))
                .Include(e => e.Parameters)
                .Include(e => e.Tags)
                .ToListAsync(cancellationToken);
        }

        if (request.TagIds.Count != 0)  
        {
            return _deliriumDbContext.ExerciseTemplates
                .Include(e => e.Parameters)
                .Include(e => e.Tags)
                .AsEnumerable()
                .Where(e => request.TagIds.All(id => e.Tags.Exists(t => t.Id == id)))
                .ToList();
        }

        return await _deliriumDbContext.ExerciseTemplates
            .Include(e => e.Parameters)
            .Include(e => e.Tags)
            .ToListAsync(cancellationToken);
    }
}