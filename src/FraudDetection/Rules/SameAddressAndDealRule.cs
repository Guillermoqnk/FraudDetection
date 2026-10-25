using FraudDetection.Domain;

namespace FraudDetection.Rules;

/// <summary>Same address/city/state/zip and deal, different credit card (email does not matter).</summary>
public sealed class SameAddressAndDealRule : SameKeyDifferentCardRule
{
    protected override string BuildKey(NormalizedOrder order)
    {
        Address address = order.Address;
        return $"{order.DealId}|{address.Street}|{address.City}|{address.State}|{address.Zip}";
    }
}
