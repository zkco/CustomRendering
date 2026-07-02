using System.Text;

public class Program
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder();

        //이미지
        int imageWidth = 1280;
        int imageHeight = 720;

        Camera mainCam = new Camera(imageWidth, imageHeight, new Vector3(0, 0, 0));

        sb.Append($"P3\n{imageWidth} {imageHeight}\n255\n");

        for (int y = 0; y < imageHeight; y++)
        {
            for (int x = 0; x < imageWidth; x++)
            {
                Point3 pixelCenter = mainCam.pixel00 + mainCam.pixelU * x + mainCam.pixelV * y;
                Vector3 rayDirection = pixelCenter - mainCam.cameraCenter;
                Ray r = new Ray(mainCam.cameraCenter, rayDirection);

                Color pixelColor = r.RayColor();
                ColorUtility.WriteColor(pixelColor, sb);
            }
        }
        File.WriteAllText("./output.ppm", sb.ToString());
    }
}