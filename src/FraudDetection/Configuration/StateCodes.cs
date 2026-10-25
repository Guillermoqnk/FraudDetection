namespace FraudDetection.Configuration;

/// <summary>Full state names mapped to their two-letter code.</summary>
public sealed class StateCodes
{
    public IReadOnlyDictionary<string, string> Values { get; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["illinois"] = "il",
            ["california"] = "ca",
            ["new york"] = "ny",
        };
}
