namespace CoffeeMachine;

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
        if (_spoonOfSugar == 2)
            return;
        _spoonOfSugar++;
    }

    public void MakeDrink()
    {
        _driver.SendOrder(GenerateOrder());
    }

    private Order GenerateOrder()
    {
        return new Order()
        {
            Drink = _currentDrink,
            SpoonOfSugar = _spoonOfSugar
        };
    }
}