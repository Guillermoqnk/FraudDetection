using FraudDetection;
using Microsoft.Extensions.DependencyInjection;

// Usage:
//   dotnet run --project src/FraudDetection -- sample.txt
//   dotnet run --project src/FraudDetection < sample.txt

using ServiceProvider serviceProvider = new ServiceCollection()
    .AddFraudDetection()
    .BuildServiceProvider();

var fraudDetectionService = serviceProvider.GetRequiredService<FraudDetectionService>();

try
{
    using TextReader input = args.Length > 0 ? new StreamReader(args[0]) : Console.In;

    fraudDetectionService.Run(input, Console.Out);

    return 0;
}
catch (Exception ex) when (ex is FormatException || ex is IOException || ex is UnauthorizedAccessException)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    return 1;
}
