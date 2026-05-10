using System.Windows.Forms;

namespace StylizedComponents.Controls
{
    public partial class StylizedControl
    {
        protected sealed override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            OnPaintContent(e);
            OnPaintBorder(e);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
    }
}
