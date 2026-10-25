using FraudDetection.Domain;

namespace FraudDetection.Detection;

public interface IFraudDetector
{
    /// <summary>Returns the fraudulent order ids, without duplicates and in ascending order.</summary>
    IReadOnlyList<long> Detect(IEnumerable<Order> orders);
}
