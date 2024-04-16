namespace CoffeeMachineApp.core;

public interface MessageCreator
{
    public Message ComposeMissingMoneyMessage(decimal missingMoney);
    public Message ComposeSelectDrinkMessage();
}