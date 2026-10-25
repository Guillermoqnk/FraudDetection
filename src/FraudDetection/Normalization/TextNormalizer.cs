namespace FraudDetection.Normalization;

/// <summary>Only applies the basic cleanup (casing and spaces). Used for city and zip.</summary>
public sealed class TextNormalizer : ITextNormalizer
{
    public string Normalize(string value)
    {
        return TextHelper.Clean(value);
    }
}
