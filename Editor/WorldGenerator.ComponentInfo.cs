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

            public readonly int Id;
            public readonly String Name;
            public readonly String FullName;
            public readonly int SizeOf;

            public ComponentInfo(int id, Type type)
            {
                this.Id = id;
                this.Name = type.Name;
                this.FullName = type.FullName;
                this.SizeOf = Helper.SizeOf(type);

                foreach (var field in type.GetFields())
                {
                    Fields.Add(field);
                }
            }
        }
    }
}
