using Delirium.Application.Features.Tags.Commands.CreateTag;
using MediatR;

namespace Delirium.Modules;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateTagsRequestHandlerRequest));
        return services;
    }
}