using StylizedComponents.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedLinkLabel
    {
        protected void PaintContent(Graphics g)
        {
            var flags = TextFormatFlags.HorizontalCenter |
               TextFormatFlags.VerticalCenter |
               TextFormatFlags.SingleLine |
               TextFormatFlags.NoPadding;

            Color foreColor = ForeColor;

            if (_hoverState)
                foreColor = Utils.ApplyColorFilter(foreColor, _hoverColorFilter, _hoverFilterStrength);

            Font font = Font;
            Font fontHover = Font;

            switch (_linkBehavior)
            {
                case LinkBehavior.SystemDefault:
                case LinkBehavior.AlwaysUnderline:
                    font = new Font(Font, FontStyle.Underline);
                    fontHover = new Font(Font, FontStyle.Underline);
                    break;

                case LinkBehavior.HoverUnderline:
                    font = Font;
                    fontHover = new Font(Font, FontStyle.Underline);
                    break;

                case LinkBehavior.NeverUnderline:
                    font = Font;
                    fontHover = Font;
                    break;
            }

            TextRenderer.DrawText(
                g,
                Text,
                _hoverState ? fontHover : font,
                ClientRectangle,
                foreColor,
                flags
            );
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
