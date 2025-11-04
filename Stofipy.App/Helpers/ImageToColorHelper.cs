namespace Stofipy.App.Helpers;
using SkiaSharp;

public class ImageToColorHelper
{
    public async Task<Color> GetMostDominantColor(ImageSource imageSource,  int k=8, double colorfulnessThreshold = 10)
    {
        var fallbackColor = Colors.LightGray;
        
        var bytes = await ImageSourceToByteArrayAsync(imageSource);
        if (bytes.Length == 0)
            return fallbackColor;
        var bitmap = SKBitmap.Decode(bytes);
        
        var resized = bitmap.Resize(new SKImageInfo(100, 100), SKSamplingOptions.Default);

        var pixels = new List<SKColor>();
        for (var y = 0; y < resized.Height; y++)
            for (var x = 0; x < resized.Width; x++)
                pixels.Add(resized.GetPixel(x, y));
        
        var clusters = KMeansCluster(pixels, k);

        var colorfulClusters = clusters.Select(c => new
        {
            Color = c,
            Score = Colorfulness(c)
        }).ToList();
        
        var best = colorfulClusters.OrderByDescending(c => c.Score).FirstOrDefault();
        
        if (best == null || best.Score < colorfulnessThreshold)
            return fallbackColor;

        return Color.FromRgb(best.Color.Red, best.Color.Green, best.Color.Blue);
    }

    private static async Task<byte[]> ImageSourceToByteArrayAsync(ImageSource imageSource)
    {
        if (imageSource is FileImageSource fileImageSource)
        {
            return await File.ReadAllBytesAsync(fileImageSource.File);
        }
        else if (imageSource is StreamImageSource streamImageSource)
        {
            await using var stream = await streamImageSource.Stream(CancellationToken.None);
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
        else if (imageSource is UriImageSource uriImageSource)
        {
            using var httpClient = new HttpClient();
            return await httpClient.GetByteArrayAsync(uriImageSource.Uri);
        }

        return null!;
    }
    
    private static double Colorfulness(SKColor c)
    {
        double r = c.Red, g = c.Green, b = c.Blue;
        double rg = Math.Abs(r - g);
        double yb = Math.Abs(0.5 * (r + g) - b);
        double rgMean = rg, ybMean = yb;
        return Math.Sqrt(rgMean * rgMean + ybMean * ybMean) + 0.3 * Math.Sqrt(rgMean + ybMean);
    }

    private static List<SKColor> KMeansCluster(List<SKColor> colors, int k)
    {
        // simplified, replace with a proper K-Means for better clustering
        return colors
            .GroupBy(c => ((int)c.Red / 64, (int)c.Green / 64, (int)c.Blue / 64))
            .OrderByDescending(g => g.Count())
            .Take(k)
            .Select(g => AverageColor(g.ToList()))
            .ToList();
    }

    private static SKColor AverageColor(List<SKColor> colors)
    {
        var r = colors.Average(c => c.Red);
        var g = colors.Average(c => c.Green);
        var b = colors.Average(c => c.Blue);
        return new SKColor((byte)r, (byte)g, (byte)b);
    }
}