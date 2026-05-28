using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace StylizedComponents.Controls
{
    public partial class StylizedLinkLabel
    {
        public class StylizedLinkLabelDesigner : ControlDesigner
        {
            public override SelectionRules SelectionRules
            {
                get
                {
                    StylizedLinkLabel control = (StylizedLinkLabel)Control;

                    if (control.AutoSize)
                    {
                        return SelectionRules.Moveable |
                               SelectionRules.Visible;
                    }

                    return base.SelectionRules;
                }
            }

            public override DesignerActionListCollection ActionLists
            {
                get
                {
                    return new DesignerActionListCollection
                    {
                        new StylizedLinkLabelActionList(Component)
                    };
                }
            }
        }

        public class StylizedLinkLabelActionList : DesignerActionList
        {
            private StylizedLinkLabel _control;
            private DesignerActionUIService _service;

            public StylizedLinkLabelActionList(IComponent component)
                : base(component)
            {
                _control = (StylizedLinkLabel)component;
                _service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                return new DesignerActionItemCollection
                {
                    new DesignerActionPropertyItem("Font", "Font", "Section 1"),
                    new DesignerActionPropertyItem("Text", "Text", "Section 1"),

                    new DesignerActionPropertyItem("ForeColor", "ForeColor", "Section2"),
                    new DesignerActionPropertyItem("LinkBehavior", "LinkBehavior", "Section2"),
                    new DesignerActionPropertyItem("HoverColorFilter", "HoverColorFilter", "Section2"),
                    new DesignerActionPropertyItem("HoverFilterStrength", "HoverFilterStrength", "Section2")
                };
            }

            private void SetProperty(string name, object value)
            {
                TypeDescriptor.GetProperties(_control)[name]
                    .SetValue(_control, value);

                _service?.Refresh(_control);
            }

            #region Section 1

            public Font Font
            {
                get => _control.Font;
                set => SetProperty(nameof(_control.Font), value);
            }

            public string Text
            {
                get => _control.Text;
                set => SetProperty(nameof(_control.Text), value);
            }

            #endregion

            #region Section 2

            public Color ForeColor
            {
                get => _control.ForeColor;
                set => SetProperty(nameof(_control.ForeColor), value);
            }

            public LinkBehavior LinkBehavior
            {
                get => _control.LinkBehavior;
                set => SetProperty(nameof(_control.LinkBehavior), value);
            }

            public Color HoverColorFilter
            {
                get => _control.HoverColorFilter;
                set => SetProperty(nameof(_control.HoverColorFilter), value);
            }

            public float HoverFilterStrength
            {
                get => _control.HoverFilterStrength;
                set => SetProperty(nameof(_control.HoverFilterStrength), value);
            }

            #endregion
        }
    }
}
