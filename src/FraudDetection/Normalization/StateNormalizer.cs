using FraudDetection.Configuration;

namespace FraudDetection.Normalization;

/// <summary>"Illinois" → "il". Unknown states are returned cleaned but unchanged.</summary>
public sealed class StateNormalizer : IStateNormalizer
{
    private readonly IReadOnlyDictionary<string, string> _stateCodes;

    public StateNormalizer(StateCodes stateCodes)
    {
        _stateCodes = stateCodes.Values;
    }

    public string Normalize(string value)
    {
        string state = TextHelper.Clean(value).Replace(".", "");

        return _stateCodes.TryGetValue(state, out string? code) ? code : state;
    }
}
