using NUnit.Framework;

namespace ShoppingCart.Tests
{
    public class ConsoleReportGeneratorTest
    {

        [Test]
        public void display_empty_cart()
        {
            var summary = ContentsSummaryBuilder.EmptySummary().Build();

            Report consoleReport = new ConsoleReport();

            consoleReport.Show(summary);
        }
    }

    public class ConsoleReport : Report
    {
        public void Show(ContentsSummary contentsSummary)
        {
        }
    }
}
