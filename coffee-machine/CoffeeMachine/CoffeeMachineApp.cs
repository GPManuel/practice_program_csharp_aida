namespace CoffeeMachine;

public class CoffeeMachineApp
{
    private readonly DrinkMakerDriver _driver;

    private readonly Order _currentOrder;

    public CoffeeMachineApp(DrinkMakerDriver driver)
    {
        _driver = driver;

        _currentOrder = new Order();
    }

    public void SelectCoffee()
    {
        _currentOrder.Drink = Drink.Coffee;
    }

    public void SelectTea()
    {
        _currentOrder.Drink = Drink.Tea;
    }

    public void SelectChocolate()
    {
        _currentOrder.Drink = Drink.Chocolate;
    }

    public void AddOneSpoonOfSugar()
    {
        _currentOrder.AddSpoonOfSugar();
    }

    public void MakeDrink()
    {
        _driver.SendOrder(_currentOrder);
    }
}