using StylizedComponents.Core;
using System.ComponentModel;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    [DesignerCategory("Code")]
    [Designer(typeof(StylizedPictureBoxDesigner))]
    public partial class StylizedPictureBox : PictureBox
    {
        private TransparentBackgroundRenderer _backgroundRender;

        public StylizedPictureBox() : base()
        {
            _backgroundRender = new(this);
        }
    }
}
