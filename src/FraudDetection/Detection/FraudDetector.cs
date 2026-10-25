using FraudDetection.Domain;
using FraudDetection.Normalization;
using FraudDetection.Rules;

namespace FraudDetection.Detection;

/// <summary>
/// Normalizes each order once and runs every rule over the normalized orders.
/// It does not know about concrete rules or about input/output formats.
/// </summary>
public sealed class FraudDetector : IFraudDetector
{
    private readonly IOrderNormalizer _normalizer;
    private readonly IReadOnlyList<IFraudRule> _rules;

    public FraudDetector(IOrderNormalizer normalizer, IEnumerable<IFraudRule> rules)
    {
        _normalizer = normalizer;
        _rules = rules.ToList();
    }

    public IReadOnlyList<long> Detect(IEnumerable<Order> orders)
    {
        List<NormalizedOrder> normalizedOrders = orders
            .Select(order => _normalizer.Normalize(order))
            .ToList();

        return _rules
            .SelectMany(rule => rule.FindFraudulentOrderIds(normalizedOrders))
            .Distinct()
            .OrderBy(id => id)
            .ToList();
    }
}
