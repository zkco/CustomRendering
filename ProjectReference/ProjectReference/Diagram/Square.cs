public class Square : IHittable
{
    private Point3 Q;
    private Vector3 u;
    private Vector3 v;
    private Vector3 w;
    private Vector3 normal;
    private double D;
    private Material mat;
    private AABB bbox;

    public Square(Point3 Q, Vector3 u, Vector3 v, Material mat)
    {
        this.Q = Q;
        this.u = u;
        this.v = v;
        this.mat = mat;

        Vector3 n = Vector3.Cross(u, v);
        normal = n.normalized;
        D = Vector3.Dot(normal, Q);
        w = n / Vector3.Dot(n, n);
        SetBoundingBox();
    }

    private void SetBoundingBox()
    {
        AABB bbox1 = new AABB(Q, Q + u + v);
        AABB bbox2 = new AABB(Q + u, Q + v);
        this.bbox = new AABB(bbox1, bbox2);
    }

    public AABB BoundingBox()
    {
        return bbox;
    }

    public bool Hit(in Ray r, Interval rayT, out HitRecord rec)
    {
        rec = new HitRecord();
        HitRecord tempRec = new HitRecord();

        double denom = Vector3.Dot(normal, r.direction);
        if (Math.Abs(denom) < 1e-8)
            return false;

        double t = (D - Vector3.Dot(normal, r.origin)) / denom;
        if (!rayT.Contains(t))
            return false;

        Vector3 intersection = r.At(t);
        Vector3 hitPointVector = intersection - Q;
        double alpha = Vector3.Dot(w, Vector3.Cross(hitPointVector, v));
        double beta = Vector3.Dot(w, Vector3.Cross(u, hitPointVector));

        if (!IsInterior(alpha, beta, out tempRec)) return false;

        rec = tempRec;
        rec.t = t;
        rec.p = intersection;
        rec.mat = mat;
        rec.SetFaceNormal(r, normal);
        
        return true;
    }

    public bool IsInterior(double a, double b, out HitRecord rec)
    {
        rec = new HitRecord();
        Interval unitInterval = new Interval(0, 1);

        if (!unitInterval.Contains(a) || !unitInterval.Contains(b)) return false;

        rec.u = a;
        rec.v = b;
        return true;
    }
}