public readonly struct Interval
{
    public readonly double min;
    public readonly double max;

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

    public double Size() => max - min;
    public bool Contains(double x) => x >= min && x <= max;
    public bool Surrounds(double x) => x > min && x < max;
    public double Clamp(double x) => Math.Max(min, Math.Min(max, x));

    readonly public Interval empty => new Interval(double.PositiveInfinity, double.NegativeInfinity);
    readonly public Interval universe => new Interval(double.NegativeInfinity, double.PositiveInfinity);
}