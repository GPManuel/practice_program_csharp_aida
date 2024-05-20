using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using static ShoppingCart.Tests.ProductBuilder;

namespace ShoppingCart.Tests;

public class ShoppingCartErrorTest
{
    private const string Iceberg = "Iceberg";
    private ProductsRepository _productsRepository;
    private Notifier _notifier;
    private ShoppingCart _shoppingCart;
    private CheckoutService _checkoutService;
    private DiscountsRepository _discountsRepository;

    [SetUp]
    public void SetUp()
    {
        _productsRepository = Substitute.For<ProductsRepository>();
        _notifier = Substitute.For<Notifier>();
        _checkoutService = Substitute.For<CheckoutService>();
        _discountsRepository = Substitute.For<DiscountsRepository>();
        _shoppingCart = new ShoppingCart(_productsRepository, _notifier, _checkoutService, _discountsRepository);
    }

    [Test]
    public void add_not_available_product()
    {
        const string notAvailableProductName = "some_item";
        _productsRepository.Get(notAvailableProductName).ReturnsNull();

        _shoppingCart.AddItem(notAvailableProductName);

        _notifier.Received(1).ShowError("Product is not available");
    }

    [Test]
    public void apply_not_available_discount()
    {
        var notAvailableDiscount = DiscountCode.PROMO_20;
        _discountsRepository.Get(notAvailableDiscount).ReturnsNull();

        _shoppingCart.ApplyDiscount(notAvailableDiscount);

        _notifier.Received(1).ShowError("Discount is not available");
    }

    [Test]
    public void checkout_without_products()
    {
        _shoppingCart.Checkout();

        _notifier.Received(1).ShowError("No product selected, please select a product");
    }

    [Test]
    public void checkout_twice()
    {
        _productsRepository.Get(Iceberg).Returns(
            TaxFreeWithNoRevenueProduct().Named(Iceberg).Costing(1m).Build());

        _shoppingCart.AddItem(Iceberg);
        _shoppingCart.Checkout();
        _shoppingCart.Checkout();

        _checkoutService.Received(1).Checkout(CreateShoppingCartDto(1));
        _notifier.Received(1).ShowError("No product selected, please select a product");
    }


    private static ShoppingCartDto CreateShoppingCartDto(decimal cost)
    {
        return new ShoppingCartDto(cost);
    }
}