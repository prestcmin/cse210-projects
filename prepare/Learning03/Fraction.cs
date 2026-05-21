public class Fraction
{
    private int _top;
    private int _bottom;

    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    public Fraction(int wholeNumber)
    {
        _top = wholeNumber;
        _bottom = 1;
    }

    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    public int GetTop()
    {
        return _top;
    }

    public void SetTop(int top)
    {
        if (top != 0)
        {
            _top = top;
        }
        else
        {
            _top = 1;
        }
    }

    public int GetBottom()
    {
        return _bottom;
    }

    public void SetBottom(int bottom)
    {
        if (bottom != 0)
        {
            _bottom = bottom;
        }
        else
        {
            _bottom = 1;
        }
    }

    public string GetFractionString()
    {
       string fractionString = $"{_top}/{_bottom}";
       return fractionString;
    }

    public double GetDecimalValue()
    {
        return (double)_top / (double)_bottom;
    }
}