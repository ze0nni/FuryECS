using System;
using System.Reflection;

namespace Fury.ECS.Editor
{
    public partial class WorldGenerator
    {
        public class SystemInfo
        {
            public readonly String Name;
            public readonly String FullName;

            public readonly bool IsSetup;
            public readonly bool IsCleanup;
            public readonly bool IsUpdate;
            public readonly bool IsFixedUpdate;

            public SystemInfo(Type type)
            {
                if (!typeof(ECSSystem).IsAssignableFrom(type))
                    throw new ArgumentException($"System {type.FullName} must inheret from Fury.ECSSystem");
                if (!type.IsSealed)
                    throw new ArgumentException($"System {type.FullName} must be sealed");
                this.Name = type.Name;
                this.FullName = type.FullName;

                var flags = BindingFlags.Instance | BindingFlags.Public;
                IsSetup = type.GetMethod(nameof(ECSSystem.Setup), flags).DeclaringType != typeof(ECSSystem);
                IsCleanup = type.GetMethod(nameof(ECSSystem.Cleanup), flags).DeclaringType != typeof(ECSSystem);
                IsUpdate = type.GetMethod(nameof(ECSSystem.Update), flags).DeclaringType != typeof(ECSSystem);
                IsFixedUpdate = type.GetMethod(nameof(ECSSystem.FixedUpdate), flags).DeclaringType != typeof(ECSSystem);
            }
        }
    }
}
