using System;
using System.Globalization;

namespace StockBroker;

public class StockBrokerClient
{
    private readonly Display _display;
    private readonly StockBrokerService _stockBrokerService;
    private readonly Calendar _calendar;

    public StockBrokerClient(Display display, StockBrokerService stockBrokerService, Calendar calendar)
    {
        _display = display;
        _stockBrokerService = stockBrokerService;
        _calendar = calendar;
    }

    public void PlaceOrders(string orderSequence)
    {
        var dateTime = _calendar.GetDate();
        if (string.IsNullOrEmpty(orderSequence))
        {
            _display.Print($"{FormatDate(dateTime)} Buy: € 0.00, Sell: € 0.00");
            return;
        }

        var price = decimal.Parse(orderSequence.Split(' ')[2]);
        var quantity = int.Parse(orderSequence.Split(' ')[1]);
        _display.Print($"{FormatDate(dateTime)} Buy: € {FormatAmount(price*quantity)}, Sell: € 0.00");
    }

    private string FormatAmount(decimal amount)
    {
        return amount.ToString("F2", new CultureInfo("es-us"));
    }

    private string FormatDate(DateTime dateTime)
    {
        return dateTime.ToString("g", new CultureInfo("en-US"));
    }
}