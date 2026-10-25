using FraudDetection.Domain;

namespace FraudDetection.Input;

/// <summary>Reads orders from a text source. Allows supporting other formats in the future.</summary>
public interface IOrderReader
{
    IEnumerable<Order> ReadOrders(TextReader input);
}
