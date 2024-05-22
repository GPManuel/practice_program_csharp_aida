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

        [Test]
        public void displaying_empty_cart()
        {
            _productsRepository = Substitute.For<ProductsRepository>();
            _notifier = Substitute.For<Notifier>();
            _discountsRepository = Substitute.For<DiscountsRepository>();
            _display = Substitute.For<Display>();
            var shoppingCart = ShoppingCartTestHelpers.CreateShoppingCartForDisplay(_productsRepository, _notifier, _discountsRepository, _display);

            shoppingCart.Display();

            _display.Received(1).Show(new ContentsSummary());
        }
    }
}