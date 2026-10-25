using FraudDetection.Domain;

namespace FraudDetection.Rules;

/// <summary>Same email and deal, different credit card (address does not matter).</summary>
public sealed class SameEmailAndDealRule : SameKeyDifferentCardRule
{
    protected override string BuildKey(NormalizedOrder order)
    {
        return $"{order.DealId}|{order.Email}";
    }
}
