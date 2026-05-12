using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        public class StylizedTextBoxDesigner : ControlDesigner
        {
            public override void InitializeNewComponent(IDictionary defaultValues)
            {
                base.InitializeNewComponent(defaultValues);

                var control = (StylizedTextBox)Control;
                control.Text = string.Empty;
            }

            public override DesignerActionListCollection ActionLists
            {
                get
                {
                    return new DesignerActionListCollection
                    {
                        new StylizedTextBoxActionList(Component)
                    };
                }
            }
        }

        public class StylizedTextBoxActionList : DesignerActionList
        {
            private StylizedTextBox _control;
            private DesignerActionUIService _service;

            public StylizedTextBoxActionList(IComponent component)
                : base(component)
            {
                _control = (StylizedTextBox)component;
                _service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                return new DesignerActionItemCollection
                {
                    new DesignerActionPropertyItem("Font", "Font", "Section1"),
                    new DesignerActionPropertyItem("Text", "Text", "Section1"),
                    new DesignerActionPropertyItem("PlaceholderText", "PlaceholderText", "Section1"),

                    new DesignerActionPropertyItem("BorderRadius", "BorderRadius", "Section2"),
                    new DesignerActionPropertyItem("BorderThickness", "BorderThickness", "Section2"),
                    new DesignerActionPropertyItem("BorderStyle", "BorderStyle", "Section2"),
                    new DesignerActionPropertyItem("BorderColor", "BorderColor", "Section2"),
                    new DesignerActionPropertyItem("HoverBorderColor", "HoverBorderColor", "Section2"),
                    new DesignerActionPropertyItem("ForeColor", "ForeColor", "Section2"),
                    new DesignerActionPropertyItem("BackColor", "BackColor", "Section2"),
                    new DesignerActionPropertyItem("PlaceholderColor", "PlaceholderColor", "Section2"),

                    new DesignerActionPropertyItem("ForeColor", "ForeColor", "Section4"),
                    new DesignerActionPropertyItem("BackColor", "BackColor", "Section4"),
                    new DesignerActionPropertyItem("PlaceholderColor", "PlaceholderColor", "Section4"),

                    new DesignerActionPropertyItem("UseSystemPasswordChar", "UseSystemPasswordChar", "Section3"),
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

            public string PlaceholderText
            {
                get => _control.PlaceholderText;
                set => SetProperty(nameof(_control.PlaceholderText), value);
            }

            #endregion

            #region Setion 2

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

            public Color HoverBorderColor
            {
                get => _control.HoverBorderColor;
                set => SetProperty(nameof(_control.HoverBorderColor), value);
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

            public Color PlaceholderColor
            {
                get => _control.PlaceholderColor;
                set => SetProperty(nameof(_control.PlaceholderColor), value);
            }

            #endregion

            #region Setion 3

            public bool UseSystemPasswordChar
            {
                get => _control.UseSystemPasswordChar;
                set => SetProperty(nameof(_control.UseSystemPasswordChar), value);
            }

            public bool AutoRoundedCorners
            {
                get => _control.AutoRoundedCorners;
                set => SetProperty(nameof(_control.AutoRoundedCorners), value);
            }

            #endregion
        }
    }
}
