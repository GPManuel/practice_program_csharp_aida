using System.Collections.Generic;
using System.Linq;
using CoffeeMachineApp.core;
using NSubstitute;
using NUnit.Framework;
using static CoffeeMachineApp.Tests.helpers.OrderBuilder;

namespace CoffeeMachineApp.Tests.core;

public class CoffeeMachineTest
{
    private const string SelectDrinkMessage = "Please, select a drink!";
    private const decimal TeaPrice = 0.4m;
    private const decimal CoffeePrice = 0.6m;
    private const decimal ChocolatePrice = 0.5m;
    private CoffeeMachine _coffeeMachine;
    private DrinkMakerDriver _drinkMakerDriver;
    private Dictionary<DrinkType, decimal> _pricesByDrinkType;
    private MessageCreator _messageCreator;

    [SetUp]
    public void SetUp()
    {
        _messageCreator = Substitute.For<MessageCreator>();
        _drinkMakerDriver = Substitute.For<DrinkMakerDriver>();
        _pricesByDrinkType = new Dictionary<DrinkType, decimal>()
        {
            { DrinkType.Chocolate, ChocolatePrice },
            { DrinkType.Coffee, CoffeePrice },
            { DrinkType.Tea, TeaPrice }
        };
    }

    [Test]
    public void Make_Chocolate()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().Build());
    }

    [Test]
    public void Make_Tea()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Tea().Build());
    }

    [Test]
    public void Make_Coffee()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectCoffee();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Coffee().Build());
    }

    [Test]
    public void Make_Any_Drink_With_1_Spoon_Of_Sugar()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(1).Build());
    }

    [Test]
    public void Make_Any_Drink_With_2_Spoons_Of_Sugar()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
    }

    [Test]
    public void Make_Any_Drink_With_More_Than_2_Spoons_Of_Sugar()
    {
        _coffeeMachine = FreeCoffeeMachine();

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Chocolate().WithSpoonsOfSugar(2).Build());
    }

    [Test]
    public void Warns_The_User_When_No_Drink_Was_Selected()
    {
        _coffeeMachine = FreeCoffeeMachine();
        var message = GetSelectDrinkMessage();

        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Notify(message);
    }

    [Test]
    public void Resets_Drink_After_Making_Drink()
    {
        AfterMakingDrink();
        var message = GetSelectDrinkMessage();

        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received(1).Send(Arg.Any<Order>());
        _drinkMakerDriver.Received().Notify(message);
    }

    [Test]
    public void Resets_Sugar_After_Making_Drink()
    {
        var ordersSent = CaptureSentOrders();
        AfterMakingDrinkWithSugar();

        _coffeeMachine.SelectCoffee();
        _coffeeMachine.MakeDrink();

        Assert.That(ordersSent.Last(), Is.EqualTo(Coffee().Build()));
    }

    [Test]
    public void Make_Tea_With_Not_Enough_Money()
    {
        var amount = 0.2m;
        _coffeeMachine = PaidCoffeeMachine();
        var missingMoney= TeaPrice - amount;
        var message = GetMissingMoneyMessage(missingMoney);

        _coffeeMachine.SelectTea();
        _coffeeMachine.AddMoney(amount);
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Notify(message);
    }

    [Test]
    public void Make_Coffee_With_Not_Enough_Money()
    {
        var amount = 0.3m;
        _coffeeMachine = PaidCoffeeMachine();
        var missingMoney = CoffeePrice - amount;
        var message = GetMissingMoneyMessage(missingMoney);

        _coffeeMachine.SelectCoffee();
        _coffeeMachine.AddMoney(amount);
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Notify(message);
    }

    [Test]
    public void Make_Chocolate_With_Not_Enough_Money()
    {
        var amount = 0.1m;
        _coffeeMachine = PaidCoffeeMachine();
        var missingMoney = ChocolatePrice - amount;
        var message = GetMissingMoneyMessage(missingMoney);

        _coffeeMachine.SelectChocolate();
        _coffeeMachine.AddMoney(amount);
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Notify(message);
    }

    [Test]
    public void Make_Tea_With_Enough_Money()
    {
        _coffeeMachine = PaidCoffeeMachine();

        _coffeeMachine.SelectTea();
        _coffeeMachine.AddMoney(0.2m);
        _coffeeMachine.AddMoney(0.2m);
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Send(Tea().Build());
    }

    [Test]
    public void Reset_Money_After_Making_Drink()
    {
        AfterPayingAndMakingDrink();
        var message = GetMissingMoneyMessage(TeaPrice);

        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();

        _drinkMakerDriver.Received().Notify(message);
    }

    private void AfterPayingAndMakingDrink()
    {
        _coffeeMachine = PaidCoffeeMachine();
        _coffeeMachine.SelectCoffee();
        _coffeeMachine.AddMoney(CoffeePrice);
        _coffeeMachine.MakeDrink();
    }

    private CoffeeMachine FreeCoffeeMachine()
    {
        var prices = new Dictionary<DrinkType, decimal>()
        {
            { DrinkType.Chocolate, 0 },
            { DrinkType.Coffee, 0 },
            { DrinkType.Tea, 0 }
        };
        return new CoffeeMachine(_drinkMakerDriver, prices, _messageCreator);
    }

    private CoffeeMachine PaidCoffeeMachine()
    {
        var prices = _pricesByDrinkType;
        return new CoffeeMachine(_drinkMakerDriver, prices, _messageCreator);
    }

    private Message GetMissingMoneyMessage(decimal missingMoney)
    {
        var message = Message.Create($"You are missing {missingMoney}");
        _messageCreator.ComposeMissingMoneyMessage(missingMoney).Returns(message);
        return message;
    }

    private Message GetSelectDrinkMessage()
    {
        var message = Message.Create(SelectDrinkMessage);
        _messageCreator.ComposeSelectDrinkMessage().Returns(message);
        return message;
    }

    private List<Order> CaptureSentOrders()
    {
        var ordersSent = new List<Order>();
        _drinkMakerDriver.Send(Arg.Do<Order>(order => ordersSent.Add(order)));
        return ordersSent;
    }

    private void AfterMakingDrink()
    {
        _coffeeMachine = FreeCoffeeMachine();
        _coffeeMachine.SelectTea();
        _coffeeMachine.MakeDrink();
    }

    private void AfterMakingDrinkWithSugar()
    {
        _coffeeMachine = FreeCoffeeMachine();
        _coffeeMachine.SelectTea();
        _coffeeMachine.AddOneSpoonOfSugar();
        _coffeeMachine.MakeDrink();
    }
}