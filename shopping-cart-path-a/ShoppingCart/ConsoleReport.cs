using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            summaryText = Header();
            summaryText += FormatProductsLines(contentsSummary);
        }
        summaryText += Footer(contentsSummary);
        _display.Display(summaryText);
    }

    private static string Header()
    {
        return $"Product name, Price with VAT, Quantity{Environment.NewLine}";
    }

    private string FormatProductsLines(ContentsSummary contentsSummary)
    {
        var lines = contentsSummary.Products().Select(product => $"{product.Name()}, {FormatPrice(product.Price())}, 1{Environment.NewLine}").ToList();
        return string.Join(string.Empty, lines);
    }

    private string Footer(ContentsSummary contentsSummary)
    {
        return $"Total products: {contentsSummary.TotalProducts()}{Environment.NewLine}Total price: {FormatPrice(contentsSummary.TotalCost())}";
    }

    private string FormatPrice(decimal price)
    {
        return price.ToString("F2", _culture)+"€";
    }
}