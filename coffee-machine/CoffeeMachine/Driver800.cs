using System;

namespace CoffeeMachine.Tests;

public class Driver800 : DrinkMakerDriver
{
    private readonly DrinkMaker _drinkMaker;

    public Driver800(DrinkMaker drinkMaker)
    {
        _drinkMaker = drinkMaker;
    }

    public void SendOrder(Order order)
    {
        _drinkMaker.Execute(TranslateOrder(order));
    }

    private string TranslateOrder(Order order)
    {
        var spoonsOfSugar = order.SpoonOfSugar > 0 ? order.SpoonOfSugar.ToString() : string.Empty;
        var useStick = order.UseStick ? "0" : string.Empty;
        return $"{TranslateDrinkTypeToCommandFormat(order.Drink)}:{spoonsOfSugar}:{useStick}";
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