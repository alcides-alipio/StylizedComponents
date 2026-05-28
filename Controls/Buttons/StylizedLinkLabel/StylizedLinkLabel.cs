using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [ToolboxItem(true)]
    [Designer(typeof(StylizedLinkLabelDesigner))]
    public partial class StylizedLinkLabel : StylizedControl
    {
        public StylizedLinkLabel()
        {
            base.Font = new Font("Segoe UI", 9);
            base.ForeColor = Color.Blue;
            base.AutoSize = true;

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
