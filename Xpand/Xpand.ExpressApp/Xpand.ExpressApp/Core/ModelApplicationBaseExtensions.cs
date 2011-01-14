﻿using System;
using DevExpress.ExpressApp.Model.Core;

namespace Xpand.ExpressApp.Core {
    public static class ModelApplicationBaseExtensions {
        public static void AddLayerBeforeLast(this ModelApplicationBase application, ModelApplicationBase layer) {
            ModelApplicationBase lastLayer = application.LastLayer;
            if (lastLayer.Id != "After Setup" && lastLayer.Id != "UserDiff")
                throw new ArgumentException("LastLayer.Id", lastLayer.Id);
            application.RemoveLayer(lastLayer);
            application.AddLayer(layer);
            application.AddLayer(lastLayer);
        }
        
        public static bool HasAspect(this ModelApplicationBase modelApplicationBase, string aspectName) {
            for (int i = 0; i < modelApplicationBase.AspectCount; i++) {
                if (modelApplicationBase.GetAspect(i)==aspectName)
                    return true;
            }
            return false;
        }
    }
}