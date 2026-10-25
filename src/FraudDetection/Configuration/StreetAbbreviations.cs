namespace FraudDetection.Configuration;

/// <summary>Street words that must be treated as equivalent, mapped to their abbreviation.</summary>
public sealed class StreetAbbreviations
{
    public IReadOnlyDictionary<string, string> Values { get; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["street"] = "st",
            ["road"] = "rd",
        };
}
