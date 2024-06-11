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

        if (time >= new TimeSpan(6, 0, 0) && time <= new TimeSpan(12, 0, 0))
        {
            _notifier.Notify("Buenos días!");
        }

        _notifier.Notify("Buenas noches!");
    }
}