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
                DesignerActionPropertyItem CreateItem(string propertyName, string category)
                {
                    var prop = TypeDescriptor.GetProperties(_control)[propertyName];

                    return new DesignerActionPropertyItem(
                        prop.Name,
                        prop.Name,
                        category,
                        prop.Description
                    );
                }

                return new DesignerActionItemCollection
                {
                    new DesignerActionHeaderItem("Common Tasks"),
                    CreateItem("Font", "Common Tasks"),
                    CreateItem("Text", "Common Tasks"),
                    CreateItem("TextAlign", "Common Tasks"),

                    new DesignerActionHeaderItem("Appearance"),
                    CreateItem("ForeColor", "Appearance"),
                    CreateItem("FillColor", "Appearance"),
                    CreateItem("BackColor", "Appearance"),
                    CreateItem("BorderColor", "Appearance"),
                    CreateItem("CornerRadius", "Appearance"),
                    CreateItem("BorderThickness", "Appearance"),
                    CreateItem("BorderStyle", "Appearance"),

                    new DesignerActionHeaderItem("Behavior"),
                    CreateItem("AutoRoundedCorners", "Behavior"),
                    CreateItem("AutoSize", "Behavior"),
                };
            }

            private void SetProperty(string name, object value)
            {
                TypeDescriptor.GetProperties(_control)[name]
                    .SetValue(_control, value);

                _service?.Refresh(_control);
            }

            #region Common Tasks

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

            public ContentAlignment TextAlign
            {
                get => _control.TextAlign;
                set => SetProperty(nameof(_control.TextAlign), value);
            }

            #endregion

            #region Appearance

            public Color ForeColor
            {
                get => _control.ForeColor;
                set => SetProperty(nameof(_control.ForeColor), value);
            }

            public Color FillColor
            {
                get => _control.FillColor;
                set => SetProperty(nameof(_control.FillColor), value);
            }

            public Color BackColor
            {
                get => _control.BackColor;
                set => SetProperty(nameof(_control.BackColor), value);
            }

            public Color BorderColor
            {
                get => _control.BorderColor;
                set => SetProperty(nameof(_control.BorderColor), value);
            }

            public int CornerRadius
            {
                get => _control.CornerRadius;
                set => SetProperty(nameof(_control.CornerRadius), value);
            }
            public int BorderThickness
            {
                get => _control.BorderThickness;
                set => SetProperty(nameof(_control.BorderThickness), value);
            }

            public DashStyle BorderStyle
            {
                get => _control.BorderStyle;
                set => SetProperty(nameof(_control.BorderStyle), value);
            }

            #endregion

            #region Behavior

            public bool AutoRoundedCorners
            {
                get => _control.AutoRoundedCorners;
                set => SetProperty(nameof(_control.AutoRoundedCorners), value);
            }

            public bool AutoSize
            {
                get => _control.AutoSize;
                set => SetProperty(nameof(_control.AutoSize), value);
            }

            #endregion
        }
    }
}
