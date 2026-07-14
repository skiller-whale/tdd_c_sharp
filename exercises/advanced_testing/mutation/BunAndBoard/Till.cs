using System;
using System.Collections.Generic;

namespace BunAndBoard;

public class Till
{
    private const int BulkThreshold = 10;
    private const decimal BulkDiscount = 0.10m;
    private const decimal MemberDiscount = 0.05m;
    private const decimal FreeDeliveryThreshold = 25.0m;
    private const decimal DeliveryFee = 3.50m;

    public Receipt Checkout(List<LineItem> items, bool member)
    {
        decimal subtotal = 0.0m;
        foreach (var item in items)
        {
            decimal lineTotal = item.Quantity * item.UnitPrice;
            if (item.Quantity > BulkThreshold)
            {
                lineTotal -= lineTotal * BulkDiscount;
            }
            subtotal += lineTotal;
        }

        if (member)
        {
            subtotal -= subtotal * MemberDiscount;
        }

        decimal delivery = DeliveryFee;
        if (subtotal >= FreeDeliveryThreshold)
        {
            delivery = 0.0m;
        }
        if (subtotal == 0.0m)
        {
            delivery = 0.0m;
        }

        int points = (int)subtotal;
        if (member)
        {
            points *= 2;
        }

        decimal total = Round(subtotal + delivery);
        return new Receipt(total, points);
    }

    private static decimal Round(decimal amount)
    {
        return Math.Round(amount, 2, MidpointRounding.AwayFromZero);
    }
}
