using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TouchTracking;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace SkiaSharpLearning
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private SKCanvas canvas;
        private SKImageInfo info;
        SKBitmap bitmapOrigin;

        // Pour le test d'animation
        private bool isAnimated = false;
        private double scale = 1;
        private double increment = 0.01;

        // Pour le test de gestion des gestes sur l'écran tactile
        bool isEffectRequired = false;
        Dictionary<long, SKPath> inProgressPaths = new Dictionary<long, SKPath>();
        List<SKPath> completedPaths = new List<SKPath>();
        long? touchId = null;
        SKMatrix currentMatrix = SKMatrix.CreateIdentity();
        bool isScaling;
        SKPoint pivotPoint;
        SKPoint pressedLocation;
        SKMatrix pressedMatrix;


        public MainPage()
        {
            InitializeComponent();
            string imageRessource = "SkiaSharpLearning.Images.SaturneSelectionnee.png";
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(imageRessource))
            {
                bitmapOrigin = SKBitmap.Decode(stream);
            }
        }

        private void Canvas_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            canvas = e.Surface.Canvas;
            info = e.Info;

            // 1ère partie : la base
            //DrawBasics();

            // 2ème partie : les lignes et les paths associés
            //isEffectRequired = true;
            //DrawLinesAndPaths();

            // 3ème partie : les Transforms 
            DrawTransforms();
        }

        private bool ComputeBitmapScale()
        {
            if(scale > 5)
            {
                increment = -increment;
            }
            if(scale < 1)
            {
                increment = -increment;
            }
            scale += increment;
            Canvas.InvalidateSurface();
            return isAnimated;
        }

        // 1ère partie : la base
        private void DrawBasics()
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
            SKBitmap bitmap = bitmapOrigin.Resize(new SKSizeI((int)(bitmapOrigin.Width * scale), (int)(bitmapOrigin.Height * scale)), SKFilterQuality.High);
            canvas.DrawBitmap(bitmap, 200, 200, paintTransparency);
        }

        // 2ème partie : les paths et la gestion des gestes sur l'écran tactile
        private void DrawLinesAndPaths()
        {
            canvas.Clear();
            SKPath path = new SKPath();
            path.MoveTo(info.Width / 2, info.Height / 2);
            path.LineTo(info.Width / 2, info.Height - 150);
            path.LineTo(200, info.Height - 150);
            path.Close();

            SKPaint strokePaint = new SKPaint
            {
                Color = SKColors.OrangeRed,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 15,
                StrokeCap = SKStrokeCap.Butt,
                StrokeJoin = SKStrokeJoin.Round,
                PathEffect = SKPathEffect.CreateDash(new float[] { 45, 45 }, 0)

            };

            SKPaint fillPaint = new SKPaint
            {
                Color = SKColors.ForestGreen,
                Style = SKPaintStyle.Fill
            };

            canvas.DrawPath(path, strokePaint);
            canvas.DrawPath(path, fillPaint);

            // Test des gestes sur l'écran tactile
            SKPaint fingerPaint = new SKPaint
            {
                Color = SKColors.Red,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 5
            };

            foreach(var item in inProgressPaths)
            {
                canvas.DrawPath(item.Value, fingerPaint);
            }


            foreach(var item in completedPaths)
            {
                canvas.DrawPath(item, fingerPaint);
            }

        }

        // 3ème partie : les Transforms 
        private void DrawTransforms()
        {
            SKPaint paint = new SKPaint
            {
                Color = SKColors.Gray,
                Style = SKPaintStyle.Fill,
                TextSize = 100,
                TextAlign = SKTextAlign.Center
            };

            //canvas.RotateDegrees(-25, info.Width / 2, info.Height / 2);
            //canvas.Scale(2);
            //canvas.Translate(10, 10);
            //canvas.DrawText("OMBRE", info.Width/4, info.Height/4, paint);
            //canvas.Translate(-10, -10);
            //paint.Color = SKColors.Bisque;
            //canvas.DrawText("OMBRE", info.Width/4, info.Height/4, paint);



            //canvas.Translate(info.Width / 2, info.Height / 2);
            //ComputeSkewInDegrees(canvas, -35, 0);
            //canvas.Scale(1, 1.5f);
            //canvas.Translate(-info.Width / 2, -info.Height / 2);
            //canvas.DrawText("OMBRE", info.Width / 2, info.Height / 2, paint);

            //canvas.Translate(info.Width / 2, info.Height / 2);
            //canvas.Scale(1, 2f/3f);
            //ComputeSkewInDegrees(canvas, 35, 0);
            //canvas.Translate(-info.Width / 2, -info.Height / 2);
            //paint.Color = SKColors.Bisque;
            //canvas.DrawText("OMBRE", info.Width / 2, info.Height / 2, paint);



            SKMatrix matrix = canvas.TotalMatrix;
            matrix.Persp1 = -0.01f;
            matrix = matrix.PostConcat(SKMatrix.CreateTranslation(info.Width / 2, info.Height / 2));
            //matrix = matrix.PostConcat(SKMatrix.CreateRotationDegrees(10, info.Width / 2, info.Height / 2));
            //matrix = matrix.PostConcat(SKMatrix.CreateScale(1, 2));
            canvas.SetMatrix(matrix);
            canvas.DrawText("Texte", 0, 0, paint);

            //canvas.Clear();
            //canvas.SetMatrix(currentMatrix);
            //canvas.DrawBitmap(bitmapOrigin, 0, 0);
        }

        private void ComputeSkewInDegrees(SKCanvas canvas, double xDegrees, double yDegrees)
        {
            canvas.Skew((float)Math.Tan(Math.PI*xDegrees/180), (float)Math.Tan(Math.PI * yDegrees / 180));
        }


        private void StopAnimation_Clicked(object sender, EventArgs e)
        {
            isAnimated = false;
        }

        private void StartAnimation_Clicked(object sender, EventArgs e)
        {
            isAnimated = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(16), ComputeBitmapScale);
        }

        private void TouchEffect_TouchAction2(object sender, TouchActionEventArgs args)
        {
            // Convert Xamarin.Forms point to pixels
            Point pt = args.Location;
            SKPoint point = ConvertToPixel(pt);
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    // Track only one finger
                    if (touchId.HasValue)
                        return;

                    // Check if the finger is within the boundaries of the bitmap
                    SKRect rect = new SKRect(0, 0, bitmapOrigin.Width, bitmapOrigin.Height);
                    rect = currentMatrix.MapRect(rect);
                    if (!rect.Contains(point))
                        return;

                    // First assume there will be no scaling
                    isScaling = false;

                    // If touch is outside interior ellipse, make this a scaling operation
                    if (Math.Pow((point.X - rect.MidX) / (rect.Width / 2), 2) +
                        Math.Pow((point.Y - rect.MidY) / (rect.Height / 2), 2) > 1)
                    {
                        isScaling = true;
                        float xPivot = point.X < rect.MidX ? rect.Right : rect.Left;
                        float yPivot = point.Y < rect.MidY ? rect.Bottom : rect.Top;
                        pivotPoint = new SKPoint(xPivot, yPivot);
                    }

                    // Common for either pan or scale
                    touchId = args.Id;
                    pressedLocation = point;
                    pressedMatrix = currentMatrix;
                    break;

                case TouchActionType.Moved:
                    if (!touchId.HasValue || args.Id != touchId.Value)
                        return;

                    SKMatrix matrix = SKMatrix.CreateIdentity();

                    // Translating
                    if (!isScaling)
                    {
                        SKPoint delta = point - pressedLocation;
                        matrix = SKMatrix.CreateTranslation(delta.X, delta.Y);
                    }
                    // Scaling
                    else
                    {
                        float scaleX = (point.X - pivotPoint.X) / (pressedLocation.X - pivotPoint.X);
                        float scaleY = (point.Y - pivotPoint.Y) / (pressedLocation.Y - pivotPoint.Y);
                        matrix = SKMatrix.CreateScale(scaleX, scaleY, pivotPoint.X, pivotPoint.Y);
                    }

                    // Concatenate the matrices
                    matrix = matrix.PreConcat(pressedMatrix);
                    currentMatrix = matrix;
                    Canvas.InvalidateSurface();
                    break;

                case TouchActionType.Released:
                case TouchActionType.Cancelled:
                    touchId = null;
                    break;
            }
        }

        private void TouchEffect_TouchAction(object sender, TouchActionEventArgs args)
        {
            if (isEffectRequired)
            {
                switch (args.Type)
                {
                    case TouchActionType.Pressed:
                        if (!inProgressPaths.ContainsKey(args.Id))
                        {
                            SKPath path = new SKPath();
                            path.MoveTo(ConvertToPixel(args.Location));
                            inProgressPaths.Add(args.Id, path);
                            Canvas.InvalidateSurface();
                        }
                        break;

                    case TouchActionType.Moved:
                        if (inProgressPaths.ContainsKey(args.Id))
                        {
                            SKPath path = inProgressPaths[args.Id];
                            path.LineTo(ConvertToPixel(args.Location));
                            Canvas.InvalidateSurface();
                        }
                        break;

                    case TouchActionType.Released:
                        if (inProgressPaths.ContainsKey(args.Id))
                        {
                            completedPaths.Add(inProgressPaths[args.Id]);
                            inProgressPaths.Remove(args.Id);
                            Canvas.InvalidateSurface();
                        }
                        break;

                    case TouchActionType.Cancelled:
                        if (inProgressPaths.ContainsKey(args.Id))
                        {
                            inProgressPaths.Remove(args.Id);
                            Canvas.InvalidateSurface();
                        }
                        break;
                }
            }
        }

        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float)(Canvas.CanvasSize.Width * pt.X / Canvas.Width),
                               (float)(Canvas.CanvasSize.Height * (pt.Y - TitleLabel.Height) / Canvas.Height));
        }

    }
}
