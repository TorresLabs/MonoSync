﻿using System;
using MonoSync.SyncTarget;

namespace MonoSync.Exceptions
{
    public class FieldSerializerNotFoundException : MonoSyncException
    {
        public FieldSerializerNotFoundException(Type type) : base(
            $"Could not find {nameof(ISyncTargetFactory)} for {type}")
        {
        }
    }
}