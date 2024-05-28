using System.Collections;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using static ShoppingCart.Tests.ContentsSummaryBuilder;
using static ShoppingCart.Tests.LineBuilder;
using static ShoppingCart.Tests.ProductBuilder;

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

            _display.Received(1).Show(EmptySummary().Build());
        }

        [Test]
        public void displaying_cart_with_one_product_tax_free_and_no_revenue()
        {
            var aProduct = "Iceberg";
            var cost = 1.0m;
            _productsRepository.Get(aProduct).Returns(TaxFreeWithNoRevenueProduct().Named(aProduct).Costing(cost).Build());

            _shoppingCart.AddItem(aProduct);
            _shoppingCart.Display();

            _display.Received(1).Show(
                Summary().With(LineForProduct()
                                            .Named(aProduct)
                                            .Costing(cost))
                                       .WithTotalCost(cost)
                                       .Build()
                );
        }

        [Test]
        public void displaying_cart_with_one_product_tax_free_and_no_revenue_with_discount()
        {
            var aProduct = "Iceberg";
            var cost = 100.0m;
            var discountCode = DiscountCode.PROMO_5;
            _productsRepository.Get(aProduct).Returns(TaxFreeWithNoRevenueProduct().Named(aProduct).Costing(cost).Build());
            var discount = new Discount(discountCode, 0.05m);
            _discountsRepository.Get(discountCode).Returns(discount);

            _shoppingCart.AddItem(aProduct);
            _shoppingCart.ApplyDiscount(discountCode);
            _shoppingCart.Display();

            _display.Received(1).Show(
                Summary().With(LineForProduct()
                        .Named(aProduct)
                        .Costing(cost))
                    .WithTotalCost(95.0m)
                    .WithDiscount(new DiscountDto(DiscountCode.PROMO_5,0.05m))
                    .Build()
            );
        }
    }
}

