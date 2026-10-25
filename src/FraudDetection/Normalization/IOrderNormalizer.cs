using FraudDetection.Domain;

namespace FraudDetection.Normalization;

public interface IOrderNormalizer
{
    NormalizedOrder Normalize(Order order);
}
