using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox
    {
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (_sizeMode != PictureBoxSizeMode.AutoSize)
            {
                base.SetBoundsCore(x, y, width, height, specified);
                return;
            }

            base.SetBoundsCore(x, y, _image.Width, _image.Height, specified);
        }
    }
}
