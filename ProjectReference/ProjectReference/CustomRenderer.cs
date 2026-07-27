//public class CustomRenderer
//{
//    public static void Main()
//    {
//        HittableList world = new HittableList();

//        Material ground = new Lambertian(new Color(0.5, 0.5, 0.5));
//        world.add(new Sphere(new Point3(0, -1000, 0), 1000, ground));

//        for(int a = -11; a < 11; a++)
//        {
//            for(int b = -11; b < 11; b++)
//            {
//                double mat = Random.Shared.NextDouble();
//                Point3 center = new Point3(a + 0.9 * Random.Shared.NextDouble(), 0.2, b + 0.9 * Random.Shared.NextDouble());

//                if ((center - new Point3(4, 0.2, 0)).magnitude > 0.9)
//                {
//                    Material sphereMat;

//                    if(mat < 0.8)
//                    {
//                        Color albedo = Color.RandomVector() * Color.RandomVector();
//                        sphereMat = new Lambertian(albedo);
//                        world.add(new Sphere(center, 0.2, sphereMat));
//                    }
//                    else if (mat < 0.95)
//                    {
//                        Color albedo = Color.RandomVector(0.5, 1);
//                        Double fuzz = Random.Shared.NextDouble() / 2;
//                        sphereMat = new Metal(albedo, fuzz);
//                        world.add(new Sphere(center, 0.2, sphereMat));
//                    }
//                    else
//                    {
//                        sphereMat = new Dielectric(1.5);
//                        world.add(new Sphere(center, 0.2, sphereMat));
//                    }
//                }
//            }
//        }

//        Material material1 = new Dielectric(1.5);
//        Material material2 = new Lambertian(new Color(0.4, 0.2, 0.1));
//        Material material3 = new Metal(new Color(0.7, 0.6, 0.5), 0.0);

//        world.add(new Sphere(new Point3(0, 1, 0), 1.0, material1));
//        world.add(new Sphere(new Point3(-4, 1, 0), 1.0, material2));
//        world.add(new Sphere(new Point3(4, 1, 0), 1.0, material3));

//        Camera cam = new Camera(1200, 16.0/9.0);
//        cam.samplesPerPixel = 500;
//        cam.maxDepth = 50;

//        cam.vfov = 20;
//        cam.lookFrom = new Point3(13, 2, 3);
//        cam.lookAt = new Point3(0, 0, 0);
//        cam.vUp = new Vector3(0, 1, 0);

//        cam.defocusAngle = 0.6;
//        cam.focusDist = 10.0;

//        cam.Render(world);
//    }
//}