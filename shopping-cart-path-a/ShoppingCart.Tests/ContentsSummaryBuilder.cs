namespace ShoppingCart.Tests;

public class ContentsSummaryBuilder
{
    private List<ProductDto> _lines;
    private decimal _totalCost;
    private DiscountDto _discount;

    public ContentsSummaryBuilder()
    {
        _lines = new List<ProductDto>();
        _totalCost = 0;
        _discount = new DiscountDto(DiscountCode.None,0);
    }

    public ContentsSummaryBuilder With(LineBuilder lineBuilder)
    {
        _lines.Add(lineBuilder.Build());
        return this;
    }

    public ContentsSummaryBuilder WithTotalCost(decimal totalCost)
    {
        _totalCost = totalCost;
        return this;
    }

    public ContentsSummary Build()
    {
        return new ContentsSummary(_lines, _totalCost, _discount);
    }

    public static ContentsSummaryBuilder EmptySummary()
    {
        return new ContentsSummaryBuilder();
    }

    public static ContentsSummaryBuilder Summary()
    {
        return EmptySummary();
    }

    public ContentsSummaryBuilder WithDiscount(DiscountDto discount)
    {
        _discount = discount;
        return this;
    }
}