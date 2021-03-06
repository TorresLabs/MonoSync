﻿using System;
using MonoSync.SyncSource;
using MonoSync.SyncTarget.SyncTargetObjects;
using MonoSync.Utils;

namespace MonoSync.SyncTarget.SyncTargetFactories
{
    public class StringSyncTargetFactory : ISyncTargetFactory
    {
        public bool CanCreate(Type baseType)
        {
            return typeof(string) == baseType;
        }

        public SyncTarget Create(int referenceId, Type baseType, ExtendedBinaryReader reader,
            IFieldSerializerResolver fieldSerializerResolver, SyncTargetRoot root)
        {
            return new StringSyncTarget(referenceId, reader);
        }
    }
}