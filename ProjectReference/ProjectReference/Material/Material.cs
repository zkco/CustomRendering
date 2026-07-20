public abstract class Material
{
    public Color albedo;
    public abstract bool Scatter(in Ray r, in HitRecord rec, out Color attenuation, out Ray scattered);
}

public class Lambertian : Material
{
    public Lambertian(Color albedo)
    {
        this.albedo = albedo;
    }

    public override bool Scatter(in Ray r, in HitRecord rec, out Color attenuation, out Ray scattered)
    {
        Vector3 scatterDir = rec.normal + Vector3.RandomUnitVector();
        if (Math.Abs(scatterDir.x) < 1e-8 && Math.Abs(scatterDir.y) < 1e-8
            && Math.Abs(scatterDir.z) < 1e-8) scatterDir = rec.normal;
        scattered = new Ray(rec.p, scatterDir);
        attenuation = albedo;
        return true;
    }
}

public class Metal : Material
{
    private double fuzz;

    public Metal(Color albedo, double fuzz)
    {
        this.albedo = albedo;
        this.fuzz = fuzz < 1 ? fuzz : 1;
    }

    public override bool Scatter(in Ray r, in HitRecord rec, out Color attenuation, out Ray scattered)
    {
        Vector3 reflected = Vector3.reflect(r.direction, rec.normal);
        reflected = reflected.normalized + (fuzz * Vector3.RandomUnitVector());
        scattered = new Ray(rec.p, reflected);
        attenuation = albedo;
        return (Vector3.Dot(scattered.direction, rec.normal) > 0);
    }
}