using FraudDetection.Detection;
using FraudDetection.Domain;
using FraudDetection.Input;
using FraudDetection.Output;

namespace FraudDetection;

/// <summary>Application entry point: reads the orders, detects the fraudulent ones and writes the result.</summary>
public sealed class FraudDetectionService
{
    private readonly IOrderReader _orderReader;
    private readonly IFraudDetector _fraudDetector;
    private readonly IFraudReportWriter _reportWriter;

    public FraudDetectionService(
        IOrderReader orderReader,
        IFraudDetector fraudDetector,
        IFraudReportWriter reportWriter)
    {
        _orderReader = orderReader;
        _fraudDetector = fraudDetector;
        _reportWriter = reportWriter;
    }

    public void Run(TextReader input, TextWriter output)
    {
        IEnumerable<Order> orders = _orderReader.ReadOrders(input);
        IReadOnlyList<long> fraudulentOrderIds = _fraudDetector.Detect(orders);
        _reportWriter.Write(fraudulentOrderIds, output);
    }
}
