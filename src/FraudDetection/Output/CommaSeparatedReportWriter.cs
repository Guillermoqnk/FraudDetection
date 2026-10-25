namespace FraudDetection.Output;

/// <summary>Writes the ids on a single line, separated by commas.</summary>
public sealed class CommaSeparatedReportWriter : IFraudReportWriter
{
    public void Write(IReadOnlyList<long> fraudulentOrderIds, TextWriter output)
    {
        output.WriteLine(string.Join(",", fraudulentOrderIds));
    }
}
