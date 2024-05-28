using System;
using System.Globalization;

namespace ShoppingCart;

public class ConsoleReport : Report
{
    private readonly Display _display;
    private readonly CultureInfo _culture;

    public ConsoleReport(Display display, CultureInfo culture)
    {
        _display = display;
        _culture = culture;
    }

    public void Show(ContentsSummary contentsSummary)
    {
        _display.Display($"Total products: {contentsSummary.TotalProducts()}{Environment.NewLine}Total price: {contentsSummary.TotalCost().ToString("F2",_culture)}\u20ac");
    }
}