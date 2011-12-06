using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.DC;
using Xpand.ExpressApp.Attributes;

namespace FeatureCenter.Module.Win.PropertyEditors.DurationAsTextEditor {
    public class AttributeRegistrator : Xpand.ExpressApp.Core.AttributeRegistrator {
        public override IEnumerable<Attribute> GetAttributes(ITypeInfo typesInfo) {
            if (!Object.Equals(typesInfo.Type, typeof(DurationAsTextTestObject))) yield break;
            //yield return new AdditionalViewControlsRuleAttribute(Module.Captions.ViewMessage + " " + Captions.HeaderStringPropertyEditors, "1=1", "1=1",
            //    Captions.ViewMessageStringPropertyEditors, Position.Bottom) { View = "StringPropertyEditors_DetailView" };
            //yield return new AdditionalViewControlsRuleAttribute(Module.Captions.ViewMessage + " " + Captions.HeaderStringPropertyEditors + "1", "1=1", "1=1",
            //    Captions.ViewMessageStringPropertyEditors1, Position.Bottom) { View = "StringPropertyEditors_DetailView" };
            //yield return new AdditionalViewControlsRuleAttribute(Module.Captions.Header + " " + Captions.HeaderStringPropertyEditors, "1=1", "1=1",
            //    Captions.HeaderStringPropertyEditors, Position.Top) { View = "StringPropertyEditors_DetailView" };
            yield return new CloneViewAttribute(CloneViewType.DetailView, "DurationPropertyEditors_DetailView");
            var xpandNavigationItemAttribute = new XpandNavigationItemAttribute(Module.Captions.PropertyEditors + "Duration editors", "DurationPropertyEditors_DetailView");
            yield return xpandNavigationItemAttribute;
            //yield return new DisplayFeatureModelAttribute("DurationPropertyEditors_DetailView");
            yield return new WhatsNewAttribute(new DateTime(2012, 3, 01), xpandNavigationItemAttribute);
        }
    }
}
