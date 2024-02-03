using System;

namespace Fury.ECS
{
    internal readonly struct Component
    {
        public readonly Type Type;
        public readonly int Size;
    }
}
