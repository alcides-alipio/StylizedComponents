using StylizedComponents.Core;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedLabel
    {
        protected void PaintContent(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

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
