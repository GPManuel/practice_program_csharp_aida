namespace ShoppingCart.Tests;

public class ContentsSummaryBuilder
{
    private List<ProductDto> _products;
    private decimal _totalCost;
    private DiscountDto _discount;

    public ContentsSummaryBuilder()
    {
        _products = new List<ProductDto>();
        _totalCost = 0;
        _discount = new DiscountDto(DiscountCode.None, 0);
    }

    public ContentsSummaryBuilder With(ProductDtoBuilder productDtoBuilder)
    {
        _products.Add(productDtoBuilder.Build());
        return this;
    }

    public ContentsSummaryBuilder WithTotalCost(decimal totalCost)
    {
        _totalCost = totalCost;
        return this;
    }

    public ContentsSummary Build()
    {
        return new ContentsSummary(_products, _totalCost, _discount);
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