public struct Interval
{
    public double min;
    public double max;

    public Interval()
    {
        this.min = double.PositiveInfinity;
        this.max = double.NegativeInfinity;
    }

    public Interval(double min, double max)
    {
        this.min = min;
        this.max = max;
    }

    public Interval (Interval a, Interval b)
    {
        this.min = a.min <= b.min ? a.min : b.min;
        this.max = a.max >= b.max ? a.max : b.max;
    }

    public double Size() => max - min;
    public bool Contains(double x) => x >= min && x <= max;
    public bool Surrounds(double x) => x > min && x < max;
    public double Clamp(double x) => Math.Max(min, Math.Min(max, x));
    public Interval Expand(double delta) => new Interval(min - delta / 2, max + delta / 2);

    public Interval empty => new Interval(double.PositiveInfinity, double.NegativeInfinity);
    public Interval universe => new Interval(double.NegativeInfinity, double.PositiveInfinity);
}