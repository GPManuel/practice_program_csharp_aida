using System;
using System.Collections.Generic;
using System.Linq;

namespace StockBroker;

public class Orders
{
    private readonly List<Order> _orders;
    private readonly List<Order> _placedOrders;
    private readonly List<Order> _failedOrders;

    public Orders(List<Order> orders)
    {
        _orders = orders;
        _placedOrders = new List<Order>();
        _failedOrders = new List<Order>();
    }

    public decimal TotalSpentMoney()
    {
        return _placedOrders.Sum(order => order.SpentMoney());
    }

    public void PlaceBuy(StockBrokerService stockBrokerService)
    {
        PlaceOrders(_orders, stockBrokerService.PlaceBuyOrder);
    }

    public void PlaceSell(StockBrokerService stockBrokerService)
    {
        PlaceOrders(_orders, stockBrokerService.PlaceSellOrder);
    }

    private void PlaceOrders(List<Order> orders, Action<Order> action)
    {
        foreach (var order in orders)
        {
            try
            {
                action.Invoke(order);
                _placedOrders.Add(order);
            }
            catch (Exception e)
            {
                _failedOrders.Add(order);
            }
        }
    }

    public void Add(Order order)
    {
        _orders.Add(order);
    }

    public List<Order> FailedOrders()
    {
        return _failedOrders;
    }
}