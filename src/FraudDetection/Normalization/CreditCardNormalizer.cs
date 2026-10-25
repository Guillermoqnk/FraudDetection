namespace FraudDetection.Normalization;

/// <summary>Keeps only digits, so "1234-5678 9012" and "123456789012" are the same card.</summary>
public sealed class CreditCardNormalizer : ICreditCardNormalizer
{
    public string Normalize(string value)
    {
        return new string(value.Where(char.IsDigit).ToArray());
    }
}
