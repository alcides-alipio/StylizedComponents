using System.ComponentModel;
using System.Drawing;

namespace StylizedComponents.Controls
{
    public partial class StylizedLabel
    {
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
