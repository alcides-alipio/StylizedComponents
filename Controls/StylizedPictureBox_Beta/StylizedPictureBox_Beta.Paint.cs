using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox_Beta
    {
        private Bitmap _backgroundCache = null;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!DesignMode)
                return;

            Graphics g = e.Graphics;

            Rectangle rect = new Rectangle(
                0,
                0,
                Width - 1,
                Height - 1
            );

            using (Pen pen = new Pen(Color.Gray, 1))
            {
                pen.Alignment = PenAlignment.Inset;
                pen.DashStyle = DashStyle.Dash;

                g.DrawRectangle(pen, rect);
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
