using StylizedComponents.Core.models;
using System;
using System.Drawing;

namespace StylizedComponents.Core
{
    internal class RoundedContentBounds
    {
        public static Rectangle Create(RoundedPathOptions options)
        {
            float radius =
                options.AutoRoundedCorners
                    ? (int)(Math.Min(options.Width, options.Height) / 2f)
                    : options.BorderRadius;

            if (radius > (int)(Math.Min(options.Width, options.Height) / 2f))
                radius = (int)(Math.Min(options.Width, options.Height) / 2f);

            double angleRad = 45.0 * (Math.PI / 180.0);
            int cornerInset = (int)(radius * (1.0 - Math.Cos(angleRad)));
            int inset = options.BorderThickness + cornerInset;

            int x = inset + 3;
            int y = inset;
            int w = Math.Max(0, options.Width - (inset * 2)) - 6;
            int h = Math.Max(0, options.Height - (inset * 2)) - 1;

            return new Rectangle(x, y, w, h);
        }
    }
}