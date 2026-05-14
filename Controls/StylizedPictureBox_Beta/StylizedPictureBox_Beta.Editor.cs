using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace StylizedComponents.Controls
{
    public partial class StylizedPictureBox_Beta
    {
        public class StylizedPictureBoxDesigner_Beta : ControlDesigner
        {
            public override SelectionRules SelectionRules
            {
                get
                {
                    StylizedPictureBox_Beta control = (StylizedPictureBox_Beta)Control;

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
                        new StylizedPictureBoxActionList_Beta(Component)
                    };
                }
            }
        }

        public class StylizedPictureBoxActionList_Beta : DesignerActionList
        {
            private StylizedPictureBox_Beta _control;
            private DesignerActionUIService _service;

            public StylizedPictureBoxActionList_Beta(IComponent component)
                : base(component)
            {
                _control = (StylizedPictureBox_Beta)component;
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
