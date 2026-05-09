using System.ComponentModel;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public partial class StylizedControl : Control
    {
        public StylizedControl()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
        }
    }
}
