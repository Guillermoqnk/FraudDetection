using FraudDetection.Configuration;
using FraudDetection.Normalization;
using Xunit;

namespace FraudDetection.Tests;

public class NormalizerTests
{
    [Theory]
    [InlineData("bugs@bunny.com", "bugs@bunny.com")]
    [InlineData("BuGS@BuNNy.COM", "bugs@bunny.com")]
    [InlineData("bugs.1@bunny.com", "bugs1@bunny.com")]
    [InlineData("bugs+10@bunny.com", "bugs@bunny.com")]
    public void EmailNormalizer_HandlesCaseDotsAndPlus(string input, string expected)
    {
        var normalizer = new EmailNormalizer();

        Assert.Equal(expected, normalizer.Normalize(input));
    }

    [Theory]
    [InlineData("123 Sesame St.", "123 sesame st")]
    [InlineData("123 SeSAME Street", "123 sesame st")]
    [InlineData("45 Abbey Road", "45 abbey rd")]
    [InlineData("45 Abbey Rd.", "45 abbey rd")]
    public void StreetNormalizer_ReplacesAbbreviations(string input, string expected)
    {
        var normalizer = new StreetNormalizer(new StreetAbbreviations());

        Assert.Equal(expected, normalizer.Normalize(input));
    }

    [Theory]
    [InlineData("IL", "il")]
    [InlineData("Illinois", "il")]
    [InlineData("New York", "ny")]
    [InlineData("TX", "tx")]
    public void StateNormalizer_MapsFullNamesToCodes(string input, string expected)
    {
        var normalizer = new StateNormalizer(new StateCodes());

        Assert.Equal(expected, normalizer.Normalize(input));
    }

    [Theory]
    [InlineData("1234-5678 9012", "123456789012")]
    [InlineData("123456789012", "123456789012")]
    public void CreditCardNormalizer_KeepsOnlyDigits(string input, string expected)
    {
        var normalizer = new CreditCardNormalizer();

        Assert.Equal(expected, normalizer.Normalize(input));
    }
}
