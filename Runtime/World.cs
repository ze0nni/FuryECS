using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("FuryECS.Editor")]

namespace Fury.ECS
{
    public abstract class World
    {
        [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
        internal protected sealed class ArchetypeAttribute : Attribute { }
        internal protected interface WithComponent<T> where T : struct { }

        protected Entities<T> CreateEntities<T>() where T : struct
        {
            return null;
        }
    }
}
