using System.Collections.Generic;
using Bakery;
using FluentAssertions;
using Xunit;

namespace ShopTests;

public class ShopTests
{
    public class NormalItems
    {
        [Fact]
        public void Decreases_SellIn_By_1_Each_Day()
        {
            var item = new Item("Bread", 5, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.SellIn.Should().Be(4);
        }

        [Fact]
        public void Decreases_Quality_By_1_Before_The_SellBy_Date()
        {
            var item = new Item("Bread", 5, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(19);
        }

        [Fact]
        public void Decreases_Quality_By_2_On_The_SellBy_Date()
        {
            var item = new Item("Bread", 0, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(18);
        }

        [Fact]
        public void Decreases_Quality_By_2_Each_Day_After_The_SellBy_Date()
        {
            var item = new Item("Bread", -3, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(18);
        }

        [Fact]
        public void Continues_To_Decrease_SellIn_When_Quality_Is_0()
        {
            var item = new Item("Bread", 5, 0);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.SellIn.Should().Be(4);
        }

        [Fact]
        public void Quality_Never_Drops_Below_0_Before_The_SellBy_Date()
        {
            var item = new Item("Bread", 5, 0);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(0);
        }

        [Fact]
        public void Quality_Never_Drops_Below_0_On_The_SellBy_Date()
        {
            var item = new Item("Bread", 0, 1);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(0);
        }

        [Fact]
        public void Quality_Never_Drops_Below_0_After_The_SellBy_Date()
        {
            var item = new Item("Bread", -1, 1);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(0);
        }

        [Fact]
        public void Degrades_Correctly_Over_Multiple_Days_Spanning_The_SellBy_Date()
        {
            var item = new Item("Bread", 2, 10);
            var shop = new Shop(new List<Item> { item });
            shop.UpdateQuality(); // sellIn 1, quality 9
            shop.UpdateQuality(); // sellIn 0, quality 8
            shop.UpdateQuality(); // sellIn -1, quality 6
            shop.UpdateQuality(); // sellIn -2, quality 4
            item.Quality.Should().Be(4);
            item.SellIn.Should().Be(-2);
        }
    }

    public class FruitCake
    {
        [Fact]
        public void Decreases_SellIn_By_1()
        {
            var item = new Item("Fruit Cake", 5, 10);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.SellIn.Should().Be(4);
        }

        [Fact]
        public void Increases_Quality_By_1_Before_The_SellBy_Date()
        {
            var item = new Item("Fruit Cake", 5, 10);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(11);
        }

        [Fact]
        public void Increases_Quality_By_2_On_The_SellBy_Date()
        {
            var item = new Item("Fruit Cake", 0, 10);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(12);
        }

        [Fact]
        public void Increases_Quality_By_2_After_The_SellBy_Date()
        {
            var item = new Item("Fruit Cake", -2, 10);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(12);
        }

        [Fact]
        public void Quality_Never_Exceeds_50()
        {
            var item = new Item("Fruit Cake", 5, 50);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(50);
        }

        [Fact]
        public void Quality_Is_Capped_At_50_Before_The_SellBy_Date()
        {
            var item = new Item("Fruit Cake", 5, 49);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(50);
        }

        [Fact]
        public void Quality_Is_Capped_At_50_After_The_SellBy_Date()
        {
            var item = new Item("Fruit Cake", -1, 49);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(50);
        }

        [Fact]
        public void Increases_Quality_Over_Many_Days_Spanning_The_SellBy_Date()
        {
            var item = new Item("Fruit Cake", 2, 40);
            var shop = new Shop(new List<Item> { item });
            shop.UpdateQuality(); // +1, sellIn 1, quality 41
            shop.UpdateQuality(); // +1, sellIn 0, quality 42
            shop.UpdateQuality(); // +2, sellIn -1, quality 44
            shop.UpdateQuality(); // +2, sellIn -2, quality 46
            item.Quality.Should().Be(46);
        }
    }

    public class SourdoughStarter
    {
        [Fact]
        public void Never_Changes_Quality()
        {
            var item = new Item("Sourdough Starter", 0, 80);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(80);
        }

        [Fact]
        public void Never_Changes_The_SellIn_Date()
        {
            var item = new Item("Sourdough Starter", 0, 80);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.SellIn.Should().Be(0);
        }

        [Fact]
        public void Remains_Completely_Unchanged_After_Many_Days()
        {
            var item = new Item("Sourdough Starter", 0, 80);
            var shop = new Shop(new List<Item> { item });
            for (int i = 0; i < 30; i++)
            {
                shop.UpdateQuality();
            }
            item.Quality.Should().Be(80);
            item.SellIn.Should().Be(0);
        }
    }

    public class WeddingCake
    {
        [Fact]
        public void Decreases_SellIn_By_1()
        {
            var item = new Item("Wedding Cake", 15, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.SellIn.Should().Be(14);
        }

        [Fact]
        public void Increases_Quality_By_1_With_More_Than_10_Days_Remaining()
        {
            var item = new Item("Wedding Cake", 15, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(21);
        }

        [Fact]
        public void Increases_Quality_By_1_With_Exactly_11_Days_Remaining()
        {
            var item = new Item("Wedding Cake", 11, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(21);
        }

        [Fact]
        public void Increases_Quality_By_2_With_Exactly_10_Days_Remaining()
        {
            var item = new Item("Wedding Cake", 10, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(22);
        }

        [Fact]
        public void Increases_Quality_By_2_With_6_Days_Remaining()
        {
            var item = new Item("Wedding Cake", 6, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(22);
        }

        [Fact]
        public void Increases_Quality_By_3_With_Exactly_5_Days_Remaining()
        {
            var item = new Item("Wedding Cake", 5, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(23);
        }

        [Fact]
        public void Increases_Quality_By_3_With_1_Day_Remaining()
        {
            var item = new Item("Wedding Cake", 1, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(23);
        }

        [Fact]
        public void Drops_Quality_To_0_On_The_SellBy_Date()
        {
            var item = new Item("Wedding Cake", 0, 20);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(0);
        }

        [Fact]
        public void Quality_Stays_0_After_The_SellBy_Date()
        {
            var item = new Item("Wedding Cake", -1, 0);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(0);
        }

        [Fact]
        public void SellIn_Continues_To_Decrease_After_The_SellBy_Date()
        {
            var item = new Item("Wedding Cake", -1, 0);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.SellIn.Should().Be(-2);
        }

        [Fact]
        public void Quality_Never_Exceeds_50()
        {
            var item = new Item("Wedding Cake", 5, 50);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(50);
        }

        [Fact]
        public void Quality_Is_Capped_At_50_With_High_Rate_Increases()
        {
            var item = new Item("Wedding Cake", 5, 48);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(50);
        }

        [Fact]
        public void Quality_Is_Capped_At_50_With_Moderate_Rate_Increases()
        {
            var item = new Item("Wedding Cake", 10, 49);
            new Shop(new List<Item> { item }).UpdateQuality();
            item.Quality.Should().Be(50);
        }

        [Fact]
        public void Simulates_A_Full_Run_Up_To_The_Wedding()
        {
            var item = new Item("Wedding Cake", 15, 5);
            var shop = new Shop(new List<Item> { item });

            // Days 15-11: +1/day for 5 days = +5
            for (int i = 0; i < 5; i++) shop.UpdateQuality();
            item.Quality.Should().Be(10);
            item.SellIn.Should().Be(10);

            // Days 10-6: +2/day for 5 days = +10
            for (int i = 0; i < 5; i++) shop.UpdateQuality();
            item.Quality.Should().Be(20);
            item.SellIn.Should().Be(5);

            // Days 5-1: +3/day for 5 days = +15
            for (int i = 0; i < 5; i++) shop.UpdateQuality();
            item.Quality.Should().Be(35);
            item.SellIn.Should().Be(0);

            // Day 0: quality drops to 0
            shop.UpdateQuality();
            item.Quality.Should().Be(0);
            item.SellIn.Should().Be(-1);
        }
    }

    public class MultipleItems
    {
        [Fact]
        public void Updates_All_Items_In_A_Single_Call()
        {
            var bread = new Item("Bread", 3, 10);
            var fruitCake = new Item("Fruit Cake", 3, 10);
            var sourdough = new Item("Sourdough Starter", 0, 80);
            var weddingCake = new Item("Wedding Cake", 8, 20);

            new Shop(new List<Item> { bread, fruitCake, sourdough, weddingCake }).UpdateQuality();

            bread.Quality.Should().Be(9);
            fruitCake.Quality.Should().Be(11);
            sourdough.Quality.Should().Be(80);
            weddingCake.Quality.Should().Be(22);
        }
    }

    public class ReturnValue
    {
        [Fact]
        public void Returns_The_Same_Items_List()
        {
            var items = new List<Item> { new Item("Bread", 5, 10) };
            var result = new Shop(items).UpdateQuality();
            result.Should().BeSameAs(items);
        }
    }
}
