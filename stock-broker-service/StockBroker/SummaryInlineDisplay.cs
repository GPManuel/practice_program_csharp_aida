namespace StockBroker;

public class SummaryInlineDisplay : Display
{
    private readonly Printer _printer;

    public SummaryInlineDisplay(Printer printer)
    {
        _printer = printer;
    }

    public void Show(Summary summary)
    {
        _printer.Print($"{summary.OrderDate()} Buy: € 0.00, Sell: € 0.00");
    }
}