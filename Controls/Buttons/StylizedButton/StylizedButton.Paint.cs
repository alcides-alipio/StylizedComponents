using StylizedComponents.Core;
using StylizedComponents.Core.builders;
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

            using (GraphicsPath path = RoundedPathBuilder.Create(
                Width, Height,
                _borderThickness, _cornerRadius,
                _autoRoundedCorners, _borderThickness / 2
            ))
            using (Brush brush = new SolidBrush(fillColor))
            {
                g.FillPath(brush, path);
            }

            Rectangle contentRect = ContentLayoutBuilder.CreateRoundedContent(
                Width, Height,
                _borderThickness, _cornerRadius,
                _autoRoundedCorners
            );

            bool hasIcon = _icon != null;
            bool hasText = !string.IsNullOrWhiteSpace(Text);

            if (!hasIcon && !hasText)
                return;

            Size textSize = hasText
                ? ContentLayoutBuilder.MeasureText(
                    Text,
                    Font)
                : Size.Empty;

            Size iconSize = hasIcon
                ? _iconSize
                : Size.Empty;

            Rectangle textAndIconRect = ContentLayoutBuilder.CreateTextAndImageContent(
                Text, Font,
                _icon, iconSize, _iconAlign,
                contentRect, _textAlign, _spacing);

            Rectangle textRect = ContentLayoutBuilder.CreateTextRectangle(
                textSize,
                iconSize, _iconAlign,
                textAndIconRect, _spacing);

            Rectangle iconRect = ContentLayoutBuilder.CreateIconRectangle(
            textSize,
            iconSize, _iconAlign,
            textAndIconRect, _spacing);

            if (hasIcon)
                g.DrawImage(_icon, iconRect);

            if (hasText)
            {
                TextRenderer.DrawText(
                    g,
                    Text,
                    Font,
                    textRect,
                    ForeColor,
                    TextFormatFlags.NoPadding);
            }
        }

        protected void PaintBorder(Graphics g)
        {
            if (BorderThickness == 0)
                return;

            Color borderColor = BorderColor;

            if (_hoverState)
                borderColor = Utils.ApplyColorFilter(
                    borderColor,
                    _hoverColorFilter,
                    _hoverFilterStrength);

            using (GraphicsPath path = RoundedPathBuilder.Create(
            Width, Height,
            _borderThickness, _cornerRadius,
            _autoRoundedCorners
            ))
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