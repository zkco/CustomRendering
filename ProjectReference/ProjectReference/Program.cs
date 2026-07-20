public class Program
{
    static void Main()
    {
        Camera mainCam = new Camera(400, 16.0 / 9.0);
        mainCam.samplesPerPixel = 100;

        Material ground = new Lambertian(new Color(0.8, 0.8, 0.0));
        Material center = new Lambertian(new Color(0.1, 0.2, 0.5));
        Material left = new Metal(new Color(0.8, 0.8, 0.8), 0.3);
        Material right = new Metal(new Color(0.8, 0.6, 0.2), 1.0);

        HittableList world = new HittableList();

        world.add(new Sphere(new Point3(0.0, 0.0, -1.2), 0.5, center));
        world.add(new Sphere(new Point3(0.0, -100.5, -1.0), 100.0, ground));
        world.add(new Sphere(new Point3(-1.0, 0.0, -1.0), 0.5, left));
        world.add(new Sphere(new Point3(1.0, 0.0, -1.0), 0.5, right));

        mainCam.Render(world);
    }
}