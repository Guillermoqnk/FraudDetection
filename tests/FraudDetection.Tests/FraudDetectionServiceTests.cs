using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FraudDetection.Tests;

public class FraudDetectionServiceTests
{
    [Fact]
    public void Run_WritesFraudulentIdsOnOneLine()
    {
        ServiceProvider services = new ServiceCollection()
            .AddFraudDetection()
            .BuildServiceProvider();
        var service = services.GetRequiredService<FraudDetectionService>();

        var input = new StringReader(
            "3\n" +
            "1,1,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010\n" +
            "2,1,elmer@fudd.com,123 Sesame St.,New York,NY,10011,10987654321\n" +
            "3,2,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345689010\n");
        var output = new StringWriter();

        service.Run(input, output);

        Assert.Equal("1,2", output.ToString().Trim());
    }
}
