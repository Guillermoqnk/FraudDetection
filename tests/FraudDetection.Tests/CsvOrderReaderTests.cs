using FraudDetection.Input;
using Xunit;

namespace FraudDetection.Tests;

public class CsvOrderReaderTests
{
    private readonly CsvOrderReader _reader = new();

    [Fact]
    public void ReadsAllFields()
    {
        var input = new StringReader("1\n7,3,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345\n");

        var order = Assert.Single(_reader.ReadOrders(input));

        Assert.Equal(7, order.OrderId);
        Assert.Equal(3, order.DealId);
        Assert.Equal("bugs@bunny.com", order.Email);
        Assert.Equal("123 Sesame St.", order.Address.Street);
        Assert.Equal("New York", order.Address.City);
        Assert.Equal("NY", order.Address.State);
        Assert.Equal("10011", order.Address.Zip);
        Assert.Equal("12345", order.CreditCard);
    }

    [Fact]
    public void HeaderWithByteOrderMark_IsAccepted()
    {
        var input = new StringReader("﻿1\n7,3,bugs@bunny.com,123 Sesame St.,New York,NY,10011,12345\n");

        var order = Assert.Single(_reader.ReadOrders(input));

        Assert.Equal(7, order.OrderId);
    }

    [Fact]
    public void InvalidHeader_Throws()
    {
        var input = new StringReader("abc\n");

        Assert.Throws<FormatException>(() => _reader.ReadOrders(input).ToList());
    }

    [Fact]
    public void WrongNumberOfFields_Throws()
    {
        var input = new StringReader("1\n1,1,bugs@bunny.com\n");

        Assert.Throws<FormatException>(() => _reader.ReadOrders(input).ToList());
    }
}
