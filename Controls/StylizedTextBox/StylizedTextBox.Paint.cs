using StylizedComponents.Core;
using StylizedComponents.Core.models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        protected void PaintContent(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (GraphicsPath path = RoundedPathBuilder.Create(new RoundedPathOptions
            {
                Width = Width,
                Height = Height,
                BorderThickness = _borderThickness,
                BorderRadius = _cornerRadius,
                AutoRoundedCorners = _autoRoundedCorners
            }))
            {
                using (SolidBrush brush = new SolidBrush(_fillColor))
                {
                    g.FillPath(brush, path);
                }

                if (BorderThickness == 0)
                    return;

                Color borderColor = _borderColor;

                if (_hoverState)
                    borderColor = _hoverBorderColor;

                if (_isFocused && borderColor != _hoverBorderColor)
                    borderColor = _hoverBorderColor;

                using (Pen pen = new Pen(borderColor, BorderThickness))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.DashStyle = _borderStyle;

                    g.DrawPath(pen, path);
                }
            }
        }

        protected void PaintBackground(PaintEventArgs e)
        {
            if (BackColor != Color.Transparent)
            {
                base.OnPaintBackground(e);
                return;
            }

            _transparentBackgroundRenderer.Paint(e.Graphics);
        }
    }
}
