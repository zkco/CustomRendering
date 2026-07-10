using System.Text;

public static class ColorUtility
{
    public static void WriteColor(Color color, StringBuilder sb)
    {
        Interval intensity = new Interval(0.000, 0.999);
        int rbyte = (int)(256 * intensity.Clamp(color.x));
        int gbyte = (int)(256 * intensity.Clamp(color.y));
        int bbyte = (int)(256 * intensity.Clamp(color.z));

        sb.AppendLine($"{rbyte} {gbyte} {bbyte}");
    }
}