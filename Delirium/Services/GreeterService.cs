using Grpc.Core;
using Delirium;
using MediatR;

namespace Delirium.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    private readonly IMediator _mediator;

    public GreeterService(ILogger<GreeterService> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public override async Task<CreateTagsResponse> CreateTags(CreateTagsRequest request, ServerCallContext context)
    {
        var innerRequest = new Delirium.Application.Features.Tags.Commands.CreateTag.CreateTagsRequest(request.Name);
        await _mediator.Send(innerRequest, context.CancellationToken);
        return new CreateTagsResponse();
    }

    public override async Task<GetTagsResponse> GetTags(GetTagsRequest request, ServerCallContext context)
    {
        var innerRequest = new Delirium.Application.Features.Tags.Queries.GetTags.GetTagsRequest(request.Filter.Names);
        var tags = await _mediator.Send(innerRequest, context.CancellationToken);

        return new GetTagsResponse
        {
            Tags = {tags.Select(t => new Tag {Id = t.Id.ToString(), Name = t.Name})}
        };
    }
}