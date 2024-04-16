namespace CoffeeMachineApp.core;

public class WeirdMessageCreator : MessageCreator
{
    public WeirdMessageCreator()
    {
    }

    public Message ComposeMissingMoneyMessage(decimal missingMoney)
    {
        return Message.Create($"You are missing {missingMoney}");
    }

    public Message ComposeSelectDrinkMessage()
    {
        const string message = "Please, select a drink!";
        return Message.Create(message);
    }
}