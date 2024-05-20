using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Tests
{
    public class ShoppingCartTestHelpers
    {
        public static ShoppingCartDto CreateShoppingCartDto(decimal cost)
        {
            return new ShoppingCartDto(cost);
        }

        public static ShoppingCart CreateShoppingCart(ProductsRepository productsRepository, Notifier notifier, CheckoutService checkoutService, DiscountsRepository discountsRepository)
        {
            return new ShoppingCart(productsRepository, notifier, checkoutService, discountsRepository);
        }
    }
}
