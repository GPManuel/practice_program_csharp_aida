using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests
{
    public class CoffeeMachineTest
    {
        [Test]
        public void order_coffee()
        {
            //Given
            var driver = Substitute.For<Driver>();
            var coffeeMachine = new CoffeeMachineApp(driver);
            var expectedOrder = new Order()
            {
                Drink = Drink.Coffee
            };

            //When
            coffeeMachine.SelectCoffee();
            coffeeMachine.MakeDrink();

            //Then
            driver.Received(1).SendOrder(Arg.Is<Order>(o => o.Drink == expectedOrder.Drink));
        }

        [Test]
        public void order_tea()
        {
            var driver = Substitute.For<Driver>();
            var coffeeMachine = new CoffeeMachineApp(driver);
            var expectedOrder = new Order()
            {
                Drink = Drink.Tea
            };

            coffeeMachine.SelectTea();
            coffeeMachine.MakeDrink();

            driver.Received(1).SendOrder(Arg.Is<Order>(o => o.Drink == expectedOrder.Drink));
        }
    }

    public class CoffeeMachineApp
    {
        private readonly Driver _driver;

        private Drink _currentDrink;


        public CoffeeMachineApp(Driver driver)
        {
            _driver = driver;
        }

        public void SelectCoffee()
        {
            _currentDrink = Drink.Coffee;
        }

        public void SelectTea()
        {
            _currentDrink = Drink.Tea;
        }

        public void MakeDrink()
        {
            _driver.SendOrder(new Order()
            {
                Drink = _currentDrink
            });
        }
    }

    public interface Driver
    {
        void SendOrder(Order order);
    }

    public class Order
    {
        public Drink Drink { get; set; }
    }

    public enum Drink
    {
        Coffee,
        Tea
    }
}