using FraudDetection.Domain;

namespace FraudDetection.Rules;

/// <summary>
/// Base class for rules of the form "same X and same deal, but different credit card".
/// Subclasses only decide how the key is built.
///
/// Orders are grouped by key. If a group contains more than one credit card, every order
/// in that group is fraudulent. This avoids comparing every pair of orders, so the cost
/// is O(N) instead of O(N²).
/// </summary>
public abstract class SameKeyDifferentCardRule : IFraudRule
{
    protected abstract string BuildKey(NormalizedOrder order);

    public IEnumerable<long> FindFraudulentOrderIds(IReadOnlyCollection<NormalizedOrder> orders)
    {
        return orders
            .GroupBy(order => BuildKey(order))
            .Where(group => HasDifferentCreditCards(group))
            .SelectMany(group => group.Select(order => order.OrderId));
    }

    private static bool HasDifferentCreditCards(IEnumerable<NormalizedOrder> group)
    {
        return group
            .Select(order => order.CreditCard)
            .Distinct()
            .Count() > 1;
    }
}
