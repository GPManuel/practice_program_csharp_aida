using NSubstitute;
using NUnit.Framework;

namespace ShoppingCart.Tests
{
    internal class ShoppingCartDisplayTest
    {
        private ProductsRepository _productsRepository;
        private Notifier _notifier;
        private DiscountsRepository _discountsRepository;
        private Display _display;
        private ShoppingCart _shoppingCart;

        [SetUp]
        public void SetUp()
        {
            _productsRepository = Substitute.For<ProductsRepository>();
            _notifier = Substitute.For<Notifier>();
            _discountsRepository = Substitute.For<DiscountsRepository>();
            _display = Substitute.For<Display>();
            _shoppingCart = ShoppingCartTestHelpers.CreateShoppingCartForDisplay(_productsRepository, _notifier, _discountsRepository, _display);
        }

        [Test]
        public void displaying_empty_cart()
        {
            _shoppingCart.Display();

            _display.Received(1).Show(new ContentsSummary(new List<Line>()));
        }

        [Test]
        public void displaying_cart_with_one_product_tax_free_and_no_revenue()
        {
            var aProduct = "Iceberg";
            var cost = 1.0m;
            _productsRepository.Get(aProduct).Returns(ProductBuilder.TaxFreeWithNoRevenueProduct().Named(aProduct).Costing(cost).Build());

            _shoppingCart.AddItem(aProduct);
            _shoppingCart.Display();

            var summary = new ContentsSummary(new List<Line>() {new(aProduct, cost)});
            _display.Received(1).Show(summary);
        }
    }
}