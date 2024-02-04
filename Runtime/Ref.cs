using System.Runtime.CompilerServices;

namespace Fury.ECS
{
    public unsafe readonly struct Ref<T> where T : unmanaged
    {
        private readonly void* _ptr;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get() => ref Helper.ToRef<T>(_ptr);
    }
}