using StylizedComponents.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedLinkLabel
    {
        protected override void OnPaintContent(PaintEventArgs e)
        {
            base.OnPaintContent(e);

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.CompositingQuality = CompositingQuality.HighSpeed;

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
