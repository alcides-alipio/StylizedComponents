using System.ComponentModel;
using System.Drawing.Design;

namespace StylizedComponents.Core.models
{
    [Editor(typeof(IconRelationEditor), typeof(UITypeEditor))]
    public enum IconAlignment
    {
        Left,
        Right,
        Top,
        Bottom
    }
}
