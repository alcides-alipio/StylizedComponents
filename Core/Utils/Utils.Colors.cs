using System;
using System.Drawing;

namespace StylizedComponents.Core
{
    internal partial class Utils
    {
        public static Color ApplyColorFilter(Color baseColor, Color filterColor, float filterStrength)
        {
            float amount = filterStrength < 0f ? 0f : (filterStrength > 1f ? 1f : filterStrength);

            int r = (int)(baseColor.R + (filterColor.R - baseColor.R) * amount);
            int g = (int)(baseColor.G + (filterColor.G - baseColor.G) * amount);
            int b = (int)(baseColor.B + (filterColor.B - baseColor.B) * amount);
            int a = (int)(baseColor.A + (filterColor.A - baseColor.A) * amount);

            return Color.FromArgb(a, r, g, b);
        }

        public static int CalculateFullRoundBorderRadius(int width, int height)
        {
            return (int)(Math.Min(width, height) / 2.1f);
        }
    }
}
