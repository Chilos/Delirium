using Delirium.Domain;
using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.Tags.Queries.GetTags;

public class GetTagsRequestHandler: IRequestHandler<GetTagsRequest, IReadOnlyList<Tag>>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public GetTagsRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<IReadOnlyList<Tag>> Handle(GetTagsRequest request, CancellationToken cancellationToken)
    {
        if (request.Names.Count != 0)
        {
            return await _deliriumDbContext.Tags
                .Where(t => request.Names.Contains(t.Name))
                .ToListAsync(cancellationToken);
        }
        return await _deliriumDbContext.Tags
            .ToListAsync(cancellationToken);
    }
}