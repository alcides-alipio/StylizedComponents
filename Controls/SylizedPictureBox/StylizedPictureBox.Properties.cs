using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox
    {
        private Image _image = null;
        private PictureBoxSizeMode _sizeMode = PictureBoxSizeMode.Normal;
        private bool _useTransparentBackground = false;

        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(null)]
        public Image Image
        {
            get => _image;
            set
            {
                _image = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue(typeof(PictureBoxSizeMode), "Normal")]
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

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [DefaultValue(false)]
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
