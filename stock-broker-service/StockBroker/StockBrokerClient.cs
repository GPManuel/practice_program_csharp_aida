using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StockBroker;

public class StockBrokerClient
{
    private readonly Display _display;
    private readonly StockBrokerService _stockBrokerService;
    private readonly Calendar _calendar;
    private OrderPlacementFactory _orderPlacementFactory;

    public StockBrokerClient(Display display, StockBrokerService stockBrokerService, Calendar calendar)
    {
        _display = display;
        _stockBrokerService = stockBrokerService;
        _calendar = calendar;
        _orderPlacementFactory = new OrderPlacementFactory();
    }

    public void PlaceOrders(string orderSequence)
    {
        var dateTime = _calendar.GetDate();
        var ordersPlacement = _orderPlacementFactory.CreateOrdersPlacementFrom(orderSequence);
        var result = ordersPlacement.Process(_stockBrokerService);
        _display.Print($"{result.Format(dateTime)}");
    }

    private class OrderPlacementFactory
    {
        public OrdersPlacement CreateOrdersPlacementFrom(string orderSequence)
        {
            var ordersRepresentations = ExtractOrderRepresentationsFrom(orderSequence);

            var buyOrders = new List<Order>();
            var sellOrders = new List<Order>();
            foreach (var orderRepresentation in ordersRepresentations)
            {
                var order = CreateOrder(orderRepresentation);

                if (order.IsBuy())
                {
                    buyOrders.Add(order);
                }
                else
                {
                    sellOrders.Add(order);
                }
            }

            var sellOrdersType = new Orders(sellOrders);
            var buysOrdersType = new Orders(buyOrders);
            return new OrdersPlacement(sellOrdersType, buysOrdersType);
        }

        private Order CreateOrder(string orderRepresentation)
        {
            var quantity = int.Parse(orderRepresentation.Split(' ')[1]);
            var price = decimal.Parse(orderRepresentation.Split(' ')[2], new CultureInfo("es-us"));
            var orderType = orderRepresentation.Split(' ')[3];
            return new Order(orderRepresentation.Split(' ')[0], quantity, price, orderType.Equals("B") ? OrderType.Buy : OrderType.Sell);
        }

        private static string[] ExtractOrderRepresentationsFrom(string orderSequence)
        {
            return orderSequence.Split(',').ToList().Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }
    }
}
