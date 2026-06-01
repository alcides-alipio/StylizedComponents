using StylizedComponents.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedButton
    {
        protected void PaintContent(Graphics g)
        {
            Color fillColor = _fillColor;

            if (_hoverState)
                fillColor = Utils.ApplyColorFilter(fillColor, _hoverColorFilter, _hoverFilterStrength);

            using (GraphicsPath path = RoundedPathBuilder.Create(new RoundedPathBuilder.RoundedPathOptions
            {
                Width = Width,
                Height = Height,
                BorderThickness = _borderThickness,
                BorderRadius = _cornerRadius,
                AutoRoundedCorners = _autoRoundedCorners
            }))
            using (Brush brush = new SolidBrush(fillColor))
            {
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

        protected void PaintBorder(Graphics g)
        {
            if (BorderThickness == 0)
                return;

            Color borderColor = BorderColor;

            if (_hoverState)
                borderColor = Utils.ApplyColorFilter(borderColor, _hoverColorFilter, _hoverFilterStrength);

            using (GraphicsPath path = RoundedPathBuilder.Create(new RoundedPathBuilder.RoundedPathOptions
            {
                Width = Width,
                Height = Height,
                BorderThickness = BorderThickness,
                BorderRadius = _cornerRadius,
                AutoRoundedCorners = AutoRoundCorners
            }))
            using (Pen pen = new Pen(borderColor, BorderThickness))
            {
                pen.LineJoin = LineJoin.Round;
                pen.DashStyle = BorderStyle;

                g.DrawPath(pen, path);
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
