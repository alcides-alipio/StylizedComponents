using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox
    {
        protected override void OnPaintContent(PaintEventArgs e)
        {
            base.OnPaintContent(e);

            if (_image == null)
                return;

            Graphics g = e.Graphics;

            Rectangle destRect;

            switch (_sizeMode)
            {
                case PictureBoxSizeMode.Normal:
                case PictureBoxSizeMode.AutoSize:
                    destRect = new Rectangle(0, 0, _image.Width, _image.Height);
                    g.DrawImage(_image, destRect);
                    break;

                case PictureBoxSizeMode.StretchImage:
                    destRect = ClientRectangle;
                    g.DrawImage(_image, destRect);
                    break;

                case PictureBoxSizeMode.CenterImage:
                    destRect = new Rectangle(
                        (ClientSize.Width - _image.Width) / 2,
                        (ClientSize.Height - _image.Height) / 2,
                        _image.Width,
                        _image.Height);

                    g.DrawImage(_image, destRect);
                    break;

                case PictureBoxSizeMode.Zoom:
                    float ratioX = (float)ClientSize.Width / _image.Width;
                    float ratioY = (float)ClientSize.Height / _image.Height;
                    float ratio = Math.Min(ratioX, ratioY);

                    int width = (int)(_image.Width * ratio);
                    int height = (int)(_image.Height * ratio);

                    destRect = new Rectangle(
                        ((ClientSize.Width - width) / 2) + 1,
                        ((ClientSize.Height - height) / 2) + 1,
                        width - 2,
                        height - 2);

                    g.DrawImage(_image, destRect);
                    break;
            }
        }

        protected override void OnPaintBorder(PaintEventArgs e)
        {
            base.OnPaintBorder(e);

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

            Point offset = GetParentRelativeOffset();

            using (Bitmap bmp = new Bitmap(Parent.ClientSize.Width, Parent.ClientSize.Height))
            using (Graphics bmpG = Graphics.FromImage(bmp))
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

                var state = e.Graphics.Save();
                e.Graphics.TranslateTransform(-offset.X, -offset.Y);
                e.Graphics.DrawImageUnscaled(bmp, 0, 0);
                e.Graphics.Restore(state);
            }
        }
    }
}
