using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox
    {
        public class StylizedPictureBoxDesigner : ControlDesigner
        {
            public override void InitializeNewComponent(IDictionary defaultValues)
            {
                base.InitializeNewComponent(defaultValues);

                var control = (StylizedPictureBox)Control;
                control.Text = string.Empty;
            }

            public override SelectionRules SelectionRules
            {
                get
                {
                    StylizedPictureBox control = (StylizedPictureBox)Control;

                    if (control.SizeMode == PictureBoxSizeMode.AutoSize)
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
                        new StylizedPictureBoxActionList(Component)
                    };
                }
            }
        }

        public class StylizedPictureBoxActionList : DesignerActionList
        {
            private StylizedPictureBox _control;
            private DesignerActionUIService _service;

            public StylizedPictureBoxActionList(IComponent component)
                : base(component)
            {
                _control = (StylizedPictureBox)component;
                _service = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
            }

            public override DesignerActionItemCollection GetSortedActionItems()
            {
                return new DesignerActionItemCollection
                {
                    new DesignerActionPropertyItem("Image", "Image", "Appearance"),
                    new DesignerActionPropertyItem("SizeMode", "SizeMode", "Appearance"),
                    new DesignerActionPropertyItem("UseTransparentBackground", "UseTransparentBackground", "Appearance"),
                };
            }

            private void SetProperty(string name, object value)
            {
                TypeDescriptor.GetProperties(_control)[name]
                    .SetValue(_control, value);

                _service?.Refresh(_control);
            }

            #region Appearance

            public Image Image
            {
                get => _control.Image;
                set => SetProperty(nameof(_control.Image), value);
            }

            public PictureBoxSizeMode SizeMode
            {
                get => _control.SizeMode;
                set => SetProperty(nameof(_control.SizeMode), value);
            }

            public bool UseTransparentBackground
            {
                get => _control.UseTransparentBackground;
                set => SetProperty(nameof(_control.UseTransparentBackground), value);
            }

            #endregion
        }
    }
}
