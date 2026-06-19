using System.Collections.Generic;

namespace Bakery;

public class OldShop
{
    private List<Item> items;

    public OldShop(List<Item> items)
    {
        this.items = items;
    }

    public List<Item> UpdateQuality()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Name != "Fruit Cake" && items[i].Name != "Wedding Cake")
            {
                if (items[i].Quality > 0)
                {
                    if (items[i].Name != "Sourdough Starter")
                    {
                        items[i].Quality = items[i].Quality - 1;
                    }
                }
            }
            else
            {
                if (items[i].Quality < 50)
                {
                    items[i].Quality = items[i].Quality + 1;
                    if (items[i].Name == "Wedding Cake")
                    {
                        if (items[i].SellIn < 11)
                        {
                            if (items[i].Quality < 50)
                            {
                                items[i].Quality = items[i].Quality + 1;
                            }
                        }
                        if (items[i].SellIn < 6)
                        {
                            if (items[i].Quality < 50)
                            {
                                items[i].Quality = items[i].Quality + 1;
                            }
                        }
                    }
                }
            }
            if (items[i].Name != "Sourdough Starter")
            {
                items[i].SellIn = items[i].SellIn - 1;
            }
            if (items[i].SellIn < 0)
            {
                if (items[i].Name != "Fruit Cake")
                {
                    if (items[i].Name != "Wedding Cake")
                    {
                        if (items[i].Quality > 0)
                        {
                            if (items[i].Name != "Sourdough Starter")
                            {
                                items[i].Quality = items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        items[i].Quality = items[i].Quality - items[i].Quality;
                    }
                }
                else
                {
                    if (items[i].Quality < 50)
                    {
                        items[i].Quality = items[i].Quality + 1;
                    }
                }
            }
        }
        return items;
    }
}
