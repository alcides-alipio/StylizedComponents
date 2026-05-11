using System.ComponentModel;
using System.Drawing;

namespace StylizedComponents.Controls
{
    [ToolboxItem(true)]
    [Designer(typeof(StylizedPictureBoxDesigner))]
    public partial class StylizedPictureBox : StylizedControl
    {
        public StylizedPictureBox()
        {
            base.Size = new Size(100, 50);
        }
    }
}
