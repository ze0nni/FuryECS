using System.Linq;

namespace Fury.ECS
{
    internal unsafe readonly struct Archetype
    {
        public readonly int Id;
        public readonly int Count;
        public readonly int[] Component;
        public readonly Components[] Buffers;

        internal Archetype(int id, (int id, int size)[] components)
        {
            Id = id;
            Count = components.Length;
            Component = components.Select(x => x.id).ToArray();
            Buffers = components.Select(x => new Components(x.size)).ToArray();
        }
    }
}
