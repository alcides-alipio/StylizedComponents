using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedButton
    {
        private Color ApplyColorFilter(Color baseColor, Color filterColor)
        {
            float amount = _hoverFilterStrength < 0f ? 0f : (_hoverFilterStrength > 1f ? 1f : _hoverFilterStrength);

            int r = (int)(baseColor.R + (filterColor.R - baseColor.R) * amount);
            int g = (int)(baseColor.G + (filterColor.G - baseColor.G) * amount);
            int b = (int)(baseColor.B + (filterColor.B - baseColor.B) * amount);
            int a = (int)(baseColor.A + (filterColor.A - baseColor.A) * amount);

            return Color.FromArgb(a, r, g, b);
        }

        private int CalculateFullRoundBorderRadius()
        {
            return (int)(Math.Min(Width, Height) / 2f);
        }

        private void SetButtonShape(GraphicsPath path)
        {
            float half = BorderThickness / 2f;

            RectangleF rect = new RectangleF(
                half,
                half,
                Width - BorderThickness,
                Height - BorderThickness
            );

            float radius = _autoRoundedCorners ? CalculateFullRoundBorderRadius() : BorderRadius;
            float diameter = radius * 2f;

            if (radius <= 0)
            {
                path.AddRectangle(rect);
            }
            else
            {
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            }

            path.CloseFigure();
        }

        protected override void OnPaintContent(PaintEventArgs e)
        {
            base.OnPaintContent(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            Color backColor = BackColor;

            if (_hoverState)
                backColor = ApplyColorFilter(backColor, _hoverColorFilter);

            using (GraphicsPath path = new GraphicsPath())
            using (Brush brush = new SolidBrush(backColor))
            {
                SetButtonShape(path);
                g.FillPath(brush, path);
            }

            var flags = TextFormatFlags.HorizontalCenter |
               TextFormatFlags.VerticalCenter |
               TextFormatFlags.SingleLine |
               TextFormatFlags.NoPadding;

            TextRenderer.DrawText(
                g,
                Text,
                Font,
                ClientRectangle,
                ForeColor,
                flags
            );
        }

        protected override void OnPaintBorder(PaintEventArgs e)
        {
            base.OnPaintBorder(e);

            if (BorderThickness == 0)
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            Color borderColor = BorderColor;

            if (_hoverState)
                borderColor = ApplyColorFilter(borderColor, _hoverColorFilter);

            using (GraphicsPath path = new GraphicsPath())
            {
                SetButtonShape(path);

                using (Pen pen = new Pen(borderColor, BorderThickness))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.DashStyle = BorderStyle;

                    g.DrawPath(pen, path);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent == null)
            {
                base.OnPaintBackground(e);
                return;
            }

            Graphics g = e.Graphics;
            var state = g.Save();

            g.TranslateTransform(-Left, -Top);
            PaintEventArgs pea = new PaintEventArgs(g, Parent.ClientRectangle);
            InvokePaintBackground(Parent, pea);
            InvokePaint(Parent, pea);

            g.Restore(state);
        }
    }
}
