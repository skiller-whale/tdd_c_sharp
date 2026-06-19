using System.Collections.Generic;

namespace Bakery;

public class NewShop
{
    private List<Item> items;

    public NewShop(List<Item> items)
    {
        this.items = items;
    }

    public List<Item> UpdateQuality()
    {
        foreach (var item in items)
        {
            if (item.Name == "Sourdough Starter")
            {
                // Legendary: quality and sellIn never change
            }
            else
            {
                new OldShop(new List<Item> { item }).UpdateQuality();
            }
        }
        return items;
    }
}
