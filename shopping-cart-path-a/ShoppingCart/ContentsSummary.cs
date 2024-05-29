using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart;

public record ContentsSummary
{
    private readonly List<ProductDto> _productDtos;
    private readonly decimal _totalCost;
    private readonly DiscountDto _discount;

    public ContentsSummary(List<ProductDto> productDtos, decimal totalCost, DiscountDto discount)
    {
        _productDtos = productDtos;
        _totalCost = totalCost;
        _discount = discount;
    }

    public virtual bool Equals(ContentsSummary other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _productDtos.SequenceEqual(other._productDtos) && _totalCost == other._totalCost && Equals(_discount, other._discount);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_productDtos, _totalCost, _discount);
    }

    public override string ToString()
    {
        return
            $"{nameof(_productDtos)}: {_productDtos}, {nameof(_totalCost)}: {_totalCost}, {nameof(_discount)}: {_discount}";
    }

    public int TotalProducts()
    {
        return _productDtos.Count();
    }

    public decimal TotalCost()
    {
        return _totalCost;
    }

    public IEnumerable<ProductDto> Products()
    {
        return new List<ProductDto>(_productDtos);
    }
}