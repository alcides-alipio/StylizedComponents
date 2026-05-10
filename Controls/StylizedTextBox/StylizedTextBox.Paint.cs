using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;

            float half = _borderThickness / 2f;

            RectangleF rect = new RectangleF(
                half,
                half,
                Width - _borderThickness - 1,
                Height - _borderThickness - 1
            );

            float radius = _borderRadius;
            float diameter = radius * 2f;

            using (GraphicsPath path = new GraphicsPath())
            {
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

                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    g.FillPath(brush, path);
                }

                if (_borderThickness == 0)
                    return;

                Color borderColor = _borderColor;

                if (_hoverState)
                    borderColor = _hoverBorderColor;

                if (_isFocused && borderColor != _hoverBorderColor)
                    borderColor = _hoverBorderColor;

                using (Pen pen = new Pen(borderColor, _borderThickness))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.DashStyle = _borderStyle;

                    g.DrawPath(pen, path);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
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
}
