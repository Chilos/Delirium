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

    public override async Task<CreateMeasurementResponse> CreateMeasurement(CreateMeasurementRequest request, ServerCallContext context)
    {
        var innerRequest = new Delirium.Application.Features.Measurements.Commands.CreateMeasurement.CreateMeasurementRequest(request.Name, request.Unit);
        await _mediator.Send(innerRequest, context.CancellationToken);
        return new CreateMeasurementResponse();
    }

    public override async Task<CreateExerciseTemplateResponse> CreateExerciseTemplate(CreateExerciseTemplateRequest request, ServerCallContext context)
    {
        var innerRequest = new Delirium.Application.Features.ExerciseTemplate.Commands.CreateExerciseTemplate.CreateExerciseTemplateRequest(
            request.Title,
            request.Description,
            request.TagIds.Select(id => Guid.Parse(id)).ToList(),
            request.ImageUrls.ToList(),
            request.DefaultSetsCount,
            request.MeasurementIds.ToList());
        await _mediator.Send(innerRequest, context.CancellationToken);
        
        return new CreateExerciseTemplateResponse();
    }

    public override async Task<GetExerciseTemplatesResponse> GetExerciseTemplates(GetExerciseTemplatesRequest request, ServerCallContext context)
    {
        var innerRequest = new Delirium.Application.Features.ExerciseTemplate.Queries.GetExerciseTemplate.GetExerciseTemplateRequest(request.Ids.Select(i => Guid.Parse(i)).ToList(), request.TagIds.Select(i => Guid.Parse(i)).ToList());
        var exerciseTemplates = await _mediator.Send(innerRequest, context.CancellationToken);

        return new GetExerciseTemplatesResponse
        {
            ExerciseTemplates = { exerciseTemplates.Select(e => new ExerciseTemplate
            {
                Id = e.Id.ToString(),
                Title = e.Title,
                Description = e.Description,
                DefaultSetsCount = e.DefaultSetsCount,
                ImageUrls = { e.ImageUrls },
                Tags = { e.Tags.Select(t => new Tag{Id = t.Id.ToString(), Name = t.Name}) },
                Measurement = { e.Parameters.Select(m => new Measurement{Id = m.Id, Name = m.Name, Unit = m.Unit}) }
            }) }
        };
    }
}