using System;
using System.Reflection.Emit;

namespace Fury.ECS.Editor
{
    internal static class Helper
    {
        public static int SizeOf(Type type)
        {
            var dm = new DynamicMethod("SizeOfType", typeof(int), new Type[] { });
            ILGenerator il = dm.GetILGenerator();
            il.Emit(OpCodes.Sizeof, type);
            il.Emit(OpCodes.Ret);
            return (int)dm.Invoke(null, null);
        }
    }
}
