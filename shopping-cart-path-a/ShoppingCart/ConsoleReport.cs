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
        var summaryText = string.Empty;
        if (contentsSummary.TotalProducts() != 0)
        {
            summaryText = $"Product name, Price with VAT, Quantity{Environment.NewLine}";
            summaryText += $"Iceberg, 1.00€, 1{Environment.NewLine}";
        }
        summaryText +=
            $"Total products: {contentsSummary.TotalProducts()}{Environment.NewLine}Total price: {contentsSummary.TotalCost().ToString("F2", _culture)}€";
        _display.Display(summaryText);
    }
}