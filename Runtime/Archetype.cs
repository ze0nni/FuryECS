using System;
using System.Linq;

namespace Fury.ECS
{
    internal unsafe readonly struct Archetype
    {
        public readonly Type Type;
        public readonly int Id;
        public readonly int ComponentsCount;
        public readonly int[] Components;
        public readonly Components[] Buffers;

        public Archetype(Type type, int id, Component[] components)
        {
            Type = type;
            Id = id;
            ComponentsCount = components.Length;
            Components = components.Select(x => x.Id).ToArray();
            Buffers = components.Select(x => new Components(x.Size)).ToArray();
        }
    }
}
