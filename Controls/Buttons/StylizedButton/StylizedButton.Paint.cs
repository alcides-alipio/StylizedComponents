using StylizedComponents.Core;
using StylizedComponents.Core.models;
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

            using (GraphicsPath path = RoundedPathBuilder.Create(new RoundedPathOptions
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

            var flags =
               TextFormatFlags.SingleLine |
               TextFormatFlags.NoPadding;

            switch (_textAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopRight:
                case ContentAlignment.TopCenter:
                    flags = flags | TextFormatFlags.Top;
                    break;

                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.MiddleCenter:
                    flags = flags | TextFormatFlags.VerticalCenter;
                    break;

                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomRight:
                case ContentAlignment.BottomCenter:
                    flags = flags | TextFormatFlags.Bottom;
                    break;
            }

            switch (_textAlign)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    flags = flags | TextFormatFlags.Left;
                    break;

                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    flags = flags | TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    flags = flags | TextFormatFlags.Right;
                    break;
            }

            Rectangle textRect = RoundedContentBounds.Create(new RoundedPathOptions
            {
                Width = Width,
                Height = Height,
                BorderThickness = BorderThickness,
                BorderRadius = _cornerRadius,
                AutoRoundedCorners = _autoRoundedCorners
            });

            TextRenderer.DrawText(
                g,
                Text,
                Font,
                textRect,
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

            using (GraphicsPath path = RoundedPathBuilder.Create(new RoundedPathOptions
            {
                Width = Width,
                Height = Height,
                BorderThickness = BorderThickness,
                BorderRadius = _cornerRadius,
                AutoRoundedCorners = _autoRoundedCorners
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
