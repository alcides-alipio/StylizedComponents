namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox_Beta
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
