using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox
    {
        private Image _image = null;
        private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.Normal;
        private bool _useTransparentBackground = false;

        public Image Image
        {
            get => _image;
            set
            {
                _image = value;
                Invalidate();
            }
        }

        public PictureBoxSizeMode SizeMode
        {
            get => _sizeMode;
            set
            {
                if (value == PictureBoxSizeMode.AutoSize)
                {
                    Width = _image.Width;
                    Height = _image.Height;
                }

                _sizeMode = value;
                Invalidate();
            }
        }

        public bool UseTransparentBackground
        {
            get => _useTransparentBackground;
            set
            {
                _useTransparentBackground = value;
                Invalidate();
            }
        }
    }
}
