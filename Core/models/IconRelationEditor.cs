using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace StylizedComponents.Core.models
{
    internal class IconRelationEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(
            ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(
        ITypeDescriptorContext context,
        IServiceProvider provider,
        object value)
        {
            var editorService =
                (IWindowsFormsEditorService)
                provider.GetService(typeof(IWindowsFormsEditorService));

            if (editorService == null)
                return value;

            var picker = new IconRelationPicker
            {
                SelectedValue = (IconAlignment)value
            };

            picker.ValueSelected += (s, e) =>
            {
                editorService.CloseDropDown();
            };

            editorService.DropDownControl(picker);

            return picker.SelectedValue;
        }
    }
}
