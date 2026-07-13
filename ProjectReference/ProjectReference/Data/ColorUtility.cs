using System.Text;

public static class ColorUtility
{
    public static void WriteColor(Color color, StringBuilder sb)
    {
        double r = Math.Sqrt(color.x);
        double g = Math.Sqrt(color.y);
        double b = Math.Sqrt(color.z);

        Interval intensity = new Interval(0.000, 0.999);
        int rbyte = (int)(256 * intensity.Clamp(r));
        int gbyte = (int)(256 * intensity.Clamp(g));
        int bbyte = (int)(256 * intensity.Clamp(b));

        sb.AppendLine($"{rbyte} {gbyte} {bbyte}");
    }
}