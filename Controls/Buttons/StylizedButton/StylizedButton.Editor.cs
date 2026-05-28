using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;

namespace StylizedComponents.Controls
{
    public partial class StylizedButton
    {
        public class StylizedButtonDesigner : ControlDesigner
        {
            public override void InitializeNewComponent(IDictionary defaultValues)
            {
                base.InitializeNewComponent(defaultValues);

                var control = (StylizedButton)Control;
            }

            public override DesignerActionListCollection ActionLists
            {
                get
                {
                    return new DesignerActionListCollection
                    {
                        new StylizedButtonActionList(Component)
                    };
                }
            }
        }

        public class StylizedButtonActionList : DesignerActionList
        {
            private StylizedButton _control;
            private DesignerActionUIService _service;

            public StylizedButtonActionList(IComponent component)
                : base(component)
            {
                _control = (StylizedButton)component;
                _service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                return new DesignerActionItemCollection
                {
                    new DesignerActionPropertyItem("Font", "Font", "Section 1"),
                    new DesignerActionPropertyItem("Text", "Text", "Section 1"),

                    new DesignerActionPropertyItem("BorderRadius", "BorderRadius", "Section2"),
                    new DesignerActionPropertyItem("BorderThickness", "BorderThickness", "Section2"),
                    new DesignerActionPropertyItem("BorderStyle", "BorderStyle", "Section2"),
                    new DesignerActionPropertyItem("BorderColor", "BorderColor", "Section2"),
                    new DesignerActionPropertyItem("ForeColor", "ForeColor", "Section2"),
                    new DesignerActionPropertyItem("BackColor", "BackColor", "Section2"),
                    new DesignerActionPropertyItem("HoverColorFilter", "HoverColorFilter", "Section2"),
                    new DesignerActionPropertyItem("HoverFilterStrength", "HoverFilterStrength", "Section2"),

                    new DesignerActionPropertyItem("AutoRoundedCorners", "AutoRoundedCorners", "Section3"),
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

            public int BorderRadius
            {
                get => _control.BorderRadius;
                set => SetProperty(nameof(_control.BorderRadius), value);
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

            public Color BorderColor
            {
                get => _control.BorderColor;
                set => SetProperty(nameof(_control.BorderColor), value);
            }

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

            #region Section 3
            public bool AutoRoundedCorners
            {
                get => _control.AutoRoundedCorners;
                set => SetProperty(nameof(_control.AutoRoundedCorners), value);
            }

            #endregion
        }
    }
}
