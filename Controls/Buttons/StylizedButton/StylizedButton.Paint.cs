using StylizedComponents.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedButton
    {
        private void SetButtonShape(GraphicsPath path)
        {
            float half = BorderThickness / 2f;

            RectangleF rect = new RectangleF(
                half,
                half,
                Width - BorderThickness,
                Height - BorderThickness
            );

            float radius = AutoRoundedCorners ? Utils.CalculateFullRoundBorderRadius(Width, Height) : BorderRadius;
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
                backColor = Utils.ApplyColorFilter(backColor, _hoverColorFilter, _hoverFilterStrength);

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
                borderColor = Utils.ApplyColorFilter(borderColor, _hoverColorFilter, _hoverFilterStrength);

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
