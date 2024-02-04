using System.Runtime.CompilerServices;

namespace Fury.ECS
{
    internal static class Helper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe ref T ToRef<T>(void* ptr) where T : unmanaged
#if NET_DOTS || UNITY_2020_1_OR_NEWER
            => ref Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AsRef<T>(ptr);
#else
            => ref System.Runtime.CompilerServices.Unsafe.AsRef<T>(ptr);
#endif
    }
}
