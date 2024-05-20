using System.Collections.Generic;

namespace ShoppingCart;

public class Summary
{
    private readonly IEnumerable<Line> _displayedLines;

    public Summary(IEnumerable<Line> displayedLines)
    {
        _displayedLines = displayedLines;
    }

    protected bool Equals(Summary other)
    {
        return Equals(_displayedLines, other._displayedLines);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Summary)obj);
    }

    public override int GetHashCode()
    {
        return (_displayedLines != null ? _displayedLines.GetHashCode() : 0);
    }

    public override string ToString()
    {
        return $"{nameof(_displayedLines)}: {_displayedLines}";
    }
}