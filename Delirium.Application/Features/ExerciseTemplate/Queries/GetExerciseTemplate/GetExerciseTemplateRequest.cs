using MediatR;

namespace Delirium.Application.Features.ExerciseTemplate.Queries.GetExerciseTemplate;

public sealed record GetExerciseTemplateRequest(IReadOnlyList<Guid> Ids, IReadOnlyList<Guid> TagIds) 
    : IRequest<IReadOnlyList<Domain.ExerciseTemplate>>;