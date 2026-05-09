using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms.Design;

namespace StylizedComponents.Controls
{
    partial class StylizedTextBox
    {
        public class TextBoxStylizedDesigner : ControlDesigner
        {
            public override DesignerActionListCollection ActionLists
            {
                get
                {
                    return new DesignerActionListCollection
                {
                    new TextBoxStylizedActionList(Component)
                };
                }
            }
        }

        public class TextBoxStylizedActionList : DesignerActionList
        {
            private StylizedTextBox _control;
            private DesignerActionUIService _service;

            public TextBoxStylizedActionList(IComponent component)
                : base(component)
            {
                _control = (StylizedTextBox)component;
                _service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                return new DesignerActionItemCollection
                {
                    new DesignerActionPropertyItem("Font", "Font", "TextStyle"),
                    new DesignerActionPropertyItem("Text", "Text", "TextStyle"),
                    new DesignerActionPropertyItem("PlaceholderText", "PlaceholderText", "TextStyle"),
                    new DesignerActionPropertyItem("UseSystemPasswordChar", "UseSystemPasswordChar", "TextStyle"),

                    new DesignerActionPropertyItem("BorderRadius", "BorderRadius", "Details"),
                    new DesignerActionPropertyItem("BorderWidth", "BorderWidth", "Details"),
                    new DesignerActionPropertyItem("BorderColor", "BorderColor", "Details"),
                    new DesignerActionPropertyItem("ForeColor", "ForeColor", "Details"),
                    new DesignerActionPropertyItem("PlaceholderColor", "PlaceholderColor", "Details"),
                };
            }

            private void SetProperty(string name, object value)
            {
                TypeDescriptor.GetProperties(_control)[name]
                    .SetValue(_control, value);

                _service?.Refresh(_control);
            }

            public Font Font
            {
                get => _control.Font;
                set => SetProperty(nameof(_control.Font), value);
            }

            public bool UseSystemPasswordChar
            {
                get => _control.UseSystemPasswordChar;
                set => SetProperty(nameof(_control.UseSystemPasswordChar), value);
            }

            public string Text
            {
                get => _control.Text;
                set => SetProperty(nameof(_control.Text), value);
            }

            public Color ForeColor
            {
                get => _control.ForeColor;
                set => SetProperty(nameof(_control.ForeColor), value);
            }

            public string PlaceholderText
            {
                get => _control.PlaceholderText;
                set => SetProperty(nameof(_control.PlaceholderText), value);
            }

            public Color PlaceholderColor
            {
                get => _control.PlaceholderColor;
                set => SetProperty(nameof(_control.PlaceholderColor), value);
            }

            public int BorderRadius
            {
                get => _control.BorderRadius;
                set => SetProperty(nameof(_control.BorderRadius), value);
            }

            public float BorderWidth
            {
                get => _control.BorderWidth;
                set => SetProperty(nameof(_control.BorderWidth), value);
            }

            public Color BorderColor
            {
                get => _control.BorderColor;
                set => SetProperty(nameof(_control.BorderColor), value);
            }
        }
    }
}
