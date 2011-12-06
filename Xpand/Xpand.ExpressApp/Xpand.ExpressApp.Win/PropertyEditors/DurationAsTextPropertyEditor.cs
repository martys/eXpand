using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors.Repository;
using Xpand.ExpressApp.Win.Editors;

namespace Xpand.ExpressApp.Win.PropertyEditors {

    [PropertyEditor(typeof(TimeSpan), false)]
    public class DurationAsTextPropertyEditor : DXPropertyEditor {
        public DurationAsTextPropertyEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model) {
        }

        protected override object CreateControlCore() {
            return new XpandDurationAsTextEdit();
        }

        protected override void SetupRepositoryItem(RepositoryItem item) {
            base.SetupRepositoryItem(item);
           
            Control.EditValueChanged += Control_EditValueChanged;
        }

        void Control_EditValueChanged(object sender, EventArgs e) {
            WriteValue();
            OnControlValueChanged();
        }

        protected override object GetControlValueCore()
        {
            return XpandDurationAsTextHelper.ConvertStringToTimeSpan(Control.Text);
        }

        protected override void ReadValueCore()
        {
            Control.EditValue = XpandDurationAsTextHelper.ConvertTimeSpanToReadableString((TimeSpan) PropertyValue);
        }
    }
}