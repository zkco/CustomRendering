public class Material
{
    public virtual bool Scatter(out Ray r, out HitRecord rec, ref Color attenuation, ref Ray scattered)
    {
        r = new Ray();
        rec = new HitRecord();
        return false;
    }
}