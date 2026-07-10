public class Program
{
    static void Main()
    {
        Camera mainCam = new Camera(400, 16.0 / 9.0);
        mainCam.samplesPerPixel = 100;

        HittableList world = new HittableList();
        world.add(new Sphere(new Vector3(0, 0, -1), 0.5));
        world.add(new Sphere(new Vector3(0, -100.5, -1), 100));

        mainCam.Render(world);
    }
}