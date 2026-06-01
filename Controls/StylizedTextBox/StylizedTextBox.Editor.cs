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
                    CreateItem("PlaceholderText", "Common Tasks"),

                    new DesignerActionHeaderItem("Appearance"),
                    CreateItem("ForeColor", "Appearance"),
                    CreateItem("PlaceholderColor", "Appearance"),
                    CreateItem("BackColor", "Appearance"),
                    CreateItem("FillColor", "Appearance"),
                    CreateItem("BorderColor", "Appearance"),
                    CreateItem("HoverBorderColor", "Appearance"),
                    CreateItem("CornerRadius", "Appearance"),
                    CreateItem("BorderThickness", "Appearance"),
                    CreateItem("BorderStyle", "Appearance"),

                    new DesignerActionHeaderItem("Behavior"),
                    CreateItem("UseSystemPasswordChar", "Behavior"),
                    CreateItem("AutoRoundedCorners", "Behavior"),
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

            public string PlaceholderText
            {
                get => _control.PlaceholderText;
                set => SetProperty(nameof(_control.PlaceholderText), value);
            }

            #endregion

            #region Appearance

            public Color ForeColor
            {
                get => _control.ForeColor;
                set => SetProperty(nameof(_control.ForeColor), value);
            }

            public Color PlaceholderColor
            {
                get => _control.PlaceholderColor;
                set => SetProperty(nameof(_control.PlaceholderColor), value);
            }

            public Color BackColor
            {
                get => _control.BackColor;
                set => SetProperty(nameof(_control.BackColor), value);
            }

            public Color FillColor
            {
                get => _control.FillColor;
                set => SetProperty(nameof(_control.FillColor), value);
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

            public int CornerRadius
            {
                get => _control._cornerRadius;
                set => SetProperty(nameof(_control._cornerRadius), value);
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
