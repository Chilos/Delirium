using MediatR;

namespace Delirium.Application.Features.Tags.Commands.CreateTag;

public sealed record CreateTagsRequest(IReadOnlyList<string> Names) : IRequest;