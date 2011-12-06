using System;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Xpand.ExpressApp.Win.Editors;

namespace FeatureCenter.Module.Win.PropertyEditors.DurationAsTextEditor {
    public class DurationAsTextTestObject : BaseObject {
        public DurationAsTextTestObject(Session session)
            : base(session) {
        }

        private TimeSpan _Duration;

        [DisplayName("Duration"), ImmediatePostData(true)]
        public TimeSpan Duration {
            get { return _Duration; }
            set { SetPropertyValue("Duration", ref _Duration, value); }
        }



        [DisplayName("DurationAsText"), ImmediatePostData(true)]
        public string DurationAsText {
            get { return XpandDurationAsTextHelper.ConvertTimeSpanToReadableString(_Duration); }

        }


        [DisplayName("DurationAsTimeSpan"), ImmediatePostData(true)]
        public TimeSpan DurationAsTimeSpan {
            get { return _Duration; }

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue) {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == "Duration") {
                OnChanged("DurationAsText");
                OnChanged("DurationAsTimeSpan");
            }
        }
    }
}