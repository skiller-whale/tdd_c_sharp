using System.Collections.Generic;
using Bakery;
using FluentAssertions;
using Xunit;

namespace ShopTests;

public class NewShopTests
{
    [Fact]
    public void Sourdough_Starter_Never_Decreases_In_Quality()
    {
        var item = new Item("Sourdough Starter", 5, 80);
        new NewShop(new List<Item> { item }).UpdateQuality();
        item.Quality.Should().Be(80);
    }

    [Fact]
    public void Sourdough_Starter_Never_Changes_Its_SellIn_Date()
    {
        var item = new Item("Sourdough Starter", 5, 80);
        new NewShop(new List<Item> { item }).UpdateQuality();
        item.SellIn.Should().Be(5);
    }
}
