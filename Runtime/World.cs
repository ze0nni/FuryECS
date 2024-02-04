using System;
using System.Linq;
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

        internal readonly Component[] Components;
        internal readonly Archetype[] Archetypes;

        protected World()
        {
            var componentsCount = 0;
            Components = GetComponents().Select(x => new Component(
                x.Item1, 
                componentsCount++, 
                x.Item2))
                .ToArray();
            var archetypesCount = 0;
            Archetypes = GetArchetypes().Select(x => new Archetype(
                x.Item1, 
                archetypesCount++, 
                x.Item2.Select(i => Components[i]).ToArray()))
                .ToArray();
        }

        protected virtual (Type, int)[] GetComponents() => throw new NotImplementedException();
        protected virtual (Type, int[])[] GetArchetypes() => throw new NotImplementedException();
        
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
