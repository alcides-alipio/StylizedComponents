using System;
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
    }
}
