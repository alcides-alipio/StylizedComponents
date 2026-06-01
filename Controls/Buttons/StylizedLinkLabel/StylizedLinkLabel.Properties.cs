using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedLinkLabel
    {
        private Color _hoverColorFilter = Color.Black;
        private float _hoverFilterStrength = 0.2f;

        private LinkBehavior _linkBehavior = LinkBehavior.SystemDefault;

        public LinkBehavior LinkBehavior
        {
            get => _linkBehavior;
            set => _linkBehavior = value;
        }

        

        #region Hover Properties

        [Category("StylizedComponents Properties")]
        [Description("Sets the hover filter color for this control.")]
        [DefaultValue(typeof(Color), "Black")]
        public Color HoverColorFilter
        {
            get => _hoverColorFilter;
            set => _hoverColorFilter = value;
        }

        [Category("StylizedComponents Properties")]
        [Description("Sets the Hover filter strength for this control.")]
        [DefaultValue(0.2f)]
        public float HoverFilterStrength
        {
            get => _hoverFilterStrength;
            set => _hoverFilterStrength = value;
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

        [DefaultValue(typeof(Color), "Blue")]
        [Description("Sets the text color for this control.")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        #endregion

        #endregion
    }
}
