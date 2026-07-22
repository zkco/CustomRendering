using System.Reflection;

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

public class Dielectric : Material
{
    private double refractionIndex;
    public Dielectric(double refractionIndex)
    {
        this.refractionIndex = refractionIndex;
    }

    public override bool Scatter(in Ray r, in HitRecord rec, out Color attenuation, out Ray scattered)
    {
        attenuation = new Color(1.0, 1.0, 1.0);
        double ri = rec.frontFace ? (1.0 / refractionIndex) : refractionIndex;

        Vector3 hatR = r.direction.normalized;
        double cosTheta = Math.Min(Vector3.Dot(-hatR, rec.normal), 1.0);
        double sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);

        bool isRefractable = ri * sinTheta > 1.0;
        Vector3 direction;

        if (isRefractable || reflectance(cosTheta, ri) > Random.Shared.NextDouble()) 
            direction = Vector3.reflect(hatR, rec.normal);
        else 
            direction = Vector3.refract(hatR, rec.normal, ri);

        scattered = new Ray(rec.p, direction);
        return true;
    }

    private double reflectance(double cosTheta, double refraction)
    {
        double f0 = (1 - refraction) / (1 + refraction);
        f0 = f0 * f0;
        return f0 + (1 - f0) * Math.Pow((1 - cosTheta), 5);
    }
}