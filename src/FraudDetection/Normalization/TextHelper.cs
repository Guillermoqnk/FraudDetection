namespace FraudDetection.Normalization;

/// <summary>Basic text cleanup shared by the normalizers.</summary>
internal static class TextHelper
{
    /// <summary>Lowercase, trimmed, with inner spaces collapsed: "  New   YORK " → "new york".</summary>
    public static string Clean(string value)
    {
        string[] words = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return string.Join(' ', words).ToLowerInvariant();
    }
}
