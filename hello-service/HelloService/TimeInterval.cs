using System;

namespace Hello;

internal class TimeInterval
{
    private readonly TimeOnly _startTime;
    private readonly TimeOnly _endTime;

    public TimeInterval(TimeOnly startTime, TimeOnly endTime)
    {
        _startTime = startTime;
        _endTime = endTime;
    }

    public bool Contains(TimeOnly time)
    {
        return time >= _startTime && time <= _endTime;
    }
}