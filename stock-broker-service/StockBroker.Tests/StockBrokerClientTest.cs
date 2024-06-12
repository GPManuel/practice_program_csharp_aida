using NSubstitute;
using NUnit.Framework;

namespace StockBroker.Tests
{
    public class StockBrokerClientTest
    {
        [Test]
        public void show_summary_after_place_order()
        {
            var display = Substitute.For<Display>();
            var stockBrokerService = Substitute.For<StockBrokerService>();
            var calendar = Substitute.For<Calendar>();
            var stockBrokerClient = new StockBrokerClient(display, stockBrokerService, calendar);
            var orderDate = new DateTime(2000, 1, 1, 12, 0, 0);
            calendar.GetDate().Returns(orderDate);

            stockBrokerClient.PlaceOrders("");

            display.Received(1).Show(new Summary(orderDate, 0m, 0m));
        }
    }
}