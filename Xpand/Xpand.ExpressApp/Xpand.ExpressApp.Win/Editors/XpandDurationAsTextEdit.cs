using System;
using System.ComponentModel;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Utils;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors;

namespace Xpand.ExpressApp.Win.Editors
{

    //The attribute that points to the registration method 
    [ToolboxItem(true)]
    public class XpandDurationAsTextEdit : StringEdit
    {
        //The static constructor which calls the registration method 
        static XpandDurationAsTextEdit() { XpandRepositoryItemTimeSpanAsTextEdit.RegisterCustomEdit(); }

        //Initialize the new instance 
        public XpandDurationAsTextEdit()
        {
            //... 

        }

        [Browsable(false), Bindable(false)]
        public override string Text { get { return base.Text; } set { } }

        [Category(CategoryName.Appearance), Description("Gets or sets the currently edited time value."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public TimeSpan Time
        {
            get { return XpandDurationAsTextHelper.ConvertStringToTimeSpan(EditValue.ToString()); }
            set { EditValue = XpandDurationAsTextHelper.ConvertTimeSpanToReadableString(value); }
        }
        
        protected override void OnCreateControl()
        {
            ShowToolTips = true;
            ToolTip = XpandDurationAsTextHelper.Hint;

            Properties.Mask.MaskType = MaskType.RegEx;
            Properties.Mask.EditMask = XpandDurationAsTextHelper.Mask;

            base.OnCreateControl();
        }


        protected override void OnLostFocus(EventArgs e)
        {
            UpdateDisplayText();
            base.OnLostFocus(e);
        }
        
        //Return the unique name 
        public override string EditorTypeName
        {
            get
            {
                return
                    XpandRepositoryItemTimeSpanAsTextEdit.CustomEditName;
            }
        }

        //Override the Properties property 
        //Simply type-cast the object to the custom repository item type 
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new XpandRepositoryItemTimeSpanAsTextEdit Properties
        {
            get { return base.Properties as XpandRepositoryItemTimeSpanAsTextEdit; }
        }


    }

    [UserRepositoryItem("RegisterCustomEdit")]
    public class XpandRepositoryItemTimeSpanAsTextEdit : RepositoryItemStringEdit
    {

        //The static constructor which calls the registration method 
        static XpandRepositoryItemTimeSpanAsTextEdit() { RegisterCustomEdit(); }


        //The unique name for the custom editor 
        public const string CustomEditName = "XpandDurationAsTextEdit";

        //Return the unique name 
        public override string EditorTypeName { get { return CustomEditName; } }

        //Register the editor 
        public static void RegisterCustomEdit()
        {

            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName,
              typeof(XpandDurationAsTextEdit), typeof(XpandRepositoryItemTimeSpanAsTextEdit),
              typeof(TextEditViewInfo), new TextEditPainter(), true));
        }


        //Override the Assign method 
        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                var source = item as XpandRepositoryItemTimeSpanAsTextEdit;
                if (source == null) return;
            }
            finally
            {
                EndUpdate();
            }
        }

        protected virtual void UpdateFormats()
        {
            Mask.MaskType = MaskType.RegEx;
            Mask.EditMask = XpandDurationAsTextHelper.Mask;
        }

        public override string GetDisplayText(FormatInfo format, object editValue)
        {
            if (editValue is TimeSpan)
                return XpandDurationAsTextHelper.ConvertTimeSpanToReadableString(((TimeSpan)editValue));
            if (editValue is string)
            {
                return XpandDurationAsTextHelper.ConvertTimeSpanToReadableString(XpandDurationAsTextHelper.ConvertStringToTimeSpan(editValue.ToString()));
                //return editValue.ToString();
            }

            return GetDisplayText(null, new TimeSpan(0));
        }

        //protected override DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs DoParseEditValue(object val)
        //{
        //    if (val is TimeSpan)
        //        return new DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs(DurationAsTextHelper.ConvertTimeSpanToReadableString(((TimeSpan)val)));
        //    if (val is string)
        //    {
        //        return new DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs(DurationAsTextHelper.ConvertTimeSpanToReadableString(DurationAsTextHelper.ConvertStringToTimeSpan(val.ToString())));
        //    }

        //    return base.DoParseEditValue(val);
        //}
    }
}
