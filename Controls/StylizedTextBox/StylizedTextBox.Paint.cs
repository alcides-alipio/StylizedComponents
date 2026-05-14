using StylizedComponents.Core;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        private Bitmap _backgroundCache = null;

        protected override void OnPaintContent(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            float half = BorderThickness / 2f;

            RectangleF rect = new RectangleF(
                half,
                half,
                Width - BorderThickness - 1,
                Height - BorderThickness - 1
            );

            float radius = AutoRoundedCorners ? Utils.CalculateFullRoundBorderRadius(Width, Height) : BorderRadius;
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

                if (BorderThickness == 0)
                    return;

                Color borderColor = BorderColor;

                if (_hoverState)
                    borderColor = _hoverBorderColor;

                if (_isFocused && borderColor != _hoverBorderColor)
                    borderColor = _hoverBorderColor;

                using (Pen pen = new Pen(borderColor, BorderThickness))
                {
                    pen.LineJoin = LineJoin.Round;
                    pen.DashStyle = BorderStyle;

                    g.DrawPath(pen, path);
                }
            }
        }

        private Point GetControlRelativeOffset(Control control)
        {
            int x = control.Left;
            int y = control.Top;

            if (control.Parent is ScrollableControl scrollable)
            {
                x += scrollable.AutoScrollPosition.X;
                y += scrollable.AutoScrollPosition.Y;
            }

            return new Point(x, y);
        }

        private Point GetParentRelativeOffset()
        {
            int x = Left;
            int y = Top;

            if (Parent is ScrollableControl scrollable)
            {
                x += scrollable.AutoScrollPosition.X;
                y += scrollable.AutoScrollPosition.Y;
            }

            return new Point(x, y);
        }

        private void BuildBackgroundCache()
        {
            _backgroundCache = new Bitmap(Parent.ClientSize.Width, Parent.ClientSize.Height);

            using (Graphics bmpG = Graphics.FromImage(_backgroundCache))
            {
                bmpG.Clear(Parent.BackColor);

                int zIndex = Parent.Controls.GetChildIndex(this);

                for (int i = Parent.Controls.Count - 1; i > zIndex; i--)
                {
                    Control control = Parent.Controls[i];

                    if (!control.Visible || control.Width <= 0 || control.Height <= 0)
                        continue;

                    using (Bitmap controlBitmap = new Bitmap(control.Width, control.Height))
                    {
                        control.DrawToBitmap(
                            controlBitmap,
                            new Rectangle(0, 0, control.Width, control.Height)
                        );

                        Point p = GetControlRelativeOffset(control);
                        bmpG.DrawImageUnscaled(controlBitmap, p.X, p.Y);
                    }
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

            if (!_useTransparentBackground)
            {
                base.OnPaintBackground(e);
                return;
            }

            if (_backgroundCache == null)
            {
                BuildBackgroundCache();
            }

            Point offset = GetParentRelativeOffset();

            var state = e.Graphics.Save();
            e.Graphics.TranslateTransform(-offset.X, -offset.Y);
            e.Graphics.DrawImageUnscaled(_backgroundCache, 0, 0);
            e.Graphics.Restore(state);
        }
    }
}
