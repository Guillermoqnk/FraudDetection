namespace FraudDetection.Domain;

/// <summary>Order exactly as it comes in the input, not normalized.</summary>
public sealed record Order(
    long OrderId,
    long DealId,
    string Email,
    Address Address,
    string CreditCard);
