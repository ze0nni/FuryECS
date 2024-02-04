using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fury.ECS.Editor
{
    public partial class WorldGenerator
    {
        public class SystemInfo
        {
            public readonly String Name;
            public readonly String FullName;

            public List<(Type RunType, Type ArgType)> Runners = new List<(Type, Type)>();

            public SystemInfo(Type type)
            {
                if (!typeof(EcsSystem).IsAssignableFrom(type))
                    throw new ArgumentException($"System {type.FullName} must inheret from Fury.ECSSystem");
                if (!type.IsSealed)
                    throw new ArgumentException($"System {type.FullName} must be sealed");
                this.Name = type.Name;
                this.FullName = type.FullName;

                foreach (var iType in type.GetInterfaces().Reverse())
                {
                    if (iType.IsGenericType)
                    {
                        if (iType.GetGenericTypeDefinition() == typeof(EcsSystem.On<>))
                        {
                            var args = iType.GetGenericArguments();
                            Runners.Add((args[0], null));
                        }
                        if (iType.GetGenericTypeDefinition() == typeof(EcsSystem.On<,>))
                        {
                            var args = iType.GetGenericArguments();
                            Runners.Add((args[0], args[1]));
                        }
                    }
                }
            }
        }
    }
}
