namespace FraudDetection.Domain;

/// <summary>Postal address of an order.</summary>
public sealed record Address(string Street, string City, string State, string Zip);
