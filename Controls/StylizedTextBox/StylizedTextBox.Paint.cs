using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.None;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            int diameter = _borderRadius * 2;
            Rectangle rect = new Rectangle(1, 1, Width - 3, Height - 3);

            using (GraphicsPath path = new GraphicsPath())
            {
                if (diameter <= 0)
                    path.AddRectangle(rect);
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

                using (Pen pen = new Pen(_borderColor, _borderWidth))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Parent != null)
            {
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
}
