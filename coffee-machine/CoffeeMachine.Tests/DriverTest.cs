using System;
using NSubstitute;
using NUnit.Framework;

namespace CoffeeMachine.Tests;

public class DriverTest
{
    [Test]
    public void transform_coffee_order_into_command()
    {
        var drinkMaker = Substitute.For<DrinkMaker>();
        var givenOrder = new Order()
        {
            Drink = Drink.Coffee
        };
        var driver = new Driver800(drinkMaker);

        driver.SendOrder(givenOrder);

        drinkMaker.Received(1).Execute("C::");
    }

    [Test]
    public void transform_tea_order_into_command()
    {
        var drinkMaker = Substitute.For<DrinkMaker>();
        var givenOrder = new Order()
        {
            Drink = Drink.Tea
        };
        var driver = new Driver800(drinkMaker);

        driver.SendOrder(givenOrder);

        drinkMaker.Received(1).Execute("T::");
    }
}

public class Driver800 : Driver
{
    private readonly DrinkMaker _drinkMaker;

    public Driver800(DrinkMaker drinkMaker)
    {
        _drinkMaker = drinkMaker;
    }

    public void SendOrder(Order order)
    {
        _drinkMaker.Execute($"{TranslateDrinkTypeToCommandFormat(order.Drink)}::");
    }

    private string TranslateDrinkTypeToCommandFormat(Drink drink) =>
        drink switch
        {
            Drink.Coffee => "C",
            Drink.Tea => "T",
            _ => throw new NotImplementedException()
        };
}