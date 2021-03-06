﻿using System;
using MonoSync.SyncSource;
using MonoSync.Utils;

namespace MonoSync.FieldSerializers
{
    public class SByteFieldSerializer : FieldSerializer<sbyte>
    {
        public override void Serialize(sbyte value, ExtendedBinaryWriter writer)
        {
            writer.Write(value);
        }

        public override void Deserialize(ExtendedBinaryReader reader, Action<sbyte> valueFixup)
        {
            valueFixup(reader.ReadSByte());
        }

        public override bool CanInterpolate => true;

        public override sbyte Interpolate(sbyte source, sbyte target, float factor)
        {
            return (sbyte) (source + (target - source) * factor);
        }
    }
}