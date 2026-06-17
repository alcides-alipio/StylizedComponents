using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedButton
    {
        private bool _hoverState;

        public bool HoverState
        {
            get => _hoverState;
            private set
            {
                if (_hoverState == value)
                    return;

                _hoverState = value;
                Invalidate();
            }
        }

        #region Paint Events

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent == null)
            {
                base.OnPaint(e);
                return;
            }

            Graphics g = e.Graphics;

            var save = g.Save();
            float translate = _cornerRadius != 0 ? 0.1f : (_autoRoundedCorners == false ? 0.0f : 0.1f);
            g.TranslateTransform(translate, translate);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.CompositingQuality = CompositingQuality.HighSpeed;

            PaintContent(g);
            PaintBorder(g);

            g.Restore(save);
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

        #region Hover Events

        private void RegisterHoverEvents(Control control)
        {
            control.MouseEnter += HandleMouseEnter;
            control.MouseLeave += HandleMouseLeave;

            foreach (Control child in control.Controls)
                RegisterHoverEvents(child);
        }
        private void UnregisterHoverEvents(Control control)
        {
            control.MouseEnter -= HandleMouseEnter;
            control.MouseLeave -= HandleMouseLeave;

            foreach (Control child in control.Controls)
                UnregisterHoverEvents(child);
        }

        private void HandleMouseEnter(object sender, EventArgs e)
        {
            if (Parent == null)
                return;

            _hoverState = true;
            Invalidate();
        }

        private void HandleMouseLeave(object sender, EventArgs e)
        {
            if (Parent == null)
                return;

            _hoverState = false;
            Invalidate();
        }

        #endregion

        #region Layout Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
            Invalidate();
        }

        #endregion
    }
}
