using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace StylizedComponents.Controls
{
    public partial class StylizedLabel
    {
        public class StylizedLabelDesigner : ControlDesigner
        {
            public override SelectionRules SelectionRules
            {
                get
                {
                    StylizedLabel control = (StylizedLabel)Control;

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
                        new StylizedLabelActionList(Component)
                    };
                }
            }
        }

        public class StylizedLabelActionList : DesignerActionList
        {
            private StylizedLabel _control;
            private DesignerActionUIService _service;

            public StylizedLabelActionList(IComponent component)
                : base(component)
            {
                _control = (StylizedLabel)component;
                _service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                return new DesignerActionItemCollection
                {
                    new DesignerActionPropertyItem("Font", "Font", "Section 1"),
                    new DesignerActionPropertyItem("Text", "Text", "Section 1"),

                    new DesignerActionPropertyItem("ForeColor", "ForeColor", "Section2"),
                    new DesignerActionPropertyItem("BackColor", "BackColor", "Section2"),
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

            public Color BackColor
            {
                get => _control.BackColor;
                set => SetProperty(nameof(_control.BackColor), value);
            }

            #endregion
        }
    }
}
