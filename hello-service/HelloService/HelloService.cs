using System;

namespace Hello;

public class HelloService
{
    private readonly Notifier _notifier;
    private readonly Clock _clock;

    public HelloService(Notifier notifier, Clock clock)
    {
        _notifier = notifier;
        _clock = clock;
    }

    public void Hello()
    {
        var time = _clock.WhatTimeItIs();
        var greeting = GetGreetingMessage(time);
        _notifier.Notify(greeting);
    }

    private static string GetGreetingMessage(TimeOnly time)
    {
        string greeting;
        if (Morning().Contains(time))
        {
            greeting = "Buenos días!";
        }
        else if (Afternoon().Contains(time))
        {
            greeting = "Buenas tardes!";
        }
        else
        {
            greeting = "Buenas noches!";
        }

        return greeting;
    }

    private static TimeInterval Afternoon()
    {
        return new TimeInterval(new TimeOnly(12, 0, 0), new TimeOnly(20, 0, 0));
    }

    private static TimeInterval Morning()
    {
        return new TimeInterval(new TimeOnly(6, 0, 0), new TimeOnly(12, 0, 0));
    }
}