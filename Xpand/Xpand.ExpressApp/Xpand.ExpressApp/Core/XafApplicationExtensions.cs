﻿using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace Xpand.ExpressApp.Core {
    public static class XafApplicationExtensions {
        public static T FindModule<T>(this XafApplication xafApplication) where T : ModuleBase {
            return (T)xafApplication.Modules.FindModule(typeof(T));
        }

        public static SimpleDataLayer CreateCachedDataLayer(this XafApplication xafApplication, IDataStore argsDataStore) {
            var cacheRoot = new DataCacheRoot(argsDataStore);
            var cacheNode = new DataCacheNode(cacheRoot);
            return new SimpleDataLayer(XafTypesInfo.XpoTypeInfoSource.XPDictionary, cacheNode);
        }

        public static void CreateCustomObjectSpaceprovider(this XafApplication xafApplication, CreateCustomObjectSpaceProviderEventArgs args) {
            var connectionString = getConnectionStringWithOutThreadSafeDataLayerInitialization(args);
            ((ISupportFullConnectionString)xafApplication).ConnectionString = connectionString;
            var connectionProvider = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema);
            IDataStore dataStore = ((IXafApplication)xafApplication).GetDataCacheRoot(connectionProvider);
            args.ObjectSpaceProvider = dataStore != null ? new XpandObjectSpaceProvider(new MultiDataStoreProvider(dataStore)) : new XpandObjectSpaceProvider(new MultiDataStoreProvider(connectionString));
        }

        static string getConnectionStringWithOutThreadSafeDataLayerInitialization(CreateCustomObjectSpaceProviderEventArgs args) {
            return args.Connection != null ? args.Connection.ConnectionString : args.ConnectionString;
        }
    }
}