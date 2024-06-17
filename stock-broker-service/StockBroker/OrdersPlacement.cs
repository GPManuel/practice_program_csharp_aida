using System;
using System.Collections.Generic;

namespace StockBroker;

public class OrdersPlacement
{
    private readonly Orders _sellOrders;
    private readonly Orders _buysOrders;

    public OrdersPlacement(Orders sellOrders, Orders buysOrders)
    {
        _sellOrders = sellOrders;
        _buysOrders = buysOrders;
    }

    public PlacementResult Process(StockBrokerService stockBrokerService)
    {
        _buysOrders.PlaceBuy(stockBrokerService);
        _sellOrders.PlaceSell(stockBrokerService);
        decimal totalBuys = _buysOrders.TotalSpentMoney();
        decimal totalSells = _sellOrders.TotalSpentMoney();
        return new PlacementResult(_buysOrders, _sellOrders);
    }
}