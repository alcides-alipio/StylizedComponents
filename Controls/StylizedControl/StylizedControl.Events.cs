using System;
using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedControl
    {
        public event EventHandler BorderRadiusChanged;
        public event EventHandler BorderThicknessChanged;
        public event EventHandler BorderColorChanged;
        public event EventHandler BorderStyleChanged;
        public event EventHandler AutoRoundedCornersChanged;

        #region Paint Events

        protected virtual void OnPaintContent(PaintEventArgs e) { }
        protected virtual void OnPaintBorder(PaintEventArgs e) { }

        #endregion

        #region Border Events

        protected virtual void OnBorderRadiusChanged(EventArgs e) { }
        protected virtual void OnBorderThicknessChanged(EventArgs e) { }
        protected virtual void OnBorderColorChanged(EventArgs e) { }
        protected virtual void OnBorderStyleChanged(EventArgs e) { }
        protected virtual void OnAutoRoundedCornersChanged(EventArgs e) { }

        #endregion
    }
}
