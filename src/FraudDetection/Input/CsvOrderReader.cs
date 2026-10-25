using FraudDetection.Domain;

namespace FraudDetection.Input;

/// <summary>
/// Reads the input format: a first line with N followed by N comma-separated records.
/// Orders are returned one at a time while the input is being read.
/// </summary>
public sealed class CsvOrderReader : IOrderReader
{
    private const int FieldCount = 8;

    public IEnumerable<Order> ReadOrders(TextReader input)
    {
        string? header = input.ReadLine();
        if (header == null)
        {
            yield break;
        }

        // A UTF-8 byte order mark is not removed when the input comes from the console.
        if (!int.TryParse(header.TrimStart('﻿').Trim(), out int count) || count < 0)
        {
            throw new FormatException($"The first line must be the number of records, but was '{header}'.");
        }

        for (int i = 0; i < count; i++)
        {
            string? line = input.ReadLine();
            if (line == null)
            {
                yield break;
            }

            yield return ParseLine(line, lineNumber: i + 2);
        }
    }

    private static Order ParseLine(string line, int lineNumber)
    {
        string[] fields = line.Split(',');
        if (fields.Length != FieldCount)
        {
            throw new FormatException($"Line {lineNumber}: expected {FieldCount} fields but found {fields.Length}.");
        }

        return new Order(
            OrderId: ParseNumber(fields[0], "order id", lineNumber),
            DealId: ParseNumber(fields[1], "deal id", lineNumber),
            Email: fields[2],
            Address: new Address(
                Street: fields[3],
                City: fields[4],
                State: fields[5],
                Zip: fields[6]),
            CreditCard: fields[7]);
    }

    private static long ParseNumber(string value, string fieldName, int lineNumber)
    {
        if (!long.TryParse(value.Trim(), out long number))
        {
            throw new FormatException($"Line {lineNumber}: {fieldName} '{value}' is not a number.");
        }

        return number;
    }
}
