﻿using System;
using MonoSync.Utils;

namespace MonoSync.FieldSerializers
{
    public class SingleFieldSerializer : FieldSerializer<float>
    {
        public override bool CanInterpolate => true;

        public override void Serialize(float value, ExtendedBinaryWriter writer)
        {
            writer.Write(value);
        }

        public override void Deserialize(ExtendedBinaryReader reader, Action<float> valueFixup)
        {
            valueFixup(reader.ReadSingle());
        }

        public override float Interpolate(float source, float target, float factor)
        {
            return source + (target - source) * factor;
        }
    }
}