using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StylizedComponents.Core
{
    internal static class RoundedPathBuilder
    {
        public struct RoundedPathOptions
        {
            public int Width;
            public int Height;
            public int BorderThickness;
            public int BorderRadius;
            public bool AutoRoundedCorners;
        }

        public static GraphicsPath Create(RoundedPathOptions options)
        {
            RectangleF rect = new RectangleF(
                options.BorderThickness / 2f,
                options.BorderThickness / 2f,
                options.Width - options.BorderThickness,
                options.Height - options.BorderThickness
            );

            float radius =
                options.AutoRoundedCorners
                    ? (int)(Math.Min(options.Width, options.Height) / 2f)
                    : options.BorderRadius;

            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                path.CloseFigure();
                return path;
            }

            float diameter = radius * 2f;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();

            return path;
        }
    }
}
