using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StylizedComponents.Controls
{
    public partial class StylizedButton
    {
        private Color _hoverColorFilter = Color.Black;
        private float _hoverFilterStrength = 0.15f;
        private bool _autoRoundedCorners = false;

        #region Text Properties

        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        public override Font Font { get => base.Font; set => base.Font = value; }

        [DefaultValue(typeof(Color), "White")]
        public override Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

        #endregion

        #region Background Properties

        [DefaultValue(typeof(Color), "94, 148, 255")]
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

        #endregion

        #region Border Properties

        [DefaultValue(0)]
        public override int BorderThickness { get => base.BorderThickness; set => base.BorderThickness = value; }

        [DefaultValue(typeof(Color), "Black")]
        public override Color BorderColor { get => base.BorderColor; set => base.BorderColor = value; }

        [Category("Appearance")]
        [Description("Automatically makes corners fully rounded.")]
        [DefaultValue(false)]
        public bool AutoRoundedCorners
        {
            get => _autoRoundedCorners;
            set
            {
                _autoRoundedCorners = value;
                Invalidate();
            }
        }

        #endregion

        #region Outhers Properties

        [Category("Appearance")]
        [Description("Color filter on mouse hover.")]
        [DefaultValue(typeof(Color), "Black")]
        public Color HoverColorFilter
        {
            get => _hoverColorFilter;
            set => _hoverColorFilter = value;
        }

        [Category("Appearance")]
        [Description("Strength of filter on mouse hover.")]
        [DefaultValue(0.15f)]
        public float HoverFilterStrength
        {
            get => _hoverFilterStrength;
            set => _hoverFilterStrength = value;
        }

        #endregion
    }
}
