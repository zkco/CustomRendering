using System.Reflection.Metadata.Ecma335;

public struct BVHNode : IHittable
{
    private IHittable left;
    private IHittable right;
    private AABB bbox;

    public BVHNode(HittableList list)
    {
        this = new BVHNode(list.objects, 0, list.objects.Count);
    }

    public BVHNode(List<IHittable> objects, int start, int end)
    {
        int axis = Random.Shared.Next(0, 3);

        //축 정렬 기준 생성
        IComparer<IHittable> comparator = Comparer<IHittable>.Create((a, b) =>
        {
            Interval aInterval = a.BoundingBox().AxisInterval(axis);
            Interval bInterval = b.BoundingBox().AxisInterval(axis);
            return aInterval.min.CompareTo(bInterval.min);
        });

        int objectSpan = end - start;
        if (objectSpan == 1)
        {
            left = right = objects[start];
        }
        else if (objectSpan == 2)
        {
            left = objects[start];
            right = objects[start + 1];
        }
        else
        {
            objects.Sort(start, objectSpan, comparator);

            int mid = start + objectSpan / 2;
            left = new BVHNode(objects, start, mid);
            right = new BVHNode(objects, mid, end);
        }

        bbox = new AABB(left.BoundingBox(), right.BoundingBox());
    }

    public bool Hit(in Ray r, Interval rayT, out HitRecord rec)
    {
        rec = new HitRecord();
        if (!bbox.Hit(r, rayT)) return false;

        bool hitLeft = left.Hit(r, rayT, out HitRecord leftRec);
        double rightMax = hitLeft ? leftRec.t : rayT.max;
        bool hitRight = right.Hit(r, new Interval(rayT.min, rightMax), out HitRecord rightRec);

        rec = hitRight ? rightRec : hitLeft ? leftRec : rec;
        return hitRight || hitLeft;
    }

    public AABB BoundingBox()
    {
        return bbox;
    }
}