namespace FraudDetection.Output;

public interface IFraudReportWriter
{
    void Write(IReadOnlyList<long> fraudulentOrderIds, TextWriter output);
}
