using FraudDetection.Domain;

namespace FraudDetection.Normalization;

// Each field has its own interface so the DI container can tell the normalizers apart
// (several of them work on strings and would otherwise share INormalizer<string>).

public interface ITextNormalizer : INormalizer<string>
{
}

public interface IEmailNormalizer : INormalizer<string>
{
}

public interface IStreetNormalizer : INormalizer<string>
{
}

public interface IStateNormalizer : INormalizer<string>
{
}

public interface ICreditCardNormalizer : INormalizer<string>
{
}

public interface IAddressNormalizer : INormalizer<Address>
{
}
