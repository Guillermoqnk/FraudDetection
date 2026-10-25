namespace FraudDetection.Domain;

/// <summary>
/// Order with every field in canonical form. This is the only thing the rules receive,
/// so no rule has to care about casing, abbreviations, etc.
/// </summary>
public sealed record NormalizedOrder(
    long OrderId,
    long DealId,
    string Email,
    Address Address,
    string CreditCard);
