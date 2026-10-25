using FraudDetection.Configuration;

namespace FraudDetection.Normalization;

/// <summary>
/// "123 Sesame Street" → "123 sesame st".
/// Replaces whole words using the configured abbreviations; trailing dots are ignored.
/// </summary>
public sealed class StreetNormalizer : IStreetNormalizer
{
    private readonly IReadOnlyDictionary<string, string> _abbreviations;

    public StreetNormalizer(StreetAbbreviations abbreviations)
    {
        _abbreviations = abbreviations.Values;
    }

    public string Normalize(string value)
    {
        IEnumerable<string> words = TextHelper.Clean(value)
            .Split(' ')
            .Select(word => word.TrimEnd('.', ','))
            .Select(word => _abbreviations.TryGetValue(word, out string? abbreviation) ? abbreviation : word);

        return string.Join(' ', words);
    }
}
