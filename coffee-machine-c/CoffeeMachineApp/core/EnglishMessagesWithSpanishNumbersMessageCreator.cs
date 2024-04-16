using System.Globalization;
using System.Net.Http.Headers;

namespace CoffeeMachineApp.core;

public class EnglishMessagesWithSpanishNumbersMessageCreator : MessageCreator
{
    public Message ComposeMissingMoneyMessage(decimal missingMoney)
    {
        var cultureInfo = new CultureInfo("es-ES");
        var result = missingMoney.ToString(cultureInfo);
        return Message.Create($"You are missing {result}");
    }

    public Message ComposeSelectDrinkMessage()
    {
        const string message = "Please, select a drink!";
        return Message.Create(message);
    }
}