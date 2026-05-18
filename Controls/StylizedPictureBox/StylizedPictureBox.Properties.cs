namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox
    {
        private bool _useTransparentBackground = false;

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
