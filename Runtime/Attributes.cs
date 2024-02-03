using System;

namespace Fury.ECS
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =false)]
    public sealed class WorldAttribute : Attribute
    {
        public string OutputDirectory;
    }
}
