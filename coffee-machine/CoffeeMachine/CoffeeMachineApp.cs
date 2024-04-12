namespace CoffeeMachine;

public class CoffeeMachineApp
{
    private const int MaxSugarSpoons = 2;
    private readonly DriverDrinkMaker _driver;

    private Drink _currentDrink;
    private int _spoonOfSugar;

    public CoffeeMachineApp(DriverDrinkMaker driver)
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
        if (_spoonOfSugar == MaxSugarSpoons)
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