using Delirium.Domain;
using MediatR;

namespace Delirium.Application.Features.Tags.Queries.GetTags;

public sealed record GetTagsRequest(IReadOnlyList<string> Names) : IRequest<IReadOnlyList<Tag>>;