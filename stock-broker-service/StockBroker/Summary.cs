using System;
using System.Collections.Generic;

namespace StockBroker;

public class Summary
{
    private readonly DateTime _orderDate;
    private readonly decimal _buyAmount;
    private readonly decimal _sellAmount;
    private List<Order> _orders;

    public Summary(DateTime orderDate, decimal buyAmount, decimal sellAmount)
    {
        _orderDate = orderDate;
        _buyAmount = buyAmount;
        _sellAmount = sellAmount;
        _orders = new List<Order>();
    }

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }

    public DateTime OrderDate()
    {
        return _orderDate;
    }

    protected bool Equals(Summary other)
    {
        return _orderDate.Equals(other._orderDate) && _buyAmount == other._buyAmount && _sellAmount == other._sellAmount;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Summary)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_orderDate, _buyAmount, _sellAmount);
    }

    public override string ToString()
    {
        return
            $"{nameof(_orderDate)}: {_orderDate}, {nameof(_buyAmount)}: {_buyAmount}, {nameof(_sellAmount)}: {_sellAmount}";
    }
}