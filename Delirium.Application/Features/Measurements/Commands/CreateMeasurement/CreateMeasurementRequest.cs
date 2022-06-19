using MediatR;

namespace Delirium.Application.Features.Measurements.Commands.CreateMeasurement;

public record CreateMeasurementRequest(string Name, string Unit) : IRequest;