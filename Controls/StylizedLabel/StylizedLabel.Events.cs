using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedLabel
    {
        #region Paint Events

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent == null)
            {
                base.OnPaint(e);
                return;
            }

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            PaintContent(e);
            PaintBorder(e.Graphics);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent == null)
            {
                base.OnPaintBackground(e);
                return;
            }

            PaintBackground(e);
        }

        #endregion

        #region Foreground Events

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            AjustSize(false);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            AjustSize(false);
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            AjustSize(false);
        }

        #endregion

        #region Layout Events

        public override Size GetPreferredSize(Size proposedSize)
        {
            var flags =
                TextFormatFlags.SingleLine |
                TextFormatFlags.NoPadding;

            Size textSize = TextRenderer.MeasureText(
                Text,
                Font,
                proposedSize,
                flags
            );

            return new Size(
                textSize.Width + Padding.Horizontal,
                textSize.Height + Padding.Vertical
            );
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AjustSize(false);
            Invalidate();
        }

        #endregion
    }
}
