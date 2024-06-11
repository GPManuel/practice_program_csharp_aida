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
            _notifier.Notify("Buenos días!");
        }

        if (TimeIsInTheAfternoon(time))
        {
            _notifier.Notify("Buenas tardes!");
        }

        _notifier.Notify("Buenas noches!");
    }

    private static bool TimeIsInTheMorning(TimeOnly time)
    {
        return time >= new TimeOnly(6, 0, 0) && time <= new TimeOnly(12, 0, 0);
    }

    private static bool TimeIsInTheAfternoon(TimeOnly time)
    {
        return time > new TimeOnly(12, 0, 0) && time < new TimeOnly(20, 0, 0);
    }
}