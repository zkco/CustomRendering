using System.Text;

public static class ColorUtility
{
    public static void WriteColor(Color color, StringBuilder sb)
    {
        // 색상 값이 0~1 범위에 있다고 가정하고 0~255 범위로 변환
        int r = (int)(255.999 * color.x);
        int g = (int)(255.999 * color.y);
        int b = (int)(255.999 * color.z);
        // 색상 값을 StringBuilder에 추가
        sb.AppendLine($"{r} {g} {b}");
    }
}