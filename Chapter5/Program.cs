public class Program
{
    static void Basic()
    {
        Camera mainCam = new Camera(400, 16.0 / 9.0);
        mainCam.samplesPerPixel = 100;
        mainCam.vfov = 20;
        mainCam.lookFrom = new Point3(-2, 2, 1);
        mainCam.lookAt = new Point3(0, 0, -1);
        mainCam.vUp = new Vector3(0, 1, 0);
        mainCam.defocusAngle = 10.0f;
        mainCam.focusDist = 3.4;

        Material ground = new Lambertian(new Color(0.8, 0.8, 0.0));
        Material center = new Lambertian(new Color(0.1, 0.2, 0.5));
        Material left = new Dielectric(1.5);
        Material bubble = new Dielectric(1.0 / 1.5);
        Material right = new Metal(new Color(0.8, 0.6, 0.2), 1.0);

        HittableList world = new HittableList();

        world.add(new Sphere(new Point3(0.0, 0.0, -1.2), 0.5, center));
        world.add(new Sphere(new Point3(0.0, -100.5, -1.0), 100.0, ground));
        world.add(new Sphere(new Point3(-1.0, 0.0, -1.0), 0.5, left));
        world.add(new Sphere(new Point3(-1.0, 0.0, -1.0), 0.4, bubble));
        world.add(new Sphere(new Point3(1.0, 0.0, -1.0), 0.5, right));

        world = new HittableList(new BVHNode(world));

        mainCam.Render(world);
    }

    static void TextureRender()
    {
        Texture tex = new Image("Smile.jpg"); //images 폴더 내부에 위치
        Material surface = new Lambertian(tex);
        Sphere globe = new Sphere(new Point3(0, 0, 0), 2, surface);

        Camera cam = new Camera(400, 16.0 / 9.0);
        cam.samplesPerPixel = 100;
        cam.maxDepth = 50;
        cam.vfov = 20;
        cam.lookFrom = new Point3(12, 0, 0);
        cam.lookAt = new Point3(0, 0, 0);
        cam.defocusAngle = 0;

        cam.Render(new HittableList(globe));
    }

    static void Squares()
    {
        HittableList world = new HittableList();

        Material red = new Lambertian(new Color(1.0, 0.2, 0.2));
        Material green = new Lambertian(new Color(0.2, 1.0, 0.2));
        Material blue = new Lambertian(new Color(0.2, 0.2, 1.0));
        Material orange = new Lambertian(new Color(1.0, 0.5, 0.0));
        Material teal = new Lambertian(new Color(0.2, 0.8, 0.8));

        world.add(new Square(new Point3(-3, -2, 5), new Vector3(0, 0, -4), new Vector3(0, 4, 0), red));
        world.add(new Square(new Point3(-2, -2, 0), new Vector3(4, 0, 0), new Vector3(0, 4, 0), green));
        world.add(new Square(new Point3(3, -2, 1), new Vector3(0, 0, 4), new Vector3(0, 4, 0), blue));
        world.add(new Square(new Point3(-2, 3, 1), new Vector3(4, 0, 0), new Vector3(0, 0, 4), orange));
        world.add(new Square(new Point3(-2, -3, 5), new Vector3(4, 0, 0), new Vector3(0, 0, -4), teal));

        Camera cam = new Camera(400, 1.0);
        cam.samplesPerPixel = 100;
        cam.maxDepth = 50;
        cam.vfov = 80;
        cam.lookFrom = new Point3(0, 0, 9);
        cam.lookAt = new Point3(0, 0, 0);
        cam.vUp = new Vector3(0, 1, 0);
        cam.defocusAngle = 0;

        cam.Render(world);
    }

    static void Triangles()
    {
        HittableList world = new HittableList();

        Material red = new Lambertian(new Color(1.0, 0.2, 0.2));
        Material green = new Lambertian(new Color(0.2, 1.0, 0.2));
        Material blue = new Lambertian(new Color(0.2, 0.2, 1.0));

        world.add(new Triangle(new Point3(0.2, 0.2, 0), new Point3(4, 0.2, 0), new Point3(0.2, 4, 0), red));
        world.add(new Triangle(new Point3(0.2, 0, 0.2), new Point3(4, 0, 0.2), new Point3(0.2, 0, 4), green));
        world.add(new Triangle(new Point3(0, 0.2, 0.2), new Point3(0, 0.2, 4), new Point3(0, 4, 0.2), blue));

        Camera cam = new Camera(400, 1.0);
        cam.samplesPerPixel = 100;
        cam.maxDepth = 50;
        cam.vfov = 20;
        cam.lookFrom = new Point3(10, 15, 10);
        cam.lookAt = new Point3(0, 0, 0);
        cam.vUp = new Vector3(0, 1, 0);
        cam.defocusAngle = 0;

        cam.Render(world);
    }

    static void OBJLoad()
    {
        HittableList world = new HittableList();

        Material basic = new Lambertian(new Color(0.8, 0.8, 0.8));

        ObjLoader loader = new ObjLoader("monkey.obj");
        loader.LoadToWorld(basic, world);

        Camera cam = new Camera(400, 16.0 / 9.0);
        cam.samplesPerPixel = 100;
        cam.maxDepth = 50;
        cam.vfov = 20;
        cam.lookFrom = new Point3(7, 7, 7);
        cam.lookAt = new Point3(0, 0, 0);
        cam.vUp = new Vector3(0, 1, 0);
        cam.defocusAngle = 0;

        cam.Render(world);
    }

    static void Main()
    {
        switch(4)
        {
            case 0: Basic(); break;
            case 1: TextureRender(); break;
            case 2: Squares(); break;
            case 3: Triangles(); break;
            case 4: OBJLoad(); break;
        }
    }
}