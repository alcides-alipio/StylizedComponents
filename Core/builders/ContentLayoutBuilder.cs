using StylizedComponents.Core.models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Core.builders
{
    internal static class ContentLayoutBuilder
    {
        public static Size MeasureText(string text, Font font)
        {
            if (string.IsNullOrEmpty(text))
                return Size.Empty;

            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                SizeF size = g.MeasureString(
                    text,
                    font,
                    PointF.Empty,
                    StringFormat.GenericTypographic);

                return Size.Ceiling(size);
            }
        }

        public static Rectangle CreateRoundedContent(RoundedPathOptions options)
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
            int w = Math.Max(0, options.Width - inset * 2) - 6;
            int h = Math.Max(0, options.Height - inset * 2) - 1;

            return new Rectangle(x, y, w, h);
        }

        public static Rectangle CreateTextAndImageContent(
            string text, Font textFont,
            Image icon, Size iconSize, IconAlignment iconAlign,
            Rectangle bounds, ContentAlignment align, int spacing)
        {
            bool hasText = !string.IsNullOrWhiteSpace(text);
            bool hasIcon = icon != null;

            if (!hasIcon && !hasText)
                return Rectangle.Empty;

            Size textSize = hasText
                ? MeasureText(
                    text,
                    textFont)
                : Size.Empty;

            int contentSpacing =
                hasIcon && hasText
                    ? spacing
                    : 0;

            int width;
            int height;

            switch (iconAlign)
            {
                case IconAlignment.Top:
                case IconAlignment.Bottom:

                    width = Math.Max(
                        iconSize.Width,
                        textSize.Width);

                    height =
                        iconSize.Height +
                        contentSpacing +
                        textSize.Height;

                    break;

                default:

                    width =
                        iconSize.Width +
                        contentSpacing +
                        textSize.Width;

                    height = Math.Max(
                        iconSize.Height,
                        textSize.Height);

                    break;
            }

            int x;
            int y;

            switch (align)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    x = bounds.Left;
                    break;

                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    x = bounds.Left + (bounds.Width - width) / 2;
                    break;

                default:
                    x = bounds.Right - width;
                    break;
            }

            switch (align)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    y = bounds.Top;
                    break;

                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    y = bounds.Top + (bounds.Height - height) / 2;
                    break;

                default:
                    y = bounds.Bottom - height;
                    break;
            }

            return new Rectangle(
                x, y,
                width,
                height);
        }

        public static Rectangle CreateTextRectangle(
            Size textSize,
            Size iconSize, IconAlignment iconAlign,
            Rectangle contentBounds, int spacing)
        {
            int contentSpacing =
                textSize != Size.Empty && iconSize != Size.Empty
                    ? spacing
                    : 0;

            switch (iconAlign)
            {
                case IconAlignment.Left:

                    return new Rectangle(
                        contentBounds.Left + iconSize.Width + contentSpacing,
                        contentBounds.Top + (contentBounds.Height - textSize.Height) / 2,
                        textSize.Width,
                        textSize.Height);

                case IconAlignment.Right:

                    return new Rectangle(
                        contentBounds.Left,
                        contentBounds.Top + (contentBounds.Height - textSize.Height) / 2,
                        textSize.Width,
                        textSize.Height);

                case IconAlignment.Top:

                    return new Rectangle(
                        contentBounds.Left + (contentBounds.Width - textSize.Width) / 2,
                        contentBounds.Top + iconSize.Height + contentSpacing,
                        textSize.Width,
                        textSize.Height);

                case IconAlignment.Bottom:

                    return new Rectangle(
                        contentBounds.Left + (contentBounds.Width - textSize.Width) / 2,
                        contentBounds.Top,
                        textSize.Width,
                        textSize.Height);

                default:

                    return Rectangle.Empty;
            }
        }

        public static Rectangle CreateIconRectangle(
            Size textSize,
            Size iconSize, IconAlignment iconAlign,
            Rectangle contentBounds, int spacing)
        {
            int contentSpacing =
                textSize != Size.Empty && iconSize != Size.Empty
                    ? spacing
                    : 0;

            switch (iconAlign)
            {
                case IconAlignment.Left:

                    return new Rectangle(
                        contentBounds.Left,
                        contentBounds.Top + (contentBounds.Height - iconSize.Height) / 2,
                        iconSize.Width,
                        iconSize.Height);

                case IconAlignment.Right:

                    return new Rectangle(
                        contentBounds.Left + textSize.Width + contentSpacing,
                        contentBounds.Top + (contentBounds.Height - iconSize.Height) / 2,
                        iconSize.Width,
                        iconSize.Height);

                case IconAlignment.Top:

                    return new Rectangle(
                        contentBounds.Left + (contentBounds.Width - iconSize.Width) / 2,
                        contentBounds.Top,
                        iconSize.Width,
                        iconSize.Height);

                case IconAlignment.Bottom:

                    return new Rectangle(
                        contentBounds.Left + (contentBounds.Width - iconSize.Width) / 2,
                        contentBounds.Top + textSize.Height + contentSpacing,
                        iconSize.Width,
                        iconSize.Height);

                default:

                    return Rectangle.Empty;
            }
        }
    }
}
