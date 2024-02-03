using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fury.ECS.Editor
{
    public partial class WorldGenerator
    {
        public class ComponentInfo
        {
            public List<FieldInfo> Fields = new List<FieldInfo>();

            public readonly String Name;
            public readonly String FullName;

            public ComponentInfo(Type type)
            {
                this.Name = type.Name;
                this.FullName = type.FullName;

                foreach (var field in type.GetFields())
                {
                    Fields.Add(field);
                }
            }
        }
    }
}
