using FraudDetection.Configuration;
using FraudDetection.Detection;
using FraudDetection.Input;
using FraudDetection.Normalization;
using FraudDetection.Output;
using FraudDetection.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace FraudDetection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers every fraud detection service. None of them keep state between calls,
    /// so they are all registered as singletons.
    /// </summary>
    public static IServiceCollection AddFraudDetection(this IServiceCollection services)
    {
        // Configuration
        services.AddSingleton<StreetAbbreviations>();
        services.AddSingleton<StateCodes>();

        // Normalization
        services.AddSingleton<ITextNormalizer, TextNormalizer>();
        services.AddSingleton<IEmailNormalizer, EmailNormalizer>();
        services.AddSingleton<IStreetNormalizer, StreetNormalizer>();
        services.AddSingleton<IStateNormalizer, StateNormalizer>();
        services.AddSingleton<ICreditCardNormalizer, CreditCardNormalizer>();
        services.AddSingleton<IAddressNormalizer, AddressNormalizer>();
        services.AddSingleton<IOrderNormalizer, OrderNormalizer>();

        // Rules: every registered IFraudRule is injected into FraudDetector as IEnumerable<IFraudRule>.
        services.AddSingleton<IFraudRule, SameEmailAndDealRule>();
        services.AddSingleton<IFraudRule, SameAddressAndDealRule>();

        // Input, detection and output
        services.AddSingleton<IOrderReader, CsvOrderReader>();
        services.AddSingleton<IFraudDetector, FraudDetector>();
        services.AddSingleton<IFraudReportWriter, CommaSeparatedReportWriter>();
        services.AddSingleton<FraudDetectionService>();

        return services;
    }
}
