namespace StockBroker;

public record Order(
    string TickerSymbol,
    int Quantity,
    decimal Price,
    OrderType OrderType
);