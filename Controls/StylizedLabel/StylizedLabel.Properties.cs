using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StylizedComponents.Controls
{
    public partial class StylizedLabel
    {
        private ContentAlignment _textAlign = ContentAlignment.MiddleCenter;

        private Color _borderColor = Color.Black;
        private int _borderThickness = 0;
        private DashStyle _borderStyle = DashStyle.Solid;

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

        [DefaultValue(typeof(Color), "ControlText")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        public override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        #endregion

        #region Layout Properties

        [Browsable(true)]
        [Category("Layout")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(true)]
        public override bool AutoSize
        {
            get => base.AutoSize;
            set
            {
                if (base.AutoSize == value)
                    return;

                base.AutoSize = value;
                AjustSize(false);
            }
        }

        #endregion

        #endregion
    }
}
