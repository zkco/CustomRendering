public interface IHittable
{
    bool Hit(in Ray r, Interval rayT, out HitRecord rec);
}

public struct HitRecord
{
    public Point3 p;
    public Vector3 normal;
    public double t;
    public bool frontFace;
    public void SetFaceNormal(in Ray r, in Vector3 outwardNormal)
    {
        frontFace = Vector3.Dot(r.direction, outwardNormal) < 0;
        normal = frontFace ? outwardNormal : -outwardNormal;
        
    }
}

public struct HittableList : IHittable
{
    public List<IHittable> objects { get; set; }

    public HittableList()
    {
        objects = new List<IHittable>();
    }
    public HittableList(IHittable obj)
    {
        objects = new List<IHittable>();
        objects.Add(obj);
    }

    public void Clear()
    {
        objects.Clear();
    }

    public void add(IHittable obj)
    {
        objects.Add(obj);
    }

    public bool Hit(in Ray r, Interval rayT, out HitRecord rec)
    {
        rec = new HitRecord();
        bool hitAnything = false;
        double closestSoFar = rayT.max;

        HitRecord tempRec;

        foreach(IHittable obj in objects)
        {
            if (obj.Hit(r, new Interval(rayT.min, closestSoFar), out tempRec))
            {
                hitAnything = true;
                closestSoFar = rec.t;
                rec = tempRec;
            }
        }
        return hitAnything;
    }
}