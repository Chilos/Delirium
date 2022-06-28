namespace Delirium.Domain;

public sealed class MeasurementValue
{
    public Guid Id { get; set; }
    public Measurement Measurement { get; set; }
    public double Value { get; set; }
    public Set Set { get; set; }
}