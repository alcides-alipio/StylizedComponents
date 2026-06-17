using StylizedComponents.Core.models;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StylizedComponents.Core.builders
{
    internal static class RoundedPathBuilder
    {
        public static GraphicsPath Create(int width, int height, int borderThickness, int borderRadius, bool autoRoundedCorners, float padding = 0)
        {
            RectangleF rect = new RectangleF(
                (borderThickness / 2f) - padding,
                (borderThickness / 2f) - padding,
                (width - borderThickness) + (padding * 2),
                (height - borderThickness) + (padding * 2)
            );

            float radius =
                autoRoundedCorners
                    ? (int)(Math.Min(rect.Width, rect.Height) / 2f)
                    : borderRadius;

            if (radius > (int)(Math.Min(rect.Width, rect.Height) / 2f))
                radius = (int)(Math.Min(rect.Width, rect.Height) / 2f);

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
