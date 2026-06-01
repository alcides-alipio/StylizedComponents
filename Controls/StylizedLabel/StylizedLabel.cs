using StylizedComponents.Core;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [Designer(typeof(StylizedLabelDesigner))]
    [DesignerCategory("Code")]
    public partial class StylizedLabel : Control
    {
        private TransparentBackgroundRenderer _transparentBackgroundRenderer;

        public StylizedLabel()
        {
            _transparentBackgroundRenderer = new TransparentBackgroundRenderer(this);
            
            base.Font = new Font("Segoe UI", 9);
            base.AutoSize = true;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);
            UpdateStyles();
        }

        private void AjustSize(bool isInitialization)
        {
            if (AutoSize || isInitialization)
                Size = GetPreferredSize(Size.Empty);
        }
    }
}
