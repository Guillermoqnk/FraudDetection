using FraudDetection.Domain;

namespace FraudDetection.Rules;

/// <summary>
/// A fraud rule returns the ids of the orders it considers fraudulent.
/// Rules keep no state between calls, so a single instance can be reused.
/// </summary>
public interface IFraudRule
{
    IEnumerable<long> FindFraudulentOrderIds(IReadOnlyCollection<NormalizedOrder> orders);
}
