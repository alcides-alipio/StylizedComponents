using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StylizedComponents.Controls
{
    public partial class StylizedControl
    {
        private int _borderRadius = 0;
        private int _borderThickness = 1;
        private Color _borderColor = Color.FromArgb(213, 218, 223);
        private DashStyle _borderStyle = DashStyle.Solid;

        #region Border Properties

        [Category("Appearance")]
        [Description("Corner radius of the control border.")]
        [DefaultValue(0)]
        public int BorderRadius
        {
            get => _borderRadius;
            set
            {
                if (_borderRadius == value)
                    return;

                _borderRadius = value;

                OnBorderRadiusChanged(EventArgs.Empty);
                BorderRadiusChanged?.Invoke(this, EventArgs.Empty);

                Invalidate();
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
                if (_borderThickness == value)
                    return;

                _borderThickness = value;

                OnBorderThicknessChanged(EventArgs.Empty);
                BorderThicknessChanged?.Invoke(this, EventArgs.Empty);

                Invalidate();
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
                if (_borderColor == value)
                    return;

                _borderColor = value;

                OnBorderColorChanged(EventArgs.Empty);
                BorderColorChanged?.Invoke(this, EventArgs.Empty);

                Invalidate();
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
                if (_borderStyle == value)
                    return;

                _borderStyle = value;

                OnBorderStyleChanged(EventArgs.Empty);
                BorderStyleChanged?.Invoke(this, EventArgs.Empty);

                Invalidate();
            }
        }

        #endregion
    }
}
