using System;
using System.Collections.Generic;

namespace Fury.ECS.Editor
{
    public partial class WorldGenerator
    {
        public class WorldInfo {
            public readonly String Namespace;
            public readonly String Name;

            public readonly List<ComponentInfo> Components = new List<ComponentInfo>();
            public readonly List<ArchetypeInfo> Archetypes = new List<ArchetypeInfo>();
            public readonly List<SystemInfo> Systems = new List<SystemInfo>();

            private readonly Dictionary<Type, ComponentInfo> _componentsMap = new Dictionary<Type, ComponentInfo>();

            public WorldInfo(Type type)
            {
                if (!typeof(World).IsAssignableFrom(type))
                    throw new ArgumentException("World must inheret from Fury.ECS.World");

                this.Namespace = type.Namespace;
                this.Name = type.Name;

                foreach (var nestedType in type.GetNestedTypes())
                {
                    foreach (var a in nestedType.GetCustomAttributesData()) {
                        if (a.AttributeType == typeof(World.ArchetypeAttribute)) {
                            Archetypes.Add(new ArchetypeInfo(this, nestedType));
                        }
                    }
                }

                foreach (var iType in type.GetInterfaces())
                {
                    if (iType.IsGenericType)
                    {
                        if (iType.GetGenericTypeDefinition() == typeof(World.WithSystem<>))
                        {
                            Systems.Add(new SystemInfo(iType.GetGenericArguments()[0]));
                        }
                    }
                }
            }

            internal ComponentInfo GetComponentInfo(Type type)
            {
                if (_componentsMap.TryGetValue(type, out var info))
                    return info;
                info = new ComponentInfo(type);
                _componentsMap.Add(type, info);
                Components.Add(info);
                return info;
            }
        }
    }
}
