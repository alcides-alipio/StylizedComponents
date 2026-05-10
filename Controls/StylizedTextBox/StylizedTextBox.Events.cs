using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
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

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            UpdateAll();
            SetPlaceholder();
            Invalidate();
        }

        #region Placeholder Events

        private void RegisterTextBoxInputEvents()
        {
            _textBox.Enter += HandleTextBoxEnter;
            _textBox.Leave += HandleTextBoxLeave;

            _textBox.TextChanged += (s, e) => UpdateTextInput();

            _clientArea.Click += (s, e) => SetTextBoxFocus();
        }

        private void UnregisterTextBoxInputEvents()
        {
            _textBox.Enter -= (s, e) => UnsetPlaceholder();
            _textBox.Leave -= (s, e) => SetPlaceholder();

            _textBox.TextChanged -= (s, e) => UpdateTextInput();

            _clientArea.Click -= (s, e) => SetTextBoxFocus();
        }

        private void HandleTextBoxEnter(object sender, EventArgs e)
        {
            _isFocused = true;
            UnsetPlaceholder();
            Invalidate();
        }

        private void HandleTextBoxLeave(object sender, EventArgs e)
        {
            _isFocused = false;
            SetPlaceholder();
            Invalidate();
        }

        #endregion Placeholder Events

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
    }
}
