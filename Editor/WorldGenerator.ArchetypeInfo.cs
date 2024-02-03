using System;
using System.Collections.Generic;
using System.Linq;

namespace Fury.ECS.Editor
{
    public partial class WorldGenerator
    {
        public class ArchetypeInfo
        {
            public readonly String Name;
            public readonly List<ComponentInfo> Components = new List<ComponentInfo>();

            public ArchetypeInfo(WorldInfo world, Type type)
            {
                Name = type.Name;

                foreach (var iType in type.GetInterfaces().Reverse())
                {
                    if (iType.IsGenericType)
                    {
                        if (iType.GetGenericTypeDefinition() == typeof(World.WithComponent<>))
                        {
                            Components.Add(world.GetComponentInfo(iType.GetGenericArguments()[0]));
                        }
                    }
                }
            }
        }
    }
}
