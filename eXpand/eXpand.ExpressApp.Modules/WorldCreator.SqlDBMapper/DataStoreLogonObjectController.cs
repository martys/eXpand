﻿using DevExpress.ExpressApp;
using DevExpress.Xpo.DB;
using eXpand.Persistent.Base.SqlDBMapper;
using eXpand.ExpressApp.Core;
using eXpand.Utils.Helpers;

namespace eXpand.ExpressApp.WorldCreator.SqlDBMapper
{
    public class DataStoreLogonObjectController:ViewController<DetailView>
    {
        public DataStoreLogonObjectController() {
            TargetObjectType = typeof (IDataStoreLogonObject);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.ObjectChanged+=ObjectSpaceOnObjectChanged;
        }

        void ObjectSpaceOnObjectChanged(object sender, ObjectChangedEventArgs objectChangedEventArgs) {
            var dataStoreLogonObject = ((IDataStoreLogonObject)View.CurrentObject);
            if (objectChangedEventArgs.PropertyName==dataStoreLogonObject.GetPropertyName(o => o.ServerName)) {
                dataStoreLogonObject.DataBases.Clear();
                var msSqlProviderFactory = new MSSqlProviderFactory();
                string[] databaseNames = msSqlProviderFactory.GetDatabases(dataStoreLogonObject.ServerName, dataStoreLogonObject.UserName, dataStoreLogonObject.PassWord);
                foreach (string databaseName in databaseNames){
                    var dataBase = ObjectSpace.CreateObjectFromInterface<IDataBase>();
                    dataBase.Name = databaseName;                
                    dataStoreLogonObject.DataBases.Add(dataBase);
                }
            }
        }
    }
}
