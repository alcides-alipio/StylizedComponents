using StylizedComponents.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [ToolboxItem(true)]
    [Designer(typeof(StylizedTextBoxDesigner))]
    public partial class StylizedTextBox : StylizedControl
    {
        private TextBox _textBox;
        private Panel _clientArea;
        private bool _isPlaceholderActive = false;
        private bool _isFocused = false;

        public StylizedTextBox()
        {
            base.Size = new Size(200, 36);
            base.BackColor = Color.White;
            base.ForeColor = Color.FromArgb(125, 137, 149);
            base.Cursor = Cursors.IBeam;

            _clientArea = new Panel
            {
                Margin = Padding.Empty
            };
            _textBox = new TextBox
            {
                BorderStyle = System.Windows.Forms.BorderStyle.None,
                Margin = Padding.Empty,
                ForeColor = ForeColor
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

            _textBox.BackColor = BackColor;

            if (_isPlaceholderActive)
                _textBox.ForeColor = PlaceholderColor;
            else
                _textBox.ForeColor = ForeColor;

            Invalidate();
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

            float radius = AutoRoundedCorners ? Utils.CalculateFullRoundBorderRadius(Width, Height) : BorderRadius;

            double angleRad = 45.0 * (Math.PI / 180.0);
            int cornerInset = (int)(radius * (1.0 - Math.Cos(angleRad)));
            int inset = BorderThickness + cornerInset;

            int x = inset + 3;
            int y = inset;
            int w = Math.Max(0, ClientSize.Width - (inset * 2)) - 6;
            int h = Math.Max(0, ClientSize.Height - (inset * 2)) - 1;

            _clientArea.Bounds = new Rectangle(x, y, w, h);
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