using System.Collections.Generic;
using BunAndBoard;
using FluentAssertions;
using Xunit;

namespace BunAndBoard.Tests;

public class TillTests
{
    private readonly Till _till = new();

    [Fact]
    public void Empty_Order_Charges_Nothing_And_Earns_No_Points()
    {
        var receipt = _till.Checkout(new List<LineItem>(), member: false);
        receipt.Total.Should().Be(0.00m);
        receipt.LoyaltyPoints.Should().Be(0);
    }

    [Fact]
    public void Small_Order_Has_Delivery_Fee_Added()
    {
        var receipt = _till.Checkout(
            new List<LineItem> { new("Croissant", 3, 2.00m) },
            member: false);
        receipt.Total.Should().Be(9.50m);
        receipt.LoyaltyPoints.Should().Be(6);
    }

    [Fact]
    public void Bulk_Discount_Applied_When_Buying_Plenty_Of_An_Item()
    {
        var receipt = _till.Checkout(
            new List<LineItem> { new("Roll", 20, 1.00m) },
            member: false);
        receipt.Total.Should().Be(21.50m);
        receipt.LoyaltyPoints.Should().Be(18);
    }

    [Fact]
    public void No_Bulk_Discount_For_A_Modest_Quantity()
    {
        var receipt = _till.Checkout(
            new List<LineItem> { new("Roll", 8, 1.00m) },
            member: false);
        receipt.Total.Should().Be(11.50m);
        receipt.LoyaltyPoints.Should().Be(8);
    }

    [Fact]
    public void Members_Get_An_Extra_Discount_Off_The_Whole_Order()
    {
        var receipt = _till.Checkout(
            new List<LineItem> { new("Cake", 1, 20.00m) },
            member: true);
        receipt.Total.Should().Be(22.50m);
    }

    [Fact]
    public void Large_Order_Ships_For_Free()
    {
        var receipt = _till.Checkout(
            new List<LineItem> { new("Cake", 2, 15.00m) },
            member: false);
        receipt.Total.Should().Be(30.00m);
        receipt.LoyaltyPoints.Should().Be(30);
    }

    [Fact]
    public void Multiple_Lines_Are_Summed_With_Bulk_Discount_Per_Line()
    {
        var receipt = _till.Checkout(
            new List<LineItem>
            {
                new("Croissant", 2, 2.00m),
                new("Roll", 12, 1.00m),
            },
            member: false);
        receipt.Total.Should().Be(18.30m);
        receipt.LoyaltyPoints.Should().Be(14);
    }
}
