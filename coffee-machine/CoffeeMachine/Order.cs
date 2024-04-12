namespace CoffeeMachine;

public record Order
{
    private const int MaxSugar = 2;

    public Drink Drink { get; set; }
    public int SpoonOfSugar { get; set; }
    public bool UseStick => SpoonOfSugar > 0;

    public void AddSpoonOfSugar()
    {
        if (SpoonOfSugar == MaxSugar)
            return;
        SpoonOfSugar++;
    }
}