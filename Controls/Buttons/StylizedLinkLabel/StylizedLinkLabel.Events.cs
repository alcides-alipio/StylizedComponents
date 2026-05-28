using System;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedLinkLabel
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
            _hoverState = true;
            Invalidate();
        }

        private void HandleMouseLeave(object sender, EventArgs e)
        {
            _hoverState = false;
            Invalidate();
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
