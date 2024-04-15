using System.Collections.Generic;
using System.Linq;
using CoffeeMachine.core;
using NSubstitute;
using NUnit.Framework;
using static CoffeeMachine.Tests.helpers.OrderBuilder;
using Message = CoffeeMachine.core.Message;

namespace CoffeeMachine.Tests.core;

public class CoffeeMachineAppTest
{
    private const string SelectDrinkMessage = "Please, select a drink!";
    private CoffeeMachineApp _coffeeMachineApp;
    private DrinkMakerDriver _drinkMakerDriver;

    [SetUp]
    public void SetUp()
    {
        _drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
    }

    [Test]
    public void Make_Chocolate()
    {
        _coffeeMachineApp = FreeCoffeeMachine();

        _coffeeMachineApp.SelectChocolate();
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().Build());
    }

    [Test]
    public void Make_Tea()
    {
        _coffeeMachineApp = FreeCoffeeMachine();

        _coffeeMachineApp.SelectTea();
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Send(Tea().Build());
    }

    [Test]
    public void Make_Coffee()
    {
        _coffeeMachineApp = FreeCoffeeMachine();

        _coffeeMachineApp.SelectCoffee();
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Send(Coffee().Build());
    }

    [Test]
    public void Make_Any_Drink_With_1_Spoon_Of_Sugar()
    {
        _coffeeMachineApp = FreeCoffeeMachine();

        _coffeeMachineApp.SelectChocolate();
        _coffeeMachineApp.AddOneSpoonOfSugar();
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(1).Build());
    }

    [Test]
    public void Make_Any_Drink_With_2_Spoons_Of_Sugar()
    {
        _coffeeMachineApp = FreeCoffeeMachine();

        _coffeeMachineApp.SelectChocolate();
        _coffeeMachineApp.AddOneSpoonOfSugar();
        _coffeeMachineApp.AddOneSpoonOfSugar();
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
    }

    [Test]
    public void Make_Any_Drink_With_More_Than_2_Spoons_Of_Sugar()
    {
        _coffeeMachineApp = FreeCoffeeMachine();

        _coffeeMachineApp.SelectChocolate();
        _coffeeMachineApp.AddOneSpoonOfSugar();
        _coffeeMachineApp.AddOneSpoonOfSugar();
        _coffeeMachineApp.AddOneSpoonOfSugar();
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
    }

    [Test]
    public void Warns_The_User_When_No_Drink_Was_Selected()
    {
        _coffeeMachineApp = FreeCoffeeMachine();

        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Notify(Message.Create(SelectDrinkMessage));
    }

    [Test]
    public void Resets_Drink_After_Making_Drink()
    {
        AfterMakingDrink();

        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received(1).Send(Arg.Any<Order>());
        _drinkMakerDriver.Received().Notify(Message.Create(SelectDrinkMessage));
    }

    [Test]
    public void Resets_Sugar_After_Making_Drink()
    {
        var ordersSent = CaptureSentOrders();
        AfterMakingDrinkWithSugar();

        _coffeeMachineApp.SelectCoffee();
        _coffeeMachineApp.MakeDrink();

        Assert.That(ordersSent.Last(), Is.EqualTo(Coffee().Build()));
    }

    [Test]
    public void make_tea_without_enough_money()
    {
        _coffeeMachineApp = PaidCoffeeMachine();

        _coffeeMachineApp.SelectTea();
        _coffeeMachineApp.AddMoney(0.2m);
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Notify(Message.Create("You are missing 0,2"));
    }

    [Test]
    public void make_coffee_without_enough_money()
    {
        _coffeeMachineApp = PaidCoffeeMachine();

        _coffeeMachineApp.SelectCoffee();
        _coffeeMachineApp.AddMoney(0.3m);
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Notify(Message.Create("You are missing 0,3"));
    }

    [Test]
    public void make_chocolate_without_enough_money()
    {
        _coffeeMachineApp = PaidCoffeeMachine();

        _coffeeMachineApp.SelectChocolate();
        _coffeeMachineApp.AddMoney(0.1m);
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Notify(Message.Create("You are missing 0,4"));
    }

    [Test]
    public void reset_amount_after_making_drink()
    {
        _coffeeMachineApp = PaidCoffeeMachine();

        _coffeeMachineApp.SelectCoffee();
        _coffeeMachineApp.AddMoney(0.6m);
        _coffeeMachineApp.MakeDrink();
        _coffeeMachineApp.SelectTea();
        _coffeeMachineApp.MakeDrink();

        _drinkMakerDriver.Received().Notify(Message.Create("You are missing 0,4"));
    }

    private CoffeeMachineApp FreeCoffeeMachine()
    {
        var prices = new Dictionary<DrinkType, decimal>()
        {
            { DrinkType.Chocolate, 0 },
            { DrinkType.Coffee, 0 },
            { DrinkType.Tea, 0 }
        };
         return new CoffeeMachineApp(_drinkMakerDriver, prices);
    }
    private CoffeeMachineApp PaidCoffeeMachine()
    {
        var prices = new Dictionary<DrinkType, decimal>()
        {
            { DrinkType.Chocolate, 0.5m },
            { DrinkType.Coffee, 0.6m },
            { DrinkType.Tea, 0.4m }
        };
        return new CoffeeMachineApp(_drinkMakerDriver, prices);
    }

    private List<Order> CaptureSentOrders()
    {
        var ordersSent = new List<Order>();
        _drinkMakerDriver.Send(Arg.Do<Order>(order => ordersSent.Add(order)));
        return ordersSent;
    }

    private void AfterMakingDrink()
    {
        _coffeeMachineApp = FreeCoffeeMachine();
        _coffeeMachineApp.SelectTea();
        _coffeeMachineApp.MakeDrink();
    }

    private void AfterMakingDrinkWithSugar()
    {
        _coffeeMachineApp = FreeCoffeeMachine();
        _coffeeMachineApp.SelectTea();
        _coffeeMachineApp.AddOneSpoonOfSugar();
        _coffeeMachineApp.MakeDrink();
    }
}