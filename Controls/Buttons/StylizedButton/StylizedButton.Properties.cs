using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StylizedComponents.Controls
{
    public partial class StylizedButton
    {
        private ContentAlignment _textAlign = ContentAlignment.MiddleCenter;
        private Color _fillColor = Color.FromArgb(94, 148, 255);

        private Color _borderColor = Color.Black;
        private int _borderThickness = 0;
        private DashStyle _borderStyle = DashStyle.Solid;

        private Color _hoverColorFilter = Color.Black;
        private float _hoverFilterStrength = 0.2f;

        private int _cornerRadius = 0;
        private bool _autoRoundedCorners = false;

        #region Foreground Properties

        [Category("StylizedComponents Properties")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        [Description("Sets the text alignment for this control.")]
        public ContentAlignment TextAlign
        {
            get => _textAlign;
            set
            {
                if (value == _textAlign)
                    return;

                _textAlign = value;
                Invalidate();
            }
        }

        [Category("StylizedComponents Properties")]
        [DefaultValue(typeof(Color), "94, 148, 255")]
        [Description("Sets the background color of the button for this control.")]
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                if (value == _fillColor)
                    return;

                _fillColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Border Properties

        [Category("StylizedComponents Properties")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("Sets the border color for this control.")]
        public Color BorderColor
        {
            get => _borderColor;
            set
            {
                if (value == _borderColor)
                    return;

                _borderColor = value;
                Invalidate();
            }
        }

        [Category("StylizedComponents Properties")]
        [DefaultValue(0)]
        [Description("Sets the border thickness for this control.")]
        public int BorderThickness
        {
            get => _borderThickness;
            set
            {
                if (value == _borderThickness)
                    return;

                _borderThickness = value;
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
                Invalidate();
            }
        }

        #endregion

        #region Hover Properties

        [Category("StylizedComponents Properties")]
        [DefaultValue(typeof(Color), "Black")]
        [Description("Sets the hover filter color for this control.")]
        public Color HoverColorFilter
        {
            get => _hoverColorFilter;
            set
            {
                if (value == _hoverColorFilter)
                    return;

                _hoverColorFilter = value;
                Invalidate();
            }
        }

        [Category("StylizedComponents Properties")]
        [DefaultValue(0.2f)]
        [Description("Sets the Hover filter strength for this control.")]
        public float HoverFilterStrength
        {
            get => _hoverFilterStrength;
            set
            {
                if (value == _hoverFilterStrength)
                    return;

                _hoverFilterStrength = value;
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
                Invalidate();
            }
        }

        #endregion

        #region Overridden Properties

        #region Foreground Properties

        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        [Description("Sets the text font for this control")]
        public override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        [Description("Sets the text for this control")]
        public override string Text
        {
            get => base.Text;
            set
            {
                if (value == base.Text)
                    return;

                base.Text = value;
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "White")]
        [Description("Sets the text color for this control.")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        #endregion

        #region Background Properties

        [Description("Sets the background color for this control.")]
        override public Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        #endregion

        #endregion

        #region Deprecated Properties

        #region Shape Properties

        [Browsable(false)]
        [Obsolete("Use AutoRoundedCorners instead.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoRoundCorners
        {
            get => _autoRoundedCorners;
            set
            {
                if (value == _autoRoundedCorners)
                    return;

                _autoRoundedCorners = value;
                Invalidate();
            }
        }

        #endregion

        #endregion
    }
}
