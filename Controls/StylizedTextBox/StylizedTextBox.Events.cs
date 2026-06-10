using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        #region Lifecycle Events

        protected override void OnCreateControl()
        {
            SuspendLayout();

            UpdateTextBox();
            UpdateColors();
            SetPlaceholder();
            Invalidate();

            ResumeLayout(false);

            base.OnCreateControl();
        }

        #endregion

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

        #region Placeholder Events

        private void RegisterTextBoxInputEvents()
        {
            _textBox.Enter += HandleTextBoxEnter;
            _textBox.Leave += HandleTextBoxLeave;

            _textBox.TextChanged += (s, e) => UpdateTextInput();
            _textBox.KeyDown += (s, e) => OnKeyDown(e);
            _textBox.KeyPress += (s, e) => OnKeyPress(e);
            _textBox.KeyUp += (s, e) => OnKeyUp(e);
            _textBox.PreviewKeyDown += (s, e) => OnPreviewKeyDown(e);

            _clientArea.Click += (s, e) => SetTextBoxFocus();
        }

        private void UnregisterTextBoxInputEvents()
        {
            _textBox.Enter -= (s, e) => UnsetPlaceholder();
            _textBox.Leave -= (s, e) => SetPlaceholder();

            _textBox.TextChanged -= (s, e) => UpdateTextInput();
            _textBox.KeyDown -= (s, e) => OnKeyDown(e);
            _textBox.KeyPress -= (s, e) => OnKeyPress(e);
            _textBox.KeyUp -= (s, e) => OnKeyUp(e);
            _textBox.PreviewKeyDown -= (s, e) => OnPreviewKeyDown(e);

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

        #region Foreground Events

        override protected void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);

            UpdateColors();
        }

        #endregion

        #region Background Events

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);

            UpdateColors();
        }

        #endregion

        #region Layout Events

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            UpdateTextBox();
        }

        #endregion
    }
}
