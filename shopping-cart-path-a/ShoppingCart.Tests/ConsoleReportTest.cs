using System.Globalization;
using NSubstitute;
using NUnit.Framework;
using static ShoppingCart.Tests.ProductDtoBuilder;

namespace ShoppingCart.Tests
{
    public class ConsoleReportTest
    {
        private Report _consoleReport;
        private Display? _display;

        [SetUp]
        public void SetUp()
        {
            _display = Substitute.For<Display>();
            CultureInfo culture = CultureInfo.InvariantCulture;
            _consoleReport = new ConsoleReport(_display, culture);
        }

        [Test]
        public void display_empty_cart()
        {
            var summary = ContentsSummaryBuilder.EmptySummary().Build();

            _consoleReport.Show(summary);

            var summaryText = Footer(0,"0.00");
            _display.Received(1).Display(summaryText);
        }

        [Test]
        public void display_cart_with_one_product()
        {
            var summary = ContentsSummaryBuilder.Summary().With(ProductData().Named("Iceberg").Costing(1))
                .WithTotalPrice(1).Build();

            _consoleReport.Show(summary);

            var summaryText = Header();
            summaryText += ProductLine("Iceberg", "1.00", 1);
            summaryText += Footer(1, "1.00");
            _display.Received(1).Display(summaryText);
        }

        [Test]
        public void display_cart_with_two_different_product()
        {
            var summary = ContentsSummaryBuilder.Summary().With(ProductData().Named("Iceberg").Costing(1))
                .With(ProductData().Named("Tomato").Costing(2))
                .WithTotalPrice(3).Build();

            _consoleReport.Show(summary);

            var summaryText = Header();
            summaryText += ProductLine("Iceberg", "1.00", 1);
            summaryText += ProductLine("Tomato", "2.00", 1);
            summaryText += Footer(2, "3.00");
            _display.Received(1).Display(summaryText);
        }

        private static string ProductLine(string name, string price, int quantity)
        {
            return $"{name}, {price}€, {quantity}{Environment.NewLine}";
        }

        private static string Header()
        {
            return $"Product name, Price with VAT, Quantity{Environment.NewLine}";
        }

        private static string Footer(int totalProducts, string totalPrice)
        {
            return $"Total products: {totalProducts}{Environment.NewLine}Total price: {totalPrice}€";
        }
    }
}
