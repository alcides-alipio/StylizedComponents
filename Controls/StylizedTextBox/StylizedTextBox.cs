using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace StylizedComponents.Controls
{
    [ToolboxItem(true)]
    [Designer(typeof(TextBoxStylizedDesigner))]
    public partial class StylizedTextBox : StylizedControl
    {
        private TextBox _textBox;
        private Panel _clientArea;
        private bool _isPlaceholderActive = false;

        public StylizedTextBox()
        {
            base.BackColor = SystemColors.Window;
            Cursor = Cursors.IBeam;

            _clientArea = new Panel
            {
                Margin = Padding.Empty
            };
            _textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Margin = Padding.Empty,
                Text = string.Empty
            };
            Controls.Add(_clientArea);
            _clientArea.Controls.Add(_textBox);

            Resize += (s, e) => UpdateAll();
            BackColorChanged += (s, e) => UpdateColors();
            ForeColorChanged += (s, e) => UpdateColors();
            Click += (s, e) => SetTextBoxFocus();

            _clientArea.Click += (s, e) => SetTextBoxFocus();

            _textBox.Enter += (s, e) => UnsetPlaceholder();
            _textBox.Leave += (s, e) => SetPlaceholder();
            _textBox.TextChanged += (s, e) => UpdateTextInput();
        }

        private void UpdateAll()
        {
            UpdateBorder();
            UpdateTextBox();
        }

        private void UpdateColors()
        {
            _textBox.BackColor = BackColor;

            if (_isPlaceholderActive)
                _textBox.ForeColor = PlaceholderColor;
            else
                _textBox.ForeColor = ForeColor;

            Invalidate();
        }

        private void UpdateBorder()
        {
            UpdateTextBox();
            Invalidate();
        }

        private void UpdateTextBox()
        {
            int border = (int)Math.Ceiling(_borderWidth);
            int cornerInset = (int)Math.Ceiling(_borderRadius / 2f);
            int inset = border + cornerInset;

            int x = inset;
            int y = inset;
            int w = Math.Max(0, ClientSize.Width - (inset * 2));
            int h = Math.Max(0, ClientSize.Height - (inset * 2));

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