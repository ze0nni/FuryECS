using System.Runtime.CompilerServices;

namespace Fury.ECS
{
    public unsafe readonly struct Ref<T> where T : unmanaged
    {
        private readonly void* _ptr;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get()
#if NET_DOTS || UNITY_2020_1_OR_NEWER
            => ref Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(_ptr);
#else
            => ref System.Runtime.CompilerServices.Unsafe.AsRef<T>(_ptr);
#endif
    }
}