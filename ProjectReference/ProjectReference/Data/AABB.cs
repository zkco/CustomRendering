public class AABB
{
    public Interval x, y, z;

    public AABB() { }
    public AABB(in Interval x, in Interval y, in Interval z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public AABB(in Point3 a, in Point3 b)
    {
        x = (a.x > b.x) ? new Interval(a.x, b.x) : new Interval(b.x, a.x);
        y = (a.y > b.y) ? new Interval(a.y, b.y) : new Interval(b.y, a.y);
        z = (a.z > b.z) ? new Interval(a.z, b.z) : new Interval(b.z, a.z);
    }
    public AABB(in AABB box0, in AABB box1)
    {
        x = new Interval(box0.x, box1.x);
        y = new Interval(box0.y, box1.y);
        z = new Interval(box0.z, box1.z);
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