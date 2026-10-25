using FraudDetection.Detection;
using FraudDetection.Input;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FraudDetection.Tests;

public class FraudDetectorTests
{
    private readonly IOrderReader _reader;
    private readonly IFraudDetector _detector;

    public FraudDetectorTests()
    {
        ServiceProvider services = new ServiceCollection()
            .AddFraudDetection()
            .BuildServiceProvider();

        _reader = services.GetRequiredService<IOrderReader>();
        _detector = services.GetRequiredService<IFraudDetector>();
    }

    private IReadOnlyList<long> Detect(params string[] records)
    {
        string input = records.Length + Environment.NewLine + string.Join(Environment.NewLine, records);

        return _detector.Detect(_reader.ReadOrders(new StringReader(input)));
    }

    [Fact]
    public void SampleFromStatement()
    {
        var result = Detect(
            "1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010",
            "2,1,elmer@fudd.com,123 Sesame St.,New York,NY,10011,10987654321",
            "3,2,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010");

        Assert.Equal(new long[] { 1, 2 }, result);
    }

    [Fact]
    public void SameEmailWithTricks_DifferentCard_IsFraud()
    {
        var result = Detect(
            "1,7,bugs@bunny.com,1 A St.,Chicago,IL,60601,111",
            "2,7,BuGS.@BuNNy.COM,2 B Rd.,Los Angeles,CA,90001,222",
            "3,7,b.u.g.s+promo@bunny.com,3 C St.,New York,NY,10011,333");

        Assert.Equal(new long[] { 1, 2, 3 }, result);
    }

    [Fact]
    public void SameAddressWithTricks_DifferentCard_IsFraud()
    {
        var result = Detect(
            "1,5,a@x.com,123 Sesame St.,Chicago,IL,60601,111",
            "2,5,b@x.com,123 SeSAME Street,CHICAGO,Illinois,60601,222");

        Assert.Equal(new long[] { 1, 2 }, result);
    }

    [Fact]
    public void SameCardOrDifferentDeal_IsNotFraud()
    {
        var result = Detect(
            "1,5,a@x.com,10 Main Road,Chicago,IL,60601,1111",
            "2,5,a+x@x.com,10 Main Rd.,Chicago,IL,60601,1111",
            "3,6,a@x.com,10 Main Rd.,Chicago,IL,60601,2222");

        Assert.Empty(result);
    }

    [Fact]
    public void Output_IsSortedAndWithoutDuplicates()
    {
        // Both rules flag these orders, but each id must appear only once.
        var result = Detect(
            "10,1,a@x.com,1 A St.,Chicago,IL,60601,111",
            "2,1,a@x.com,1 A St.,Chicago,IL,60601,222");

        Assert.Equal(new long[] { 2, 10 }, result);
    }

    [Fact]
    public void Detector_CanBeReused_WithoutMixingBatches()
    {
        var first = Detect(
            "1,1,a@x.com,1 A St.,Chicago,IL,60601,111",
            "2,1,a@x.com,1 A St.,Chicago,IL,60601,222");

        var second = Detect(
            "3,1,a@x.com,1 A St.,Chicago,IL,60601,333");

        Assert.Equal(new long[] { 1, 2 }, first);
        Assert.Empty(second);
    }
}
