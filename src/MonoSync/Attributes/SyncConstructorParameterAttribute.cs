﻿using System;

namespace MonoSync.Attributes
{
    /// <summary>
    ///     <see cref="SyncConstructorParameterAttribute" /> is used when the name cannot be resolved implicitly
    ///     by capitalizing the first letter of the parameter.
    /// </summary>
    /// <remarks><see cref="PropertyName" /> must be a public property with the <see cref="SyncAttribute" /> attribute</remarks>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class SyncConstructorParameterAttribute : Attribute
    {
        public SyncConstructorParameterAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}