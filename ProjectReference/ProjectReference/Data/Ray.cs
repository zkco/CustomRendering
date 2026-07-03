public struct Ray
{
    public Point3 origin { get; private set; }
    public Vector3 direction { get; private set; }

    public Ray() { } //Default 생성자
    public Ray(in Point3 origin, in Vector3 direction)
    {
        this.origin = origin;
        this.direction = direction;
    }

    public Point3 At(double t)
    {
        return origin + direction * t;
    }

    public Color RayColor()
    {
        double t = HitSphere(new Point3(0, 0, -1), 0.5, this);
        if(t > 0.0)
        {
            Vector3 N = (At(t) - new Point3(0, 0, -1)).normalized;
            return 0.5 * new Color(N.x + 1, N.y + 1, N.z + 1);
        }

        Vector3 unitDirection = this.direction.normalized;
        double a = 0.5 * (unitDirection.y + 1.0);
        return (1.0 - a)* new Color(1.0, 1.0, 1.0) + a * new Color(0.5, 0.7, 1.0);
    }

    private double HitSphere(in Point3 center, double radius, in Ray r)
    {
        //(C-Q)
        Vector3 oc = center - r.origin;
        //x^2 + y^2 + z^2 = ray.magnitude^2
        double a = Vector3.Dot(r.direction, r.direction);
        //r.direction * (C-Q) (내적의 곱)
        double h = Vector3.Dot(r.direction,oc);
        //(C-Q)^2 - radius^2
        double c = Vector3.Dot(oc, oc) - radius * radius; 
        double discriminant = h * h - a * c;

        if (discriminant < 0) return -1.0;
        else return (h - Math.Sqrt(discriminant)) / a;
    }
}