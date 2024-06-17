using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StockBroker;

public class PlacementResult
{
    private readonly Orders _buysOrders;
    private readonly Orders _sellOrders;

    public PlacementResult(Orders buysOrders, Orders sellOrders)
    {
        _buysOrders = buysOrders;
        _sellOrders = sellOrders;
    }

    public string Format(DateTime dateTime)
    {
        return $"{FormatDate(dateTime)} Buy: € {FormatAmount(_buysOrders.TotalSpentMoney())}, Sell: € {FormatAmount(_sellOrders.TotalSpentMoney())}{FormatFailedOrders()}";
    }
    private string FormatDate(DateTime dateTime)
    {
        return dateTime.ToString("g", new CultureInfo("en-US"));
    }

    private string FormatFailedOrders()
    {
        var failedOrders = _sellOrders.FailedOrders().Concat(_buysOrders.FailedOrders()).ToList();
        if (failedOrders.Count == 0)
        {
            return string.Empty;
        }

        return $", Failed: {string.Join(", ", failedOrders.Select(order => order.TickerSymbol()).ToList())}";
    }

    private string FormatAmount(decimal amount)
    {
        return amount.ToString("F2", new CultureInfo("es-us"));
    }
}