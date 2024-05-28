using System.Globalization;
using NSubstitute;
using NUnit.Framework;

namespace ShoppingCart.Tests
{
    public class ConsoleReportTest
    {

        [Test]
        public void display_empty_cart()
        {
            var summary = ContentsSummaryBuilder.EmptySummary().Build();
            var display = Substitute.For<Display>();
            CultureInfo culture = CultureInfo.InvariantCulture;
            Report consoleReport = new ConsoleReport(display, culture);

            consoleReport.Show(summary);

            var summaryText = $"Total products: 0{Environment.NewLine}Total price: 0.00\u20ac";
            display.Received(1).Display(summaryText);
        }


    }
}
