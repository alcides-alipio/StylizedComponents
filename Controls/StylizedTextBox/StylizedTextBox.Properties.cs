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

        private Color _hoverBorderColor = Color.DodgerBlue;

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
        [Description("Border color on mouse hover.")]
        [DefaultValue(typeof(Color), "DodgerBlue")]
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

        #region Foreground Properties

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
