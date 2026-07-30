public abstract class Texture
{
    public abstract Color Value(double u, double v, Point3 p);
}

public class Solid : Texture
{
    private Color albedo;

    public Solid(Color albedo)
    {
        this.albedo = albedo;
    }

    public Solid(double red, double green, double blue)
    {
        this.albedo = new Color(red, green, blue);
    }

    public override Color Value(double u, double v, Color p)
    {
        return albedo;
    }
}

public class Image : Texture
{
    private ImageLoader image;
    public Image(string fileName)
    {
        this.image = new ImageLoader(fileName);
    }

    public override Color Value(double u, double v, Color p)
    {
        if (image.Height() <= 0) return new Color(0, 1, 1);
        u = Math.Clamp(u, 0, 1);
        v = 1.0 - Math.Clamp(v, 0, 1);

        int i = (int)(u * image.Width());
        int j = (int)(v * image.Height());
        ReadOnlySpan<Byte> pixel = image.PixelData(i, j);

        double colorScale = 1.0 / 255.0;
        return new Color(colorScale * pixel[0], colorScale * pixel[1], colorScale * pixel[2]);
    }
}