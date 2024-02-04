using System;

namespace Fury.ECS
{
    internal readonly struct Component
    {
        public readonly Type Type;
        public readonly int Id;
        public readonly int Size;
        public Component(Type type, int id, int size)
        {
            Type = type;
            Id = id;
            Size = size;
        }
    }
}
