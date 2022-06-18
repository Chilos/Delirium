using Delirium.Domain;
using Delirium.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Delirium.Application.Features.Tags.Commands.CreateTag;

public class CreateTagsRequestHandlerRequest : IRequestHandler<CreateTagsRequest>
{
    private readonly IDeliriumDbContext _deliriumDbContext;
    
    public CreateTagsRequestHandlerRequest(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }
    
    public async Task<Unit> Handle(CreateTagsRequest request, CancellationToken cancellationToken)
    {
        var excludedTagNames = await _deliriumDbContext.Tags
            .Where(t => request.Names.Contains(t.Name))
            .Select(t => t.Name)
            .ToListAsync(cancellationToken);
        
        foreach (var name in request.Names)
        {
            if (!excludedTagNames.Contains(name))
                _deliriumDbContext.Tags.Add(new Tag(Guid.NewGuid(), name));
        }
        await _deliriumDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}