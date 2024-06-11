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
        if (Morning().Contains(time))
        {
            return "Buenos días!";
        }
        if (Afternoon().Contains(time))
        {
            return "Buenas tardes!";
        }
        return "Buenas noches!";
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