using System;

namespace StockBroker;

public class Order
{
    private readonly string _tickerSymbol;
    private readonly int _quantity;
    private readonly decimal _price;
    private readonly OrderType _orderType;
    
    public Order(string tickerSymbol, int quantity, decimal price, OrderType orderType)
    {
        _tickerSymbol = tickerSymbol;
        _quantity = quantity;
        _price = price;
        _orderType = orderType;
    }

    public decimal SpentMoney()
    {
        return _quantity * _price;
    }

    public bool IsBuy()
    {
        return _orderType == OrderType.Buy;
    }

    protected bool Equals(Order other)
    {
        return _tickerSymbol == other._tickerSymbol && _quantity == other._quantity && _price == other._price;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Order)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_tickerSymbol, _quantity, _price);
    }

    public override string ToString()
    {
        return
            $"{nameof(_tickerSymbol)}: {_tickerSymbol}, {nameof(_quantity)}: {_quantity}, {nameof(_price)}: {_price}";
    }

    public string TickerSymbol()
    {
        return _tickerSymbol;
    }
}