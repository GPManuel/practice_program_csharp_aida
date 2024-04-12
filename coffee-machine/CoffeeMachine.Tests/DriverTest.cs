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

    [Test]
    public void transform_chocolate_with_one_spoon_of_sugar_into_command()
    {
        var drinkMaker = Substitute.For<DrinkMaker>();
        var givenOrder = new Order()
        {
            Drink = Drink.Chocolate,
            SpoonOfSugar = 1
        };
        var driver = new Driver800(drinkMaker);

        driver.SendOrder(givenOrder);

        drinkMaker.Received(1).Execute("H:1:0");
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
        var spoonsOfSugar = order.SpoonOfSugar > 0 ? order.SpoonOfSugar.ToString() : string.Empty;
        var useStick = order.UseStick ? "0" : string.Empty;
        _drinkMaker.Execute($"{TranslateDrinkTypeToCommandFormat(order.Drink)}:{spoonsOfSugar}:{useStick}");
    }

    private string TranslateDrinkTypeToCommandFormat(Drink drink) =>
        drink switch
        {
            Drink.Coffee => "C",
            Drink.Tea => "T",
            Drink.Chocolate => "H",
            _ => throw new NotImplementedException()
        };
}