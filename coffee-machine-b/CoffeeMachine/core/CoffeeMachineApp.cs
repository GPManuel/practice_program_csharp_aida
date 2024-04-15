using System.Collections.Generic;

namespace CoffeeMachine.core;

public class CoffeeMachineApp
{
    private readonly DrinkMakerDriver _drinkMakerDriver;
    private readonly Dictionary<DrinkType, decimal> _prices;
    private Order _order;
    private decimal _totalMoney;

    public CoffeeMachineApp(DrinkMakerDriver drinkMakerDriver, Dictionary<DrinkType, decimal> prices)
    {
        _drinkMakerDriver = drinkMakerDriver;
        _order = new Order();
        _prices = prices;
    }

    public void SelectChocolate()
    {
        _order.SelectDrink(DrinkType.Chocolate);
    }

    public void SelectTea()
    {
        _order.SelectDrink(DrinkType.Tea);
    }

    public void SelectCoffee()
    {
        _order.SelectDrink(DrinkType.Coffee);
    }

    public void AddOneSpoonOfSugar()
    {
        _order.AddSpoonOfSugar();
    }

    public void AddMoney(decimal money)
    {
        _totalMoney += money;
    }

    public void MakeDrink()
    {
        if (NoDrinkWasSelected())
        {
            _drinkMakerDriver.Notify(SelectDrinkMessage());
            return;
        }

        if (IsThereEnoughMoney())
        {
            _drinkMakerDriver.Send(_order);
            _order = new Order();
            return;
        }

        _drinkMakerDriver.Notify(Message.Create($"You are missing {ComputeMissingMoney()}"));
    }

    private double ComputeMissingMoney()
    {
        return 0.2;
    }

    private bool IsThereEnoughMoney()
    {
        return _totalMoney >= _prices[_order.GetDrinkType()];
    }

    private bool NoDrinkWasSelected()
    {
        return _order.GetDrinkType() == DrinkType.None;
    }

    private Message SelectDrinkMessage()
    {
        const string message = "Please, select a drink!";
        return Message.Create(message);
    }
}