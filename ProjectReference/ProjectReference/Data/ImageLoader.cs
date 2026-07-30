using StbImageSharp;

public class ImageLoader
{
    private const int BytePerPixel = 3;
    private float[]? fdata = null;
    private byte[]? bdata = null;
    private int imageWidth = 0;
    private int imageHeight = 0;
    private int bytesPerScanLine = 0;
    private static readonly byte[] magenta = new byte[] { 255, 0, 255 };

    public ImageLoader() { }
    public ImageLoader(string imageFile)
    {
        string filename = imageFile;

        if (Load(filename)) return;
        if (Load(Path.Combine("images", filename))) return;
        if (Load(Path.Combine("../images", filename))) return;
        if (Load(Path.Combine("../../images", filename))) return;
        if (Load(Path.Combine("../../../images", filename))) return;

        Console.Error.WriteLine($"이미지 경로 또는 이름 오류");
    }

    public bool Load(string filename)
    {
        if (!File.Exists(filename)) return false;

        try
        {
            using (var stream = File.OpenRead(filename))
            {
                ImageResultFloat image = ImageResultFloat.FromStream(stream, ColorComponents.RedGreenBlue);
                if (image == null) return false;

                imageWidth = image.Width;
                imageHeight = image.Height;
                fdata = image.Data;
            }

            bytesPerScanLine = imageWidth * BytePerPixel;
            ConvertToByte();
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void ConvertToByte()
    {
        int totalBytes = imageWidth * imageHeight * BytePerPixel;
        bdata = new byte[totalBytes];

        for(int i = 0; i < totalBytes; i++)
        {
            bdata[i] = FloatToByte(fdata[i]);
        }
    }

    private byte FloatToByte(float value)
    {
        if(value <= 0.0f)
        {
            return 0;
        }
        if(value >= 1.0f)
        {
            return 255;
        }
        return (byte)(256.0f * value);
    }

    public int Width() => imageWidth;
    public int Height() => imageHeight;
    public ReadOnlySpan<byte> PixelData(int x, int y)
    {
        if (bdata == null) return magenta;

        x = Math.Clamp(x, 0, imageWidth - 1);
        y = Math.Clamp(y, 0, imageHeight - 1);

        int index = y * bytesPerScanLine + x * BytePerPixel;
        return new ReadOnlySpan<byte>(bdata, index, BytePerPixel);
    }
}