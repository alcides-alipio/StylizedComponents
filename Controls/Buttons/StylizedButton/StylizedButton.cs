using StylizedComponents.Core;
using StylizedComponents.Core.builders;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [Designer(typeof(StylizedButtonDesigner))]
    [DesignerCategory("Code")]
    public partial class StylizedButton : Control
    {
        private readonly TransparentBackgroundRenderer _transparentBackgroundRenderer;

        public StylizedButton()
        {
            _transparentBackgroundRenderer = new TransparentBackgroundRenderer(this);

            base.ForeColor = Color.White;
            base.Font = new Font("Segoe UI", 9);
            Size = new Size(180, 45);

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

        private void UpdateRegion()
        {
            using (var path = RoundedPathBuilder.Create(
                Width, Height,
                _borderThickness, _cornerRadius,
                _autoRoundedCorners, 1
            ))
                Region = new Region(path);
        }
    }
}
