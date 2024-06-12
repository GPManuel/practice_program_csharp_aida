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
        _display.Show(new Summary(_calendar.GetDate(), 0m, 0m));
    }
}