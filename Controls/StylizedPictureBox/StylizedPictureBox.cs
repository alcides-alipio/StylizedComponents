using System.ComponentModel;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [DesignerCategory("Code")]
    [Designer(typeof(StylizedPictureBoxDesigner))]
    public partial class StylizedPictureBox : PictureBox
    {
        public StylizedPictureBox() : base() { }
    }
}
