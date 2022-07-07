using Delirium.Application.Features.Workout.Commands.AddSet;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
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

    public override async Task<CreateWorkoutResponse> CreateWorkout(CreateWorkoutRequest request, ServerCallContext context)
    {
        var innerRequest =
            new Application.Features.Workout.Commands.CreateWorkout.CreateWorkoutRequest(request.UserId,
                DateOnly.FromDateTime(request.Date.ToDateTime()));
        await _mediator.Send(innerRequest, context.CancellationToken);

        return new CreateWorkoutResponse();
    }

    public override async Task<AddExerciseToWorkoutResponse> AddExerciseToWorkout(AddExerciseToWorkoutRequest request, ServerCallContext context)
    {
        var innerRequest =
            new Application.Features.Workout.Commands.AddExercise
                .AddExerciseRequest(Guid.Parse(request.WorkoutId), Guid.Parse(request.ExerciseId));
        await _mediator.Send(innerRequest, context.CancellationToken);

        return new AddExerciseToWorkoutResponse();
    }

    public override async Task<ChangeExerciseFromWorkoutResponse> ChangeExerciseFromWorkout(
        ChangeExerciseFromWorkoutRequest request,
        ServerCallContext context)
    {
        var innerRequest =
            new Application.Features.Workout.Commands.ChangeExercise
                .ChangeExerciseRequest(Guid.Parse(request.WorkoutId),
                    Guid.Parse(request.FromId), Guid.Parse(request.ToId));
        await _mediator.Send(innerRequest, context.CancellationToken);

        return new ChangeExerciseFromWorkoutResponse();
    }

    public override async Task<RemoveExerciseFromWorkoutResponse> RemoveExerciseFromWorkout(
        RemoveExerciseFromWorkoutRequest request,
        ServerCallContext context)
    {
        var innerRequest =
            new Application.Features.Workout.Commands.RemoveExercise
                .RemoveExerciseRequest(Guid.Parse(request.WorkoutId), Guid.Parse(request.ExerciseId));
        await _mediator.Send(innerRequest, context.CancellationToken);

        return new RemoveExerciseFromWorkoutResponse();
    }

    public override async Task<GetWorkoutsByDateResponse> GetWorkoutsByDate(GetWorkoutsByDateRequest request, ServerCallContext context)
    {
        var innerRequest = new Delirium.Application.Features.Workout.Queries.GetWorkoutsByDate
            .GetWorkoutsByDateRequest(request.UserId, DateOnly.FromDateTime(request.Date.ToDateTime()));
        var workouts = await _mediator.Send(innerRequest, context.CancellationToken);
        return new GetWorkoutsByDateResponse
        {
            Workouts = { workouts.Select(w => new Workout
            {
                Date = Timestamp.FromDateTime(w.Date.ToDateTime(new TimeOnly(0,0), DateTimeKind.Utc)),
                Id = w.Id.ToString(),
                State = (int)w.State,
                UserId = w.UserId,
                Exercises = { w.Exercises.Select(e => new ExerciseTemplate
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Description = e.Description,
                    DefaultSetsCount = e.DefaultSetsCount,
                    ImageUrls = { e.ImageUrls },
                    Tags = { e.Tags.Select(t => new Tag{Id = t.Id.ToString(), Name = t.Name}) },
                    Measurement = { e.Parameters.Select(m => new Measurement{Id = m.Id, Name = m.Name, Unit = m.Unit}) }
                }) },
                Sets = { w.Sets.Select(s => new Set
                {
                    
                    Id = s.Id.ToString(),
                    Exercise = new ExerciseTemplate
                    {
                        Id = s.Exercise.Id.ToString(),
                        Title = s.Exercise.Title,
                        Description = s.Exercise.Description,
                        DefaultSetsCount = s.Exercise.DefaultSetsCount,
                        ImageUrls = { s.Exercise.ImageUrls },
                        Tags = { s.Exercise.Tags.Select(t => new Tag{Id = t.Id.ToString(), Name = t.Name}) },
                        Measurement = { s.Exercise.Parameters.Select(m => new Measurement{Id = m.Id, Name = m.Name, Unit = m.Unit}) }
                    },
                    Values = { s.Values.Select(v => new MeasurementValue
                    {
                        Id = v.Id.ToString(),
                        Measurement = new Measurement{Id = v.Measurement.Id, Name = v.Measurement.Name, Unit = v.Measurement.Unit},
                        Value = v.Value
                    }) }
                }) }
            }) }
        };
    }

    public override async Task<GetWorkoutByIdResponse> GetWorkoutById(GetWorkoutByIdRequest request, ServerCallContext context)
    {
        var innerRequest = new Delirium.Application.Features.Workout.Queries.GetWorkoutById
            .GetWorkoutByIdRequest(Guid.Parse(request.Id));
        var workout = await _mediator.Send(innerRequest, context.CancellationToken);

        return new GetWorkoutByIdResponse
        {
            Workout = new Workout
            {
                Date = Timestamp.FromDateTime(workout.Date.ToDateTime(new TimeOnly(0,0), DateTimeKind.Utc)),
                Id = workout.Id.ToString(),
                State = (int)workout.State,
                UserId = workout.UserId,
                Exercises = { workout.Exercises.Select(e => new ExerciseTemplate
                {
                    Id = e.Id.ToString(),
                    Title = e.Title,
                    Description = e.Description,
                    DefaultSetsCount = e.DefaultSetsCount,
                    ImageUrls = { e.ImageUrls },
                    Tags = { e.Tags.Select(t => new Tag{Id = t.Id.ToString(), Name = t.Name}) },
                    Measurement = { e.Parameters.Select(m => new Measurement{Id = m.Id, Name = m.Name, Unit = m.Unit}) }
                }) },
                Sets = { workout.Sets.Select(s => new Set
                {
                    Id = s.Id.ToString(),
                    Exercise = new ExerciseTemplate
                    {
                        Id = s.Exercise.Id.ToString(),
                        Title = s.Exercise.Title,
                        Description = s.Exercise.Description,
                        DefaultSetsCount = s.Exercise.DefaultSetsCount,
                        ImageUrls = { s.Exercise.ImageUrls },
                        Tags = { s.Exercise.Tags.Select(t => new Tag{Id = t.Id.ToString(), Name = t.Name}) },
                        Measurement = { s.Exercise.Parameters.Select(m => new Measurement{Id = m.Id, Name = m.Name, Unit = m.Unit}) }
                    },
                    Values = { s.Values.Select(v => new MeasurementValue
                    {
                        Id = v.Id.ToString(),
                        Measurement = new Measurement{Id = v.Measurement.Id, Name = v.Measurement.Name, Unit = v.Measurement.Unit},
                        Value = v.Value
                    }) }
                }) }
            }
        };
    }

    public override async Task<AddSetToWorkoutResponse> AddSetToWorkout(AddSetToWorkoutRequest request, ServerCallContext context)
    {
        var innerRequest = new AddSetRequest(Guid.Parse(request.WorkoutId),
            Guid.Parse(request.ExerciseId),
            request.Values.Select(v => (v.MeasurementId, v.Value)).ToList());
        await _mediator.Send(innerRequest, context.CancellationToken);

        return new AddSetToWorkoutResponse();
    }
}