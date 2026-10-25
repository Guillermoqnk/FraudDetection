using FraudDetection.Domain;

namespace FraudDetection.Normalization;

/// <summary>Normalizes an address by applying the right normalizer to each of its fields.</summary>
public sealed class AddressNormalizer : IAddressNormalizer
{
    private readonly IStreetNormalizer _streetNormalizer;
    private readonly IStateNormalizer _stateNormalizer;
    private readonly ITextNormalizer _textNormalizer;

    public AddressNormalizer(
        IStreetNormalizer streetNormalizer,
        IStateNormalizer stateNormalizer,
        ITextNormalizer textNormalizer)
    {
        _streetNormalizer = streetNormalizer;
        _stateNormalizer = stateNormalizer;
        _textNormalizer = textNormalizer;
    }

    public Address Normalize(Address address)
    {
        return new Address(
            _streetNormalizer.Normalize(address.Street),
            _textNormalizer.Normalize(address.City),
            _stateNormalizer.Normalize(address.State),
            _textNormalizer.Normalize(address.Zip));
    }
}
