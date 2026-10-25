namespace FraudDetection.Normalization;

/// <summary>Turns a value into its canonical form so it can be compared for equality.</summary>
public interface INormalizer<T>
{
    T Normalize(T value);
}
