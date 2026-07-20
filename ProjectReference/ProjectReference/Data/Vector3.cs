global using Point3 = Vector3;
global using Color = Vector3;

public struct Vector3
{
    public double x { get; private set; }
    public double y { get; private set; }
    public double z { get; private set; }
    private double squaredLength => x * x + y * y + z * z;
    public double magnitude => Math.Sqrt(squaredLength);
    public Vector3 normalized
    {
        get => this / this.magnitude;
    }

    public static Vector3 RandomVector() => new Vector3(Random.Shared.NextDouble(), Random.Shared.NextDouble(), Random.Shared.NextDouble());
    public static Vector3 RandomVector(double min, double max) => new Vector3(min + (max - min) * Random.Shared.NextDouble(), min + (max - min) * Random.Shared.NextDouble(), min + (max - min) * Random.Shared.NextDouble());
    public static Vector3 RandomUnitVector()
    {
        while(true)
        {
            Vector3 p = RandomVector(-1, 1);
            double lenq = p.squaredLength;
            if(1e-160 < lenq && lenq <= 1) return p / Math.Sqrt(lenq);
        }
    }

    public static Vector3 RandomOnHemisphere(in Vector3 normal)
    {
        Vector3 OnUnitSphere = Vector3.RandomUnitVector();
        if (Dot(OnUnitSphere, normal) > 0.0) return OnUnitSphere;
        else return -OnUnitSphere;
    }

    public static Vector3 reflect(in Vector3 v, in Vector3 n)
    {
        return v - 2 * Dot(v, n) * n;
    }

    public static Vector3 refract(in Vector3 uv, in Vector3 n, double eta)
    {
        double cosTheta = Math.Min(Dot(-uv, n), 1.0);
        Vector3 rBot = eta * (uv + cosTheta * n);
        Vector3 rVert = -Math.Sqrt(Math.Abs(1.0 - rBot.squaredLength)) * n;
        return rBot + rVert;
    }

    //Vector3 간의 연산을 위한 연산자 오버로딩들
    public static Vector3 operator -(Vector3 v)
    {
        return new Vector3(-v.x, -v.y, -v.z);
    }
    public static Vector3 operator +(Vector3 u, Vector3 v)
    {
        return new Vector3(u.x + v.x, u.y + v.y, u.z + v.z);
    }
    public static Vector3 operator -(Vector3 u, Vector3 v)
    {
        return new Vector3(u.x - v.x, u.y - v.y, u.z - v.z);
    }
    public static Vector3 operator *(Vector3 u, Vector3 v)
    {
        return new Vector3(u.x * v.x, u.y * v.y, u.z * v.z);
    }
    public static Vector3 operator *(double t, Vector3 v)
    {
        return new Vector3(t * v.x, t * v.y, t * v.z);
    }
    public static Vector3 operator *(Vector3 v, double t)
    {
        return t * v;
    }
    public static Vector3 operator /(Vector3 v, double t)
    {
        return (1 / t) * v;
    }

    public Vector3(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    //두 벡터 간의 내적을 구하기 위한 메서드 생성
    public static double Dot(Vector3 u, Vector3 v)
    {
        return u.x * v.x
             + u.y * v.y
             + u.z * v.z;
    }

    //두 벡터 간의 외적을 구하기 위한 메서드 생성
    public static Vector3 Cross(Vector3 u, Vector3 v)
    {
        return new Vector3(
            u.x * v.y - u.y * v.x,   
            u.y * v.z - u.z * v.y,
            u.z * v.x - u.x * v.z
        );
    }
}