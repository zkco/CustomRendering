using System.Text;

public class Camera
{
    public double aspectRatio = 1.0;
    public int imageWidth = 100;
    public int samplesPerPixel = 100;
    public int maxDepth = 50;
    public double vfov = 90;

    public Point3 lookFrom = new Point3(0, 0, 0);
    public Point3 lookAt = new Point3(0, 0, -1);
    public Vector3 vUp = new Vector3(0, 1, 0);

    public double defocusAngle = 0;
    public double focusDist = 10;

    private int imageHeight;
    private double pixelSamplesScale;
    private Point3 cameraCenter;
    private Point3 pixel00;
    private Vector3 pixelU;
    private Vector3 pixelV;
    private StringBuilder sb = new StringBuilder();
    private static readonly Random random = new Random();
    private Vector3 u, v, w;
    private Vector3 defocusU;
    private Vector3 defocusV;


    public Camera(int imageWidth, double aspectRatio)
    {
        this.imageWidth = imageWidth;
        this.aspectRatio = aspectRatio;
    }

    private void Initialize()
    {
        imageHeight = (int)(imageWidth / aspectRatio);
        imageHeight = (imageHeight < 1) ? 1 : imageHeight;
        pixelSamplesScale = 1.0 / samplesPerPixel;

        cameraCenter = lookFrom;

        double focalLength = (lookFrom - lookAt).magnitude;
        double theta = vfov * Math.PI / 180.0;
        double h = Math.Tan(theta / 2);
        double viewportHeight = 2.0 * h * focusDist;
        double viewportWidth = viewportHeight * (double)imageWidth / imageHeight;

        w = (lookFrom - lookAt).normalized;
        u = (Vector3.Cross(vUp, w)).normalized;
        v = Vector3.Cross(w, u);

        Vector3 viewportU = viewportWidth * u;
        Vector3 viewportV = viewportHeight * -v;

        pixelU = viewportU / imageWidth;
        pixelV = viewportV / imageHeight;

        Point3 viewportOrigin = cameraCenter - (focusDist * w) - viewportU / 2 - viewportV / 2;
        pixel00 = viewportOrigin + 0.5 * (pixelU + pixelV);

        double defocusRadius = focusDist * Math.Tan(defocusAngle / 360 * Math.PI);
        defocusU = u * defocusRadius;
        defocusV = v * defocusRadius;
    }

    private Color RayColor(in Ray r, int depth, in IHittable world)
    {
        if (depth <= 0) return new Color(0, 0, 0);
        HitRecord rec;

        if (world.Hit(r, new Interval(0.001, double.PositiveInfinity), out rec))
        {
            Ray scattered;
            Color attenuation;
            if (rec.mat.Scatter(r, rec, out attenuation, out scattered))
                return attenuation * RayColor(scattered, depth - 1, world);
            return new Color(0, 0, 0);
        }

        Vector3 unitDirection = r.direction.normalized;
        double a = 0.5 * (unitDirection.y + 1.0);
        return (1.0 - a) * new Color(1.0, 1.0, 1.0) + a * new Color(0.5, 0.7, 1.0);
    }

    private Ray GetRay(int i, int j)
    {
        Vector3 offset = SampleSquare();
        Point3 pixelSample = pixel00 + ((i + offset.x) * pixelU) + ((j + offset.y) * pixelV);
        Point3 rayOrigin = defocusAngle <= 0 ? cameraCenter : defocusDiskSample();
        Vector3 rayDirection = pixelSample - rayOrigin;
        return new Ray(rayOrigin, rayDirection);
    }

    private Color defocusDiskSample()
    {
        Vector3 p = Vector3.RandomUnitDisk();
        return cameraCenter + (p.x * defocusU) + (p.y * defocusV);
    }

    private Vector3 SampleSquare()
    {
        return new Vector3(random.NextDouble() - 0.5, random.NextDouble() - 0.5, 0);
    }

    public void Render(IHittable world)
    {
        Initialize();

        sb.Append($"P3\n{imageWidth} {imageHeight}\n255\n");

        for (int y = 0; y < imageHeight; y++)
        {
            Console.Error.WriteLine($"{y + 1}/{imageHeight} Rendering...");
            for (int x = 0; x < imageWidth; x++)
            {
                Color pixelColor = new Color(0, 0, 0);
                for(int sample = 0; sample < samplesPerPixel; sample++)
                {
                    Ray r = GetRay(x, y);
                    pixelColor += RayColor(r, maxDepth, world);
                }
                ColorUtility.WriteColor(pixelSamplesScale * pixelColor, sb);
            }
        }
        Console.Error.WriteLine("Rendering Done");
        File.WriteAllText("./output.ppm", sb.ToString());
    }
}