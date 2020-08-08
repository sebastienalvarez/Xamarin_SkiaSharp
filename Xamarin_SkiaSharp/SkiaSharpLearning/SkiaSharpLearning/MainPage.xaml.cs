using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkiaSharpLearning
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Canvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;
            SKImageInfo info = e.Info;

            // 1ère partie : la base
            DrawBasics(canvas, info);
        }


        private void DrawBasics(SKCanvas canvas, SKImageInfo info)
        {
            canvas.Clear();

            SKPaint paint = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.OrangeRed,
                StrokeWidth = 5,
            };

            SKFont font = new SKFont()
            {
                Typeface = SKTypeface.Default,
                Size = 100,
                Embolden = true
            };

            SKPaint fontPaint = new SKPaint(font)
            {
                Color = SKColors.Violet
            };

            canvas.DrawCircle(info.Width / 3, info.Height / 1.5f, 200, paint);

            canvas.DrawLine(20, 30, 180, 220, paint);

            canvas.DrawOval(info.Width / 2, info.Height / 2, 50, 300, paint);

            canvas.DrawPoint(200, 100, paint);

            SKPoint[] points = new SKPoint[]
            {
                new SKPoint(200, 100),
                new SKPoint(200, 150),
                new SKPoint(350, 150),
                new SKPoint(450, 220)
            };

            canvas.DrawPoints(SKPointMode.Polygon, points, paint);

            canvas.DrawRect(400, 200, 200, 200, paint);

            canvas.DrawText("Test", 500, 300, fontPaint);

            canvas.DrawArc(new SKRect(20, 20, 55, 85), 45, 180, true, paint);

            SKPaint paintTransparency = new SKPaint()
            {
                Color = new SKColor(255, 220, 220, 100)
            };
            SKBitmap bitmap = null;
            string imageRessource = "SkiaSharpLearning.Images.SaturneSelectionnee.png";
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(imageRessource))
            {
                bitmap = SKBitmap.Decode(stream);
            }
            bitmap = bitmap.Resize(new SKSizeI(bitmap.Width * 3, bitmap.Height * 3), SKFilterQuality.High);
            canvas.DrawBitmap(bitmap, 200, 200, paintTransparency);
        }

    }
}
