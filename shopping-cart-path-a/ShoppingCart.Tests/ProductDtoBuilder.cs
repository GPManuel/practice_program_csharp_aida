namespace ShoppingCart.Tests;

public class ProductDtoBuilder
{
    private decimal _priceWithVat;
    private string _productName;

    public ProductDtoBuilder(string productName, decimal priceWithVat)
    {
        _priceWithVat = priceWithVat;
        _productName = productName;
    }

    public ProductDtoBuilder Named(string productName)
    {
        _productName = productName;
        return this;
    }

    public ProductDtoBuilder Costing(decimal priceWithVat)
    {
        _priceWithVat = priceWithVat;
        return this;
    }

    public ProductDto Build()
    {
        return new ProductDto(_productName, _priceWithVat);
    }

    public static ProductDtoBuilder ProductData()
    {
        return new ProductDtoBuilder("Pepe", 1);
    }
}