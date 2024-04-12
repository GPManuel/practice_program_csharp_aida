using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests;

public class DriverTest
{
    private Driver800 _driver;
    private DrinkMaker _drinkMaker;

    [SetUp]
    public void SetUp()
    {
        _drinkMaker = Substitute.For<DrinkMaker>();
        _driver = new Driver800(_drinkMaker);
    }

    [Test]
    public void transform_coffee_order_into_command()
    {
        var givenOrder = new Order()
        {
            Drink = Drink.Coffee
        };

        _driver.SendOrder(givenOrder);
        _drinkMaker.Received(1).Execute("C::");
    }

    [Test]
    public void transform_tea_order_into_command()
    {
        var givenOrder = new Order()
        {
            Drink = Drink.Tea
        };

        _driver.SendOrder(givenOrder);

        _drinkMaker.Received(1).Execute("T::");
    }

    [TestCase(1)]
    [TestCase(2)]
    public void transform_chocolate_with_one_spoon_of_sugar_into_command(int spoons)
    {
        var givenOrder = new Order()
        {
            Drink = Drink.Chocolate,
            SpoonOfSugar = spoons
        };

        _driver.SendOrder(givenOrder);

        _drinkMaker.Received(1).Execute($"H:{spoons}:0");
    }
}