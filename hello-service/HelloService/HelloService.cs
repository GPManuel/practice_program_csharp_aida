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

        if (Morning().Contains(time))
        {
            _notifier.Notify("Buenos días!");
            return;
        }

        if (Afternoon().Contains(time))
        {
            _notifier.Notify("Buenas tardes!");
            return;
        }

        _notifier.Notify("Buenas noches!");
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