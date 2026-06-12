using StylizedComponents.Core;
using StylizedComponents.Core.models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [Designer(typeof(StylizedTextBoxDesigner))]
    [DesignerCategory("Code")]
    public partial class StylizedTextBox : Control
    {
        private readonly TransparentBackgroundRenderer _transparentBackgroundRenderer;

        private TextBox _textBox;
        private Panel _clientArea;
        private bool _isPlaceholderActive = false;
        private bool _isFocused = false;

        public StylizedTextBox() : base()
        {
            _transparentBackgroundRenderer =
                new TransparentBackgroundRenderer(this);

            base.Size = new Size(200, 36);
            base.Cursor = Cursors.IBeam;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);
            UpdateStyles();

            _clientArea = new Panel
            {
                Margin = Padding.Empty,
                BackColor = _fillColor
            };
            _textBox = new TextBox
            {
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Margin = Padding.Empty,
                ForeColor = ForeColor,
                BackColor = _fillColor,
            };
            Controls.Add(_clientArea);
            _clientArea.Controls.Add(_textBox);

            RegisterTextBoxInputEvents();
            RegisterHoverEvents(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnregisterTextBoxInputEvents();
                UnregisterHoverEvents(this);
            }

            base.Dispose(disposing);
        }

        private void UpdateColors()
        {
            if (IsDisposed || Disposing)
                return;

            if (_textBox == null)
                return;

            if (!_textBox.IsHandleCreated)
                return;

            _textBox.BackColor = _fillColor;
            _clientArea.BackColor = _fillColor;

            if (_isPlaceholderActive)
                _textBox.ForeColor = PlaceholderColor;
            else
                _textBox.ForeColor = ForeColor;
        }

        private void UpdateTextBox()
        {
            if (IsDisposed || Disposing)
                return;

            if (_clientArea == null || _textBox == null)
                return;

            if (_textBox.Parent == null)
                return;

            if (ClientSize.Width <= 0 || ClientSize.Height <= 0)
                return;

            _clientArea.Bounds = ContentLayoutBuilder.CreateRoundedContent(new RoundedPathOptions
            {
                Width = Width,
                Height = Height,
                BorderThickness = BorderThickness,
                BorderRadius = _cornerRadius,
                AutoRoundedCorners = AutoRoundedCorners
            });

            _textBox.Location = new Point(
                _textBox.Location.X,
                (_textBox.Parent.ClientSize.Height - _textBox.ClientSize.Height) / 2
            );
            _textBox.Size = new Size(_clientArea.Width, _textBox.Size.Height);

            Invalidate();
        }

        private void UpdateTextInput()
        {
            if (_isPlaceholderActive)
            {
                if (_textInput != string.Empty)
                    _textBox.Text = _textInput;

                return;
            }

            _textInput = _textBox.Text;

            if (_isPlaceholderActive)
                OnTextChanged(EventArgs.Empty);
        }

        private void SetTextBoxFocus() => _textBox.Focus();

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(_textBox.Text) || _isPlaceholderActive)
            {
                _isPlaceholderActive = true;
                _textBox.Text = _placeholderText;
                _textInput = string.Empty;
                _textBox.ForeColor = _placeholderColor;
                _textBox.UseSystemPasswordChar = false;
            }
        }

        private void UnsetPlaceholder()
        {
            if (_isPlaceholderActive)
            {
                _isPlaceholderActive = false;
                _textBox.Text = string.Empty;
                _textBox.ForeColor = ForeColor;
                _textBox.UseSystemPasswordChar = _useSystemPasswordChar;
            }
        }
    }
}