using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using static ShoppingCart.Tests.ShoppingCartTestHelpers;

namespace ShoppingCart.Tests
{
    internal class ShoppingCartDisplayTest
    {
        private ProductsRepository _productsRepository;
        private Notifier _notifier;
        private ShoppingCart _shoppingCart;
        private DiscountsRepository _discountsRepository;
        private Display _display;

        [SetUp]
        public void SetUp()
        {
            _productsRepository = Substitute.For<ProductsRepository>();
            _notifier = Substitute.For<Notifier>();
            _discountsRepository = Substitute.For<DiscountsRepository>();
            _display = Substitute.For<Display>();
            _shoppingCart = CreateShoppingCartForDisplay(_productsRepository, _notifier, _discountsRepository, _display);
        }

        [Test]
        public void displaying_empty_cart()
        {
            _shoppingCart.Display();

            var displayedLines = Enumerable.Empty<Line>();
            var summary = new Summary(displayedLines);
            _display.Received(1).Show(summary);
        }
    }
}
