using System.Text;

public class Camera
{
    public double aspectRatio = 1.0;
    public int imageWidth = 100;
    public int samplesPerPixel = 100;
    public int maxDepth = 50;
    private int imageHeight;
    private double pixelSamplesScale;
    private Point3 cameraCenter;
    private Point3 pixel00;
    private Vector3 pixelU;
    private Vector3 pixelV;
    private StringBuilder sb = new StringBuilder();
    private static readonly Random random = new Random();

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

        cameraCenter = new Point3(0, 0, 0);

        double focalLength = 1.0;
        double viewportHeight = 2.0;
        double viewportWidth = viewportHeight * (double)imageWidth / imageHeight;

        Vector3 viewportU = new Vector3(viewportWidth, 0, 0);
        Vector3 viewportV = new Vector3(0, -viewportHeight, 0);

        pixelU = viewportU / imageWidth;
        pixelV = viewportV / imageHeight;

        Point3 viewportOrigin = cameraCenter - new Vector3(0, 0, focalLength) - viewportU / 2 - viewportV / 2;
        pixel00 = viewportOrigin + 0.5 * (pixelU + pixelV);
    }

    private Color RayColor(in Ray r, int depth, in IHittable world)
    {
        if (depth <= 0) return new Color(0, 0, 0);
        HitRecord rec;
        if (world.Hit(r, new Interval(0.001, double.PositiveInfinity), out rec))
        {
            Vector3 dir = rec.normal + Vector3.RandomUnitVector();
            return 0.5 * RayColor(new Ray(rec.p, dir), depth-1, world);
        }

        Vector3 unitDirection = r.direction.normalized;
        double a = 0.5 * (unitDirection.y + 1.0);
        return (1.0 - a) * new Color(1.0, 1.0, 1.0) + a * new Color(0.5, 0.7, 1.0);
    }

    private Ray GetRay(int i, int j)
    {
        Vector3 offset = SampleSquare();
        Point3 pixelSample = pixel00 + ((i + offset.x) * pixelU) + ((j + offset.y) * pixelV);
        Point3 rayOrigin = cameraCenter;
        Vector3 rayDirection = pixelSample - rayOrigin;
        return new Ray(rayOrigin, rayDirection);
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
        File.WriteAllText("./output.ppm", sb.ToString());
    }
}