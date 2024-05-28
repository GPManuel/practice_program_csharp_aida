using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

internal class CartProducts
{
    private List<Product> _products;

    public CartProducts()
    {
        _products = new List<Product>();
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public bool ThereAreNoProducts()
    {
        return !_products.Any();
    }

    public decimal ComputeAllProductsCost()
    {
        return _products.Sum(p => p.ComputeCost());
    }

    public ContentsSummary CreateContentsSummary(Discount discount)
    {
        var lines = _products.Select(p => new ProductDto(p.Name, p.ComputeCost())).ToList();
        return new ContentsSummary(lines, discount.Apply(ComputeAllProductsCost()), new DiscountDto(discount.DiscountCode(), discount.Amount()));
    }
}