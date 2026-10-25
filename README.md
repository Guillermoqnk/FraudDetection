# Fraud detection

Detects orders where the same user tries to buy the same deal more than once with different credit cards.

## Running

Requires the .NET 8 SDK.

```bash
dotnet run --project src/FraudDetection -- sample.txt    # prints 1,2
dotnet test
```

## Design

The program is split into small pieces, each with a single responsibility:

- `Input` reads the orders (`IOrderReader`, `CsvOrderReader`).
- `Normalization` puts every field in a canonical form (email, street, state, card...).
  Each field has its own normalizer and interface (`IEmailNormalizer`, `IStreetNormalizer`...),
  all based on `INormalizer<T>`, and `OrderNormalizer` combines them. Having one interface per
  field lets the DI container resolve every normalizer by type, with nothing built by hand.
- `Configuration` holds the data used by the normalizers (`StreetAbbreviations`, `StateCodes`).
- `Rules` contains the fraud rules (`IFraudRule`). Both current rules follow the same
  pattern, "same key and deal, different card", so they share `SameKeyDifferentCardRule`
  and only define how the key is built.
- `Detection` normalizes the orders and runs every rule over them (`IFraudDetector`).
- `Output` writes the result (`IFraudReportWriter`).
- `FraudDetectionService` ties reading, detection and writing together.

Dependencies are injected through constructors and registered with
`Microsoft.Extensions.DependencyInjection` in `ServiceCollectionExtensions.AddFraudDetection()`.
`Program.cs` only builds the container and runs the service.

All services are stateless, so they are registered as singletons and can be reused safely
(there is a test for this).

Patterns used: Strategy (normalizers and rules), Template Method (`SameKeyDifferentCardRule`)
and dependency injection.

### Extending it

- New abbreviation or state: add it to `StreetAbbreviations` or `StateCodes`.
- New rule: implement `IFraudRule` (or inherit from `SameKeyDifferentCardRule`) and register it
  in `AddFraudDetection()`. `FraudDetector` receives every registered rule automatically.
- New input or output format: implement `IOrderReader` or `IFraudReportWriter` and register it.

### Performance

The input is read as a stream, each order is normalized once, and each rule groups the
orders by key with a hash-based `GroupBy`. A group is fraudulent when it contains more than
one card, so orders are never compared in pairs. The total cost is O(N).

The normalized orders are kept in memory so that every rule can process them. For inputs
too large to fit in memory, the rules could be changed to process orders one at a time.

## Assumptions

- Records have exactly 8 fields (no commas inside the street).
- Credit cards are compared by their digits only.
- Only Street/St. and Road/Rd., and IL, CA and NY need to be handled, as stated.
