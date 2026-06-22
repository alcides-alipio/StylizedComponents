using System;

namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox
    {
        protected override void OnLocationChanged(EventArgs e)
        {
            _backgroundCache?.Dispose();
            _backgroundCache = null;
            base.OnLocationChanged(e);
            Invalidate();
        }
    }
}
