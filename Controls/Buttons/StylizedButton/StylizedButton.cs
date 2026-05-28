using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [ToolboxItem(true)]
    [Designer(typeof(StylizedButtonDesigner))]
    public partial class StylizedButton : StylizedControl
    {
        public StylizedButton()
        {
            base.Size = new Size(180, 45);
            base.BackColor = Color.FromArgb(94, 148, 255);
            base.ForeColor = Color.White;
            base.Font = new Font("Segoe UI", 9);

            base.BorderThickness = 0;
            base.BorderColor = Color.Black;

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
    }
}
