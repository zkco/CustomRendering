public class Sphere : IHittable
{
    private Point3 center;
    private double radius;

    public Sphere(Point3 center, double radius)
    {
        this.center = center;
        this.radius = radius;
    }

    public bool Hit(in Ray r, Interval rayT, out HitRecord rec)
    {
        rec = new HitRecord();
        Vector3 oc = center - r.origin;
        double a = Vector3.Dot(r.direction, r.direction);
        double h = Vector3.Dot(r.direction, oc);
        double c = Vector3.Dot(oc, oc) - radius * radius;
        double discriminant = h * h - a * c;

        if (discriminant < 0) return false;

        double sqrtD = Math.Sqrt(discriminant);

        double root = (h - sqrtD) / a;
        if(root <= rayT.min || rayT.max <= root)
        {
            root = (h + sqrtD) / a;
            if (root <= rayT.min || rayT.max <= root) return false;
        }

        rec.t = root;
        rec.p = r.At(rec.t);
        Vector3 outwardNormal = (rec.p - center) / radius;
        rec.SetFaceNormal(r, outwardNormal);

        return true;
    }
}