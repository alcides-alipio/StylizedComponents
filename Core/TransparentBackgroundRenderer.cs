using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace StylizedComponents.Core
{
    internal sealed class TransparentBackgroundRenderer
    {
        private readonly Control _owner;
        private Bitmap _cache;
        private bool _cacheDirty = true;

        public TransparentBackgroundRenderer(Control owner)
        {
            _owner = owner;
        }

        public void Invalidade()
        {
            _cacheDirty = true;
        }

        public void Paint(Graphics g)
        {
            if (_cacheDirty || _cache == null)
            {
                BuildCache();
                _cacheDirty = false;
            }

            if (_cache == null)
                return;

            Point offset = GetParentRelativeOffset();

            var state = g.Save();
            g.TranslateTransform(-offset.X, -offset.Y);
            g.DrawImageUnscaled(_cache, 0, 0);
            g.Restore(state);
        }

        private Point GetParentRelativeOffset()
        {
            int x = _owner.Left;
            int y = _owner.Top;

            if (_owner.Parent is ScrollableControl scrollable)
            {
                x += scrollable.AutoScrollPosition.X;
                y += scrollable.AutoScrollPosition.Y;
            }

            return new Point(x, y);
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

        private void BuildCache()
        {
            _cache?.Dispose();
            _cache = null;

            _cache = new Bitmap(
                _owner.Parent.ClientSize.Width,
                _owner.Parent.ClientSize.Height
            );

            using (Graphics bmpG = Graphics.FromImage(_cache))
            {
                bmpG.Clear(_owner.Parent.BackColor);

                int zIndex = _owner.Parent.Controls.GetChildIndex(_owner);

                for (int i = _owner.Parent.Controls.Count - 1; i > zIndex; i--)
                {
                    Control control = _owner.Parent.Controls[i];

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
    }
}
