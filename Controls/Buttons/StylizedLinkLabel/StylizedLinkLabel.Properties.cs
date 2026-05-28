using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedLinkLabel
    {
        private Color _hoverColorFilter = Color.Black;
        private float _hoverFilterStrength = 0.15f;
        private LinkBehavior _linkBehavior = LinkBehavior.SystemDefault;

        public LinkBehavior LinkBehavior
        {
            get => _linkBehavior;
            set => _linkBehavior = value;
        }

        #region Hover Properties

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

        #region Foreground Properties

        [DefaultValue(typeof(Font), "Segoe UI, 9pt")]
        public override Font Font
        {
            get => base.Font;
            set => base.Font = value;
        }

        [DefaultValue(typeof(Color), "Blue")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        #endregion

        #region Border Properties

        [DefaultValue(0)]
        public override int BorderThickness
        {
            get => base.BorderThickness;
            set => base.BorderThickness = value;
        }

        [DefaultValue(typeof(Color), "Black")]
        public override Color BorderColor
        {
            get => base.BorderColor;
            set => base.BorderColor = value;
        }

        #endregion

        #region Layout Properties

        [Browsable(true)]
        [Category("Layout")]
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
    }
}
