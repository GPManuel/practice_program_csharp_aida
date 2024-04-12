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
    }

    public class CoffeeMachineApp
    {
        private readonly Driver _driver;

        public CoffeeMachineApp(Driver driver)
        {
            _driver = driver;
        }

        public void SelectCoffee()
        {
            
        }

        public void MakeDrink()
        {
            _driver.SendOrder(new Order()
            {
                Drink = Drink.Coffee
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
        Coffee
    }
}