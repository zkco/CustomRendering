public class Camera
{
    private double aspectRatio;
    private double viewportHeight;
    private double viewportWidth;
    public Point3 cameraCenter { get; }
    public Vector3 viewportOrigin { get; }
    public Vector3 viewportU { get; }
    public Vector3 viewportV { get; }
    public Point3 pixel00 { get; }
    public Vector3 pixelU { get; }
    public Vector3 pixelV { get; }
    

    public Camera(int imageWidth, int imageHeight, Vector3 cameraPos, double focalLength = 1.0f)
    {
        this.aspectRatio = (double)imageWidth / imageHeight;
        this.viewportHeight = 2.0;
        this.viewportWidth = viewportHeight * aspectRatio;
        this.viewportU = new Vector3(viewportWidth, 0, 0);
        this.viewportV = new Vector3(0, -viewportHeight, 0);
        this.viewportOrigin = cameraCenter - new Vector3(0, 0, focalLength) - viewportU / 2 - viewportV / 2;
        this.cameraCenter = cameraPos;
        this.pixelU = viewportU / imageWidth;
        this.pixelV = viewportV / imageHeight;
        this.pixel00 = viewportOrigin + 0.5 *(pixelU + pixelV);
    }
}