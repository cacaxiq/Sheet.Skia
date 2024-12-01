using Sheet.Skia.Controls;
using SkiaSharp;
using System.Reflection;

namespace Sheet.Skia.Extensions
{
    public static class SKCanvasExtensions
    {
        public static void DrawRowLine(this SKCanvas canvas, int stroke, SKColor color, int width, int height = 0)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = stroke,
                IsAntialias = true
            };
            canvas.DrawLine(0, 0 + height, width, 0 + height, paint);
        }

        public static void DrawColunmLine(this SKCanvas canvas, int stroke, SKColor color, int height, int width = 0)
        {
            using var paint = new SKPaint
            {
                Color = color,
                StrokeWidth = stroke,
                IsAntialias = true
            };
            canvas.DrawLine(0 + width, 0, 0 + width, height, paint);
        }

        public static void DrawBorderLine(this SKCanvas canvas, int stroke, SKColor color, int width, int height)
        {
            var rect = new SKRect(0, 0, width, height);
            using var paint = new SKPaint
            {
                Color = color,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = stroke,
                IsAntialias = true
            };
            canvas.DrawRect(rect, paint);
        }

       
        public static (int height, int width ) GetTextBounds(this string text, SKPaint paint)
        {
            var bounds = new SKRect();
            paint.MeasureText(text, ref bounds);
            return ((int)bounds.Height, (int)bounds.Width);
        }
    }
}
