using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

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
        var summaryLines = CreateSummaryLines(contentsSummary);
        return string.Join(string.Empty, summaryLines.Select(line => line.FormatLine(this)).ToList());
    }

    private static IEnumerable<Line> CreateSummaryLines(ContentsSummary contentsSummary)
    {
        return GroupProductsByName(contentsSummary).Select(g => new Line(g.Key, g.First().Price(), g.Count()));
    }

    private static IEnumerable<IGrouping<string, ProductDto>> GroupProductsByName(ContentsSummary contentsSummary)
    {
        return contentsSummary.Products().GroupBy(p => p.Name());
    }

    private string Footer(ContentsSummary contentsSummary)
    {
        return $"Total products: {contentsSummary.TotalProducts()}{Environment.NewLine}Total price: {FormatPrice(contentsSummary.TotalCost())}";
    }

    public string FormatPrice(decimal price)
    {
        return price.ToString("F2", _culture)+"€";
    }
}

public class Line
{
    private readonly string _productName;
    private readonly decimal _price;
    private readonly int _quantity;

    public Line(string productName, decimal price, int quantity)
    {
        _productName = productName;
        _price = price;
        _quantity = quantity;
    }

    public string Name()
    {
        return _productName;
    }

    public decimal Price()
    {
        return _price;
    }

    public int Quantity()
    {
        return _quantity;
    }

    public string FormatLine(ConsoleReport consoleReport)
    {
        return $"{this.Name()}, {consoleReport.FormatPrice(this.Price())}, {this.Quantity()}{Environment.NewLine}";
    }
}