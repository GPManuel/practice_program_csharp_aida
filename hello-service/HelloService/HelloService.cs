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

        if (TimeIsInTheMorning(time))
        {
            _notifier.Notify("Buenos d�as!");
        }

        _notifier.Notify("Buenas noches!");
    }

    private static bool TimeIsInTheMorning(TimeSpan time)
    {
        return time >= new TimeSpan(6, 0, 0) && time <= new TimeSpan(12, 0, 0);
    }
}