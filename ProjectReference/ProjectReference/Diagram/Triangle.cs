public class Triangle : IHittable
{
    private Point3 A, B, C;
    private Vector3 u, v;
    private Vector3 normal;
    private Material mat;
    private AABB bbox;

    public Triangle(Point3 A, Point3 B, Point3 C, Vector3 normal, Material mat)
    {
        this.A = A;
        this.B = B;
        this.C = C;
        this.u = B - A;
        this.v = C - A;
        this.normal = normal;
        this.mat = mat;
        SetBoundingBox();
    }

    public Triangle(Point3 A, Point3 B, Point3 C, Material mat)
    {
        this.A = A;
        this.B = B;
        this.C = C;
        this.u = B - A;
        this.v = C - A;
        this.normal = Vector3.Cross(u, v);
        this.mat = mat;
        SetBoundingBox();
    }

    private void SetBoundingBox()
    {
        AABB bbox1 = new AABB(A, B);
        AABB bbox2 = new AABB(A, C);
        this.bbox = new AABB(bbox1, bbox2);
    }

    public AABB BoundingBox()
    {
        return bbox;
    }

    public bool Hit(in Ray r, Interval rayT, out HitRecord rec)
    {
        rec = new HitRecord();

        Vector3 p = Vector3.Cross(r.direction, v);
        double det = Vector3.Dot(u, p);

        if (Math.Abs(det) < 1e-8) return false;

        double invdet = 1.0 / det;
        Vector3 s = r.origin - A;

        double beta = Vector3.Dot(s, p) * invdet;
        if (beta < 0.0 || beta > 1.0) return false;

        Vector3 q = Vector3.Cross(s, u);
        double gamma = Vector3.Dot(r.direction, q) * invdet;

        if (gamma < 0.0 || beta + gamma > 1.0) return false;
        double t = Vector3.Dot(v, q) * invdet;

        if (!rayT.Contains(t)) return false;

        rec.t = t;
        rec.p = r.At(t);
        rec.mat = mat;
        rec.SetFaceNormal(r, normal);

        return true;
    }
}