public class AABB
{
    public Interval x, y, z;

    public AABB() { }
    public AABB(in Interval x, in Interval y, in Interval z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        PadToMinimums();
    }

    public AABB(in Point3 a, in Point3 b)
    {
        x = new Interval(Math.Min(a.x, b.x), Math.Max(a.x, b.x));
        y = new Interval(Math.Min(a.y, b.y), Math.Max(a.y, b.y));
        z = new Interval(Math.Min(a.z, b.z), Math.Max(a.z, b.z));
        PadToMinimums();
    }

    public AABB(in AABB box0, in AABB box1)
    {
        x = new Interval(Math.Min(box0.x.min, box1.x.min), Math.Max(box0.x.max, box1.x.max));
        y = new Interval(Math.Min(box0.y.min, box1.y.min), Math.Max(box0.y.max, box1.y.max));
        z = new Interval(Math.Min(box0.z.min, box1.z.min), Math.Max(box0.z.max, box1.z.max));
        PadToMinimums();
    }
    private void PadToMinimums()
    {
        double delta = 0.0001;
        if (x.Size() < delta) x = x.Expand(delta);
        if (y.Size() < delta) y = y.Expand(delta);
        if (z.Size() < delta) z = z.Expand(delta);
    }

    public Interval AxisInterval(int n)
    {
        if (n == 1) return y;
        if (n == 2) return z;
        return x;
    }

    public bool Hit(in Ray r, Interval rayT)
    {
        Point3 rOrigin = r.origin;
        Vector3 rDir = r.direction;

        for(int axis = 0; axis < 3; axis++)
        {
            Interval ax = AxisInterval(axis);
            double adinv = 1.0 / rDir[axis];

            double t0 = (ax.min - rOrigin[axis]) * adinv;
            double t1 = (ax.max - rOrigin[axis]) * adinv;

            if(t0 < t1)
            {
                if (t0 > rayT.min) rayT.min = t0;
                if (t1 < rayT.max) rayT.max = t1;
            }
            else
            {
                if (t1 > rayT.min) rayT.min = t1;
                if (t0 < rayT.max) rayT.max = t0;
            }

            if (rayT.max <= rayT.min) return false;
        }
        return true;
    }
}