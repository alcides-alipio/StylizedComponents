using System.ComponentModel;
using System.Drawing;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        private int _borderRadius = 0;
        private float _borderWidth = 2f;
        private Color _borderColor = Color.FromArgb(213, 218, 223);
        private string _placeholderText = string.Empty;
        private Color _placeholderColor = Color.FromArgb(193, 200, 207);
        private string _textInput = string.Empty;
        private bool _useSystemPasswordChar = false;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
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

        [Category("Appearance")]
        [Description("")]
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
        [Description("")]
        [DefaultValue(typeof(Color), "193; 200; 207")]
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

        [Category("Appearance")]
        [Description("Raio dos cantos")]
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
        [Description("Espessura da borda")]
        [DefaultValue(1f)]
        public float BorderWidth
        {
            get => _borderWidth;
            set
            {
                if (value != _borderWidth)
                {
                    _borderWidth = value;
                    UpdateBorder();
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        [Description("Cor da borda")]
        [DefaultValue(typeof(Color), "213; 218; 223")]
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
    }
}
