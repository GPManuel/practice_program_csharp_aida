using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace StockBroker.Tests
{
    public class StockBrokerClientTest
    {
        private Display _display;
        private StockBrokerService? _stockBrokerService;
        private Calendar _calendar;
        private StockBrokerClient _stockBrokerClient;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<Display>();
            _stockBrokerService = Substitute.For<StockBrokerService>();
            _calendar = Substitute.For<Calendar>();
            _stockBrokerClient = new StockBrokerClient(_display, _stockBrokerService, _calendar);
        }

        [Test]
        public void place_empty_order_sequence()
        {
            _calendar.GetDate().Returns(Date(2000, 1, 25, 9, 0));

            _stockBrokerClient.PlaceOrders("");

            _display.Received(1).Print($"1/25/2000 9:00 AM Buy: € 0.00, Sell: € 0.00");
        }

        [TestCase(829.08)]
        [TestCase(745)]
        public void place_1_buy_order_of_1_stock(decimal price)
        {
            _calendar.GetDate().Returns(Date(2011, 3, 16, 11, 0));

            _stockBrokerClient.PlaceOrders($"GOOG 1 {FormatAmount(price)} B");

            _display.Received(1).Print($"3/16/2011 11:00 AM Buy: € {FormatAmount(price)}, Sell: € 0.00");
        }

        [Test]
        public void place_1_buy_order_of_2_stock()
        {
            _calendar.GetDate().Returns(Date(1999, 3, 5, 16, 0));

            _stockBrokerClient.PlaceOrders($"GOOG 2 2 B");

            var expectedPrice = 4m;
            _display.Received(1).Print($"3/5/1999 4:00 PM Buy: € {FormatAmount(expectedPrice)}, Sell: € 0.00");
        }

        [Test]
        public void place_1_sell_order()
        {
            _calendar.GetDate().Returns(Date(1999, 3, 5, 16, 0));

            _stockBrokerClient.PlaceOrders($"ORCL 2 3 S");

            _display.Received(1).Print($"3/5/1999 4:00 PM Buy: € 0.00, Sell: € {FormatAmount(6)}");
        }

        [Test]
        public void place_2_orders()
        {
            _calendar.GetDate().Returns(Date(1999, 3, 5, 16, 0));

            _stockBrokerClient.PlaceOrders($"ORCL 2 3 S,GOOG 2 2 B");

            _display.Received(1).Print($"3/5/1999 4:00 PM Buy: € {FormatAmount(4)}, Sell: € {FormatAmount(6)}");
            _stockBrokerService.Received(1).PlaceSellOrder(new Order("ORCL", 2, 3, OrderType.Sell));
            _stockBrokerService.Received(1).PlaceBuyOrder(new Order("GOOG", 2, 2, OrderType.Buy));
        }

        [Test]
        public void place_1_failed_sell_order()
        {
            _calendar.GetDate().Returns(Date(1999, 3, 5, 16, 0));
            _stockBrokerService.When(x => x.PlaceSellOrder(Arg.Any<Order>())).Do(x => { throw new OrderNotPlacedException(); });

            _stockBrokerClient.PlaceOrders($"ORCL 2 3 S");

            _display.Received(1).Print($"3/5/1999 4:00 PM Buy: € 0.00, Sell: € 0.00, Failed: ORCL");
        }

        [Test]
        public void place_1_failed_buy_order()
        {
            _calendar.GetDate().Returns(Date(1999, 3, 5, 16, 0));
            _stockBrokerService.When(x => x.PlaceBuyOrder(Arg.Any<Order>())).Do(x => { throw new OrderNotPlacedException(); });

            _stockBrokerClient.PlaceOrders($"GOO 2 1.00 B");

            _display.Received(1).Print($"3/5/1999 4:00 PM Buy: € 0.00, Sell: € 0.00, Failed: GOO");
        }

        [Test]
        public void place_2_failed_buy_order()
        {
            _calendar.GetDate().Returns(Date(1999, 3, 5, 16, 0));
            _stockBrokerService.When(x => x.PlaceBuyOrder(Arg.Any<Order>())).Do(x => { throw new OrderNotPlacedException(); });
            _stockBrokerService.When(x => x.PlaceSellOrder(Arg.Any<Order>())).Do(x => { throw new OrderNotPlacedException(); });

            _stockBrokerClient.PlaceOrders($"GOO 2 1.00 B,PACO 1 1.00 S");

            _display.Received(1).Print($"3/5/1999 4:00 PM Buy: € 0.00, Sell: € 0.00, Failed: PACO, GOO");
        }

        [Test]
        public void place_1_failed_order_and_1_sucessful_order()
        {
            _calendar.GetDate().Returns(Date(1999, 3, 5, 16, 0));
            _stockBrokerService.When(x => x.PlaceBuyOrder(Arg.Any<Order>())).Do(x => { throw new OrderNotPlacedException(); });

            _stockBrokerClient.PlaceOrders($"GOO 2 1.00 B,PACO 1 1.00 S");

            _display.Received(1).Print($"3/5/1999 4:00 PM Buy: € 0.00, Sell: € 1.00, Failed: GOO");
            _stockBrokerService.Received(1).PlaceSellOrder(new Order("PACO",1, 1m, OrderType.Sell));
        }

        private static string FormatAmount(decimal amount)
        {
            return amount.ToString("F2", new CultureInfo("en-us"));
        }

        private static DateTime Date(int year, int month, int day, int hour, int minute)
        {
            return new DateTime(year, month, day, hour, minute, 0);
        }
    }
}