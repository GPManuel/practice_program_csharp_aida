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

            //When
            coffeeMachine.SelectCoffee();
            coffeeMachine.MakeDrink();

            //Then
            var expectedOrder = new Order()
            {
                Drink = Drink.Coffee
            };
            driver.Received(1).SendOrder(expectedOrder);
        }

        [Test]
        public void order_tea()
        {
            var driver = Substitute.For<Driver>();
            var coffeeMachine = new CoffeeMachineApp(driver);
            
            coffeeMachine.SelectTea();
            coffeeMachine.MakeDrink();

            var expectedOrder = new Order()
            {
                Drink = Drink.Tea
            };
            driver.Received(1).SendOrder(expectedOrder);
        }

        [Test]
        public void order_chocolate_with_one_sugar_and_uses_stick()
        {
            var driver = Substitute.For<Driver>();
            var coffeeMachine = new CoffeeMachineApp(driver);

            coffeeMachine.SelectChocolate();
            coffeeMachine.AddOneSpoonOfSugar();
            coffeeMachine.MakeDrink();

            var expectedOrder = new Order()
            {
                Drink = Drink.Chocolate,
                SpoonOfSugar = 1
            };
            driver.Received(1).SendOrder(expectedOrder);
        }
    }

    public class CoffeeMachineApp
    {
        private readonly Driver _driver;

        private Drink _currentDrink;
        private int _spoonOfSugar;


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

        public void SelectChocolate()
        {
            _currentDrink = Drink.Chocolate;
        }

        public void AddOneSpoonOfSugar()
        {
            _spoonOfSugar = 1;
        }

        public void MakeDrink()
        {
            _driver.SendOrder(new Order()
            {
                Drink = _currentDrink,
                SpoonOfSugar = _spoonOfSugar
            });
        }
    }

    public interface Driver
    {
        void SendOrder(Order order);
    }

    public record Order
    {
        public Drink Drink { get; set; }
        public int SpoonOfSugar { get; set; }
        public bool UseStick => SpoonOfSugar > 0;
    }

    public enum Drink
    {
        Coffee,
        Tea,
        Chocolate
    }
}