namespace StockBroker;

public interface StockBrokerService
{
    void PlaceBuyOrder(Order order);
    void PlaceSellOrder(Order order);
}