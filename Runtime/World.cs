using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("FuryECS.Editor")]

namespace Fury.ECS
{
    public abstract class World
    {
        [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
        internal protected sealed class ArchetypeAttribute : Attribute { }
        internal protected interface WithComponent<TComponent> where TComponent : struct { }
        public interface WithSystem<TSystem> where TSystem : ECSSystem, new() { }

        protected Entities<T> CreateEntities<T>() where T : struct
        {
            return null;
        }

        public virtual void Setup()
        {

        }

        public virtual void Cleanup()
        {

        }

        public virtual void Update(float a)
        {

        }

        public virtual void FixedUpdate(float a)
        {

        }
    }
}
