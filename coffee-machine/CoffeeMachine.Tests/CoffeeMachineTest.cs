using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests
{
    public class CoffeeMachineTest
    {
        private CoffeeMachineApp _coffeeMachine;
        private DrinkMakerDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = Substitute.For<DrinkMakerDriver>();
            _coffeeMachine = new CoffeeMachineApp(_driver);
        }

        [Test]
        public void order_coffee()
        {
            _coffeeMachine.SelectCoffee();
            _coffeeMachine.MakeDrink();

            var expectedOrder = new Order()
            {
                Drink = Drink.Coffee
            };
            _driver.Received(1).SendOrder(expectedOrder);
        }

        [Test]
        public void order_tea()
        {
            _coffeeMachine.SelectTea();
            _coffeeMachine.MakeDrink();

            var expectedOrder = new Order()
            {
                Drink = Drink.Tea
            };
            _driver.Received(1).SendOrder(expectedOrder);
        }

        [Test]
        public void order_chocolate_with_one_sugar_and_uses_stick()
        {
            _coffeeMachine.SelectChocolate();
            _coffeeMachine.AddOneSpoonOfSugar();
            _coffeeMachine.MakeDrink();

            var expectedOrder = new Order()
            {
                Drink = Drink.Chocolate,
                SpoonOfSugar = 1
            };
            _driver.Received(1).SendOrder(expectedOrder);
        }

        [Test]
        public void order_drink_with_two_sugar_and_uses_stick()
        {
            _coffeeMachine.SelectChocolate();
            _coffeeMachine.AddOneSpoonOfSugar();
            _coffeeMachine.AddOneSpoonOfSugar();
            _coffeeMachine.MakeDrink();

            var expectedOrder = new Order()
            {
                Drink = Drink.Chocolate,
                SpoonOfSugar = 2
            };
            _driver.Received(1).SendOrder(expectedOrder);
        }

        [Test]
        public void order_drink_with_two_spoons_when_user_orders_more_than_two_spoons()
        {
            _coffeeMachine.SelectChocolate();
            _coffeeMachine.AddOneSpoonOfSugar();
            _coffeeMachine.AddOneSpoonOfSugar();
            _coffeeMachine.AddOneSpoonOfSugar();
            _coffeeMachine.MakeDrink();

            var expectedOrder = new Order()
            {
                Drink = Drink.Chocolate,
                SpoonOfSugar = 2
            };
            _driver.Received(1).SendOrder(expectedOrder);
        }
    }
}