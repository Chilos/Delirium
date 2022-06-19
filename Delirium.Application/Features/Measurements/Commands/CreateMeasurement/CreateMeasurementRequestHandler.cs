using Delirium.Domain;
using Delirium.Persistence;
using MediatR;

namespace Delirium.Application.Features.Measurements.Commands.CreateMeasurement;

public class CreateMeasurementRequestHandler : IRequestHandler<CreateMeasurementRequest>
{
    private readonly IDeliriumDbContext _deliriumDbContext;

    public CreateMeasurementRequestHandler(IDeliriumDbContext deliriumDbContext)
    {
        _deliriumDbContext = deliriumDbContext;
    }

    public async Task<Unit> Handle(CreateMeasurementRequest request, CancellationToken cancellationToken)
    {
        _deliriumDbContext.Measurements.Add(new Measurement(request.Name, request.Unit));
        await _deliriumDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}