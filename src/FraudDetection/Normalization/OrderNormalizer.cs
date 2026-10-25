using FraudDetection.Domain;

namespace FraudDetection.Normalization;

public sealed class OrderNormalizer : IOrderNormalizer
{
    private readonly IEmailNormalizer _emailNormalizer;
    private readonly IAddressNormalizer _addressNormalizer;
    private readonly ICreditCardNormalizer _creditCardNormalizer;

    public OrderNormalizer(
        IEmailNormalizer emailNormalizer,
        IAddressNormalizer addressNormalizer,
        ICreditCardNormalizer creditCardNormalizer)
    {
        _emailNormalizer = emailNormalizer;
        _addressNormalizer = addressNormalizer;
        _creditCardNormalizer = creditCardNormalizer;
    }

    public NormalizedOrder Normalize(Order order)
    {
        return new NormalizedOrder(
            order.OrderId,
            order.DealId,
            _emailNormalizer.Normalize(order.Email),
            _addressNormalizer.Normalize(order.Address),
            _creditCardNormalizer.Normalize(order.CreditCard));
    }
}
