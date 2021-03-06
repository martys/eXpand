﻿using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using Xpand.ExpressApp.FilterDataStore.Model;

namespace Xpand.ExpressApp.FilterDataStore {
    [ToolboxItem(false)]
    public sealed partial class FilterDataStoreModule : XpandModuleBase {
        public FilterDataStoreModule() {
            InitializeComponent();
        }
        public override void ExtendModelInterfaces(ModelInterfaceExtenders extenders) {
            base.ExtendModelInterfaces(extenders);
            extenders.Add<IModelClass, IModelClassDisabledDataStoreFilters>();
            extenders.Add<IModelApplication, IModelApplicationFilterDataStore>();
        }
    }
}