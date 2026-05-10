using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.Mime;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        private string _textInput = string.Empty;
        private bool _useSystemPasswordChar = false;

        private string _placeholderText = string.Empty;
        private Color _placeholderColor = Color.FromArgb(193, 200, 207);

        private int _borderRadius = 0;
        private int _borderThickness = 1;
        private Color _borderColor = Color.FromArgb(213, 218, 223);
        private DashStyle _borderStyle = DashStyle.Solid;
        private Color _hoverBorderColor = Color.Blue;

        #region Text Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue("")]
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

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Behavior")]
        [DefaultValue(false)]
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

        [DefaultValue(typeof(Color), "125, 137, 149")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        #endregion

        #region Placeholder Properties

        [Category("Appearance")]
        [Description("Text displayed when the field is empty.")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get => _placeholderText;
            set
            {
                if (value != _placeholderText)
                {
                    _placeholderText = value;
                    SetPlaceholder();
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("Color used for the placeholder text.")]
        [DefaultValue(typeof(Color), "193, 200, 207")]
        public Color PlaceholderColor
        {
            get => _placeholderColor;
            set
            {
                if (value != _placeholderColor)
                {
                    _placeholderColor = value;
                    UpdateColors();
                }
            }
        }

        #endregion

        #region Border Properties

        [Category("Appearance")]
        [Description("Corner radius of the control border.")]
        [DefaultValue(0)]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                if (value != _borderRadius)
                {
                    _borderRadius = value;
                    UpdateBorder();
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("Thickness of the control border.")]
        [DefaultValue(1)]
        public int BorderThickness
        {
            get => _borderThickness;
            set
            {
                if (value != _borderThickness)
                {
                    _borderThickness = value;
                    UpdateBorder();
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("Color of the control border.")]
        [DefaultValue(typeof(Color), "213, 218, 223")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                if (value != _borderColor)
                {
                    _borderColor = value;
                    UpdateColors();
                }
            }
        }

        [Category("Appearance")]
        [Description("Style of the control border line.")]
        [DefaultValue(DashStyle.Solid)]
        public DashStyle BorderStyle
        {
            get => _borderStyle;
            set
            {
                if (value != _borderStyle)
                {
                    _borderStyle = value;
                    UpdateBorder();
                }
            }
        }
        [Category("Appearance")]
        [Description("Border color on mouse hover.")]
        [DefaultValue(DashStyle.Solid)]
        public Color HoverBorderColor
        {
            get => _hoverBorderColor;
            set
            {
                _hoverBorderColor = value;
                UpdateColors();
            }
        }

        #endregion

        #region Background Properties

        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Image BackgroundImage
        {
            get => base.BackgroundImage;
            set => base.BackgroundImage = value;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ImageLayout BackgroundImageLayout
        {
            get => base.BackgroundImageLayout;
            set => base.BackgroundImageLayout = value;
        }

        #endregion

        #region Outhers Properties

        public override Cursor Cursor
        {
            get => base.Cursor;
            set
            {
                _textBox.Cursor = value;
                base.Cursor = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Padding Padding
        {
            get => base.Padding;
            set => base.Padding = value;
        }

        #endregion
    }
}
