using StylizedComponents.Core;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [Designer(typeof(StylizedLinkLabelDesigner))]
    [DesignerCategory("Code")]
    public partial class StylizedLinkLabel : Control
    {
        private readonly TransparentBackgroundRenderer _transparentBackgroundRenderer;

        public StylizedLinkLabel()
        {
            _transparentBackgroundRenderer = new TransparentBackgroundRenderer(this);

            base.Font = new Font("Segoe UI", 9);
            base.ForeColor = Color.Blue;
            base.AutoSize = true;

            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.SupportsTransparentBackColor,
                true);
            UpdateStyles();

            RegisterHoverEvents(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnregisterHoverEvents(this);
            }

            base.Dispose(disposing);
        }

        private void AjustSize(bool isInitialization)
        {
            if (AutoSize || isInitialization)
                Size = GetPreferredSize(Size.Empty);
        }
    }
}
