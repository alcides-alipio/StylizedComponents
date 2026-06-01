using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        private string _textInput = string.Empty;
        private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
        private bool _useSystemPasswordChar = false;

        private Color _fillColor = Color.White;

        private string _placeholderText = "Enter text...";
        private Color _placeholderColor = Color.FromArgb(193, 200, 207);

        private Color _borderColor = Color.FromArgb(213, 218, 223);
        private int _borderThickness = 1;
        private DashStyle _borderStyle = DashStyle.Solid;

        private Color _hoverBorderColor = Color.DodgerBlue;

        private int _cornerRadius = 0;
        private bool _autoRoundedCorners = false;

        #region Foreground Properties

        [Browsable(true)]        
        [Category("StylizedComponents Properties")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set
            {
                if (value == ContentAlignment.MiddleRight || value == ContentAlignment.TopRight || value == ContentAlignment.BottomRight)
                    _textBox.TextAlign = HorizontalAlignment.Right;

                if (value == ContentAlignment.MiddleLeft || value == ContentAlignment.TopLeft || value == ContentAlignment.BottomLeft)
                    _textBox.TextAlign = HorizontalAlignment.Left;

                if (value == ContentAlignment.MiddleCenter || value == ContentAlignment.TopCenter || value == ContentAlignment.BottomCenter)
                    _textBox.TextAlign = HorizontalAlignment.Center;

                if (value == ContentAlignment.TopLeft || value == ContentAlignment.TopCenter || value == ContentAlignment.TopRight)
                    _textBox.Dock = DockStyle.Top;

                if (value == ContentAlignment.MiddleLeft || value == ContentAlignment.MiddleCenter || value == ContentAlignment.MiddleRight)
                    _textBox.Dock = DockStyle.None;

                if (value == ContentAlignment.BottomLeft || value == ContentAlignment.BottomCenter || value == ContentAlignment.BottomRight)
                    _textBox.Dock = DockStyle.Bottom;

                _textAlign = value;
            }
        }

        [Browsable(true)]        
        [Category("StylizedComponents Properties")]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]

        public bool UseSystemPasswordChar
        {
            get => _useSystemPasswordChar;
            set
            {
                _useSystemPasswordChar = value;

                if (!_isPlaceholderActive)
                    _textBox.UseSystemPasswordChar = value;
            }
        }

        #endregion

        #region Background Properties

        [Category("StylizedComponents Properties")]
        [DefaultValue(typeof(Color), "White")]
        [Description("Sets the color of the text area.")]
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                if (value == _fillColor)
                    return;

                _fillColor = value;

                UpdateColors();
                Invalidate();
            }
        }

        #endregion

        #region Placeholder Properties

        [Category("StylizedComponents Properties")]
        [DefaultValue("Enter text...")]
        [Description("Sets the text displayed when the field is empty.")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                if (value == _placeholderText)
                    return;

                _placeholderText = value;

                SetPlaceholder();
                Invalidate();
            }
        }

        [Category("StylizedComponents Properties")]
        [DefaultValue(typeof(Color), "193, 200, 207")]
        [Description("Sets the color used for the placeholder text.")]
        public Color PlaceholderColor
        {
            get => _placeholderColor;
            set
            {
                if (value == _placeholderColor)
                    return;

                _placeholderColor = value;

                UpdateColors();
                Invalidate();
            }
        }

        #endregion

        #region Border Properties

        [Category("StylizedComponents Properties")]
        [DefaultValue(typeof(Color), "213, 218, 223")]
        [Description("Sets the border color for this control.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                if (value == _borderColor)
                    return;

                _borderColor = value;

                UpdateColors();
                Invalidate();
            }
        }

        [Category("StylizedComponents Properties")]
        [DefaultValue(1)]
        [Description("Sets the border thickness for this control.")]
        public int BorderThickness
        {
            get => _borderThickness;
            set
            {
                if (value == _borderThickness)
                    return;

                _borderThickness = value;

                UpdateTextBox();
                Invalidate();
            }
        }

        [Category("StylizedComponents Properties")]
        [DefaultValue(DashStyle.Solid)]
        [Description("Sets the border style for this control.")]
        public DashStyle BorderStyle
        {
            get => _borderStyle;
            set
            {
                if (value == _borderStyle)
                    return;

                _borderStyle = value;

                UpdateTextBox();
                Invalidate();
            }
        }

        #endregion

        #region Hover Properties

        [Category("StylizedComponents Properties")]
        [DefaultValue(typeof(Color), "DodgerBlue")]
        [Description("Sets the border color on hover for this control.")]

        public Color HoverBorderColor
        {
            get => _hoverBorderColor;
            set
            {
                _hoverBorderColor = value;

                UpdateColors();
                Invalidate();
            }
        }

        #endregion

        #region Shape Properties

        [Category("StylizedComponents Properties")]
        [DefaultValue(0)]
        [Description("Sets the corner radius for this control.")]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                if (value == _cornerRadius)
                    return;

                _cornerRadius = value;

                UpdateTextBox();
                Invalidate();
            }
        }

        [Category("StylizedComponents Properties")]
        [DefaultValue(false)]
        [Description("Sets the value that indicates whether the corners will be rounded automatically.")]
        public bool AutoRoundedCorners
        {
            get => _autoRoundedCorners;
            set
            {
                if (value == _autoRoundedCorners)
                    return;

                _autoRoundedCorners = value;

                UpdateTextBox();
                Invalidate();
            }
        }

        #endregion

        #region Outhers Properties

        public override Cursor Cursor
        {
            get => base.Cursor;
            set
            {
                if (value == base.Cursor)
                    return;

                _textBox.Cursor = value;
                base.Cursor = value;
            }
        }

        #endregion

        #region Overridden Properties

        #region Foreground Properties

        [DefaultValue(typeof(Color), "ControlText")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => _isPlaceholderActive ? string.Empty : _textInput;
            set
            {
                if (value == _textInput)
                    return;

                if (string.IsNullOrWhiteSpace(value))
                {
                    _textBox.Text = value;
                    _textInput = value;

                    SetPlaceholder();
                    return;
                }

                UnsetPlaceholder();
                _textBox.Text = value;
                _textInput = value;
            }
        }

        #endregion

        #endregion
    }
}
