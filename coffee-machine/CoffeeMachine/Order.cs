namespace CoffeeMachine;

public record Order
{
    public Drink Drink { get; set; }
    public int SpoonOfSugar { get; set; }
    public bool UseStick => SpoonOfSugar > 0;
}